
using System;
using System.Collections.Generic;
using Utils;

public class DijkstraPathFinder : IPathFinder
{
    int[,] grid;

    int cols;

    int rows;

    Dictionary<(int, int), List<((int, int), int)>> graph = new Dictionary<(int, int), List<((int, int), int)>> { };

    public DijkstraPathFinder(int[,] grid)
    {
        this.grid = grid;
        this.cols = grid.GetLength(1);
        this.rows = grid.GetLength(0);
    }

    public void Find(int startX, int startY, int endX, int endY)
    {
        (int, int) start = (startX, startY);
        (int, int) end = (endX, endY);

        GetGraph();

        List<(int, int)> path = new List<(int, int)> { };

        (int, int) curNode = start;

        while (true)
        {
            path.Add(curNode);

            if (curNode == end) break;
            else
            {
                List<((int, int), int)> sosedi = graph[(curNode.Item1, curNode.Item2)];

                PriorityQueue<(int, int), int> priorityQueue = new PriorityQueue<(int, int), int>();

                foreach (var sosed in sosedi)
                {

                    (int, int) sosedNode = sosed.Item1;

                    if (path.Contains(sosedNode)) continue;

                    int visitCost = sosed.Item2 + Heuristic(sosedNode, end);

                    priorityQueue.Enqueue(sosedNode, visitCost);

                }

                curNode = priorityQueue.Peek();

            }

        }

        foreach (var node in path)
        {
            Console.WriteLine(node.Item1 + " " + node.Item2);
            Console.WriteLine();
        }

    }

    private void GetGraph()
    {
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                graph.Add((x, y), GetSosedi(x, y));
            }
        }

    }
    private List<((int, int), int)> GetSosedi(int x, int y)
    {
        bool CheckNextNode(int x, int y)
        {
            return 0 <= x && x < cols && 0 <= y && y < rows;
        }

        List<(int, int)> ways = new List<(int, int)>
            {
                (-1, 0), (0, -1), (1, 0), (0, 1), (-1, -1), (1, -1), (1, 1), (-1, 1)
            };

        List<((int, int), int)> result = new List<((int, int), int)>();

        foreach ((int dx, int dy) in ways)
        {
            int newX = x + dx;
            int newY = y + dy;
            if (CheckNextNode(newX, newY))
            {
                if (grid[y + dy, x + dx] == 0) continue;
                else
                    result.Add(((newX, newY), grid[y + dy, x + dx]));
            }
        }
        return result;
    }

    private int Heuristic((int, int) v, (int, int) goal)
    {
        return Math.Max(Math.Abs(v.Item1 - goal.Item1), Math.Abs(v.Item2 - goal.Item2));
    }

}
