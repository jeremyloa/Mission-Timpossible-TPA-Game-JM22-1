using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarNode
{
    public bool walkable;
    public Vector3 worldPos;
    public int gridX, gridY;

    public int gCost, hCost;
    public AStarNode parentNode;
    public AStarNode(bool walkable, Vector3 worldPos, int gridX, int gridY)
    {
        this.walkable = walkable;
        this.worldPos = worldPos;
        this.gridX = gridX;
        this.gridY = gridY;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}
