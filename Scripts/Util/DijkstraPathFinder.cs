
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class DijkstraPathFinder : IPathFinder
{
    int[,] grid;

    int cols;

    int rows;

    Dictionary<Vector2Int, List<(Vector2Int, int)>> graph = new Dictionary<Vector2Int, List<(Vector2Int, int)>> { };

    public DijkstraPathFinder(int[,] grid)
    {
        this.grid = grid;
        this.cols = grid.GetLength(1);
        this.rows = grid.GetLength(0);
    }

    public void Find(Vector2Int start, Vector2Int end)
    {

        GetGraph();

        List<Vector2Int> path = new List<Vector2Int> { };

        Vector2Int curNode = start;

        while (true)
        {
            path.Add(curNode);

            if (curNode == end) break;
            else
            {
                List<(Vector2Int, int)> sosedi = graph[curNode];

                PriorityQueue<Vector2Int, int> priorityQueue = new PriorityQueue<Vector2Int, int>();

                foreach (var sosed in sosedi)
                {

                    Vector2Int sosedNode = sosed.Item1;

                    if (path.Contains(sosedNode)) continue;

                    int visitCost = sosed.Item2 + Heuristic(sosedNode, end);

                    priorityQueue.Enqueue(sosedNode, visitCost);

                }

                curNode = priorityQueue.Peek();

            }

        }

        foreach (var node in path)
        {
            Console.WriteLine(node.x + " " + node.y);
            Console.WriteLine();
        }

    }

    private void GetGraph()
    {
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                Vector2Int coords = new Vector2Int(x, y);
                graph.Add(coords, GetSosedi(coords));
            }
        }

    }
    private List<(Vector2Int, int)> GetSosedi(Vector2Int tilePosition)
    {
        bool CheckNextNode(int x, int y)
        {
            return 0 <= x && x < cols && 0 <= y && y < rows;
        }

        List<Vector2Int> ways = new List<Vector2Int>
            {
                new Vector2Int(-1, 0),
                new Vector2Int(0, -1),
                new Vector2Int(1, 0),
                new Vector2Int(0, 1),
                new Vector2Int(-1, -1),
                new Vector2Int(1, -1),
                new Vector2Int(1, 1),
                new Vector2Int(-1, 1)
            };

        List<(Vector2Int, int)> result = new List<(Vector2Int, int)>();

        foreach (Vector2Int d in ways)
        {
            int newX = tilePosition.x + d.x;
            int newY = tilePosition.y + d.y;
            if (CheckNextNode(newX, newY))
            {
                if (grid[tilePosition.y + d.y, tilePosition.x + d.x] == 0) continue;
                else
                    result.Add((new Vector2Int(newX, newY), grid[tilePosition.y + d.y, tilePosition.x + d.x]));
            }
        }
        return result;
    }

    private int Heuristic(Vector2Int v, Vector2Int goal)
    {
        return Math.Max(Math.Abs(v.x - goal.x), Math.Abs(v.y - goal.y));
    }

}
