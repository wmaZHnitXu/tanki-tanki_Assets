using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathFinder 
{
    public abstract void Find(int startX, int startY, int endX, int endY);

}
