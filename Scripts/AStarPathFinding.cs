using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathFinding : MonoBehaviour
{
    public Transform start, end;
    public AStarGrid grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponent<AStarGrid>();   
    }

    // Update is called once per frame
    void Update()
    {
        FindPath(start.position, end.position);
    }

    void FindPath(Vector3 startPos, Vector3 endPos)
    {
        AStarNode startNode = grid.NodeFromWorldPoint(startPos);
        AStarNode endNode = grid.NodeFromWorldPoint(endPos);

        List<AStarNode> open = new List<AStarNode>();
        HashSet<AStarNode> closed = new HashSet<AStarNode>();
        open.Add(startNode);

        while(open.Count > 0)
        {
            AStarNode node = open[0];
            for (int i = 1; i<open.Count; i++)
            {
                if (open[i].fCost < node.fCost || open[i].fCost == node.fCost)
                {
                    if (open[i].hCost < node.hCost) node = open[i];
                }
            }

            closed.Add(node); open.Remove(node);

            if (node == endNode)
            {
                Retrace(startNode, endNode);
                return;
            }

            foreach (AStarNode next in grid.GetNext(node))
            {
                if (next.walkable == false || closed.Contains(next)) continue;
                int newCosttoNext = node.gCost + Dist(node, next);
                if (newCosttoNext < next.gCost || !open.Contains(next))
                {
                    next.gCost = newCosttoNext;
                    next.hCost = Dist(next, endNode);
                    next.parentNode = node;

                    if (!open.Contains(next)) open.Add(next);
                }
            }

            void Retrace(AStarNode startNode, AStarNode endNode)
            {
                List<AStarNode> path = new List<AStarNode>();
                AStarNode curr = endNode;

                while (curr!=startNode)
                {
                    path.Add(curr);
                    curr = curr.parentNode;
                }
                path.Reverse();
                grid.path = path;
                //Debug.Log(path.Count);
            }

            int Dist(AStarNode a, AStarNode b)
            {
                int distX = Mathf.Abs(a.gridX - b.gridX);
                int distY = Mathf.Abs(a.gridY - b.gridY);

                if (distX > distY) return 14 * distY + 10 * (distX - distY);
                return 14 * distX + 10 * (distY - distX);
            }
        }
    }
}
