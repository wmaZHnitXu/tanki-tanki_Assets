using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathFinder 
{
    public abstract void Find(Vector2Int start, Vector2Int end);

}
