using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class AStarGrid : MonoBehaviour
{
    [SerializeField] public LayerMask ground, noWalk;
    public Vector2 worldSize;
    public float nodeRadius;
    AStarNode[,] grid;
    float nodeDiameter;
    int sizeX, sizeY;
    //public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        sizeX = Mathf.RoundToInt(worldSize.x / nodeDiameter);
        sizeY = Mathf.RoundToInt(worldSize.y / nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new AStarNode[sizeX, sizeY];
        Vector3 botLeft = transform.position - Vector3.right * (worldSize.x / 2) - Vector3.forward * (worldSize.y / 2);
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y <sizeY; y++)
            {
                Vector3 worldPoint = 
                    botLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, noWalk));
                grid[x,y] = new AStarNode(walkable, worldPoint, x, y);
            }
        }
    }

    public List<AStarNode> GetNext(AStarNode node)
    {
        List<AStarNode> nexts = new List<AStarNode>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                if (checkX >=0 && checkX < sizeX && checkY >=0 && checkY<sizeY)
                {
                    nexts.Add(grid[checkX, checkY]);
                }
            }
        }
        return nexts;
    }
    public AStarNode NodeFromWorldPoint(Vector3 worldPos)
    {
        float percentX = Mathf.Clamp01(Mathf.Abs((worldPos.x + 350 + 15 + (worldSize.x / 2)) / worldSize.x));
        float percentY = Mathf.Clamp01((worldPos.z + (worldSize.y / 2)) / worldSize.y);
        //Debug.Log("world size x = " + worldSize.x + " world size y = " + worldSize.y);
        Debug.Log("world pos x = " + worldPos.x + " world pos y = " + worldPos.y);
        //Debug.Log("size x = " + sizeX + "size y = " + sizeY);
        Debug.Log("percent x = " + percentX + " percent y = " + percentY);
        int x = Mathf.RoundToInt((sizeX - 1) * percentX) -28;
        int y = Mathf.RoundToInt((sizeY - 1) * percentY) +43;
        Debug.Log("x: " + x + " y: " + y);
        return grid[x, y];
    }
    public List<AStarNode> path;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(worldSize.x, 1, worldSize.y));
        if (grid!=null)
        {
            Debug.Log(path);
            Debug.Log("Count Path: " + path.Count);
            //AStarNode playerNode = NodeFromWorldPoint(player.transform.position);
            foreach (AStarNode node in grid)
            {
                if (node.walkable == true) Gizmos.color = Color.white;
                else Gizmos.color = Color.red;
                if (path != null)
                {
                    if (path.Contains(node)) Gizmos.color = Color.black;
                }
                Gizmos.DrawCube(node.worldPos, Vector3.one * (nodeDiameter - .1f));
                //if (playerNode == node) Gizmos.color = Color.cyan;
            }
        }
    }
}
