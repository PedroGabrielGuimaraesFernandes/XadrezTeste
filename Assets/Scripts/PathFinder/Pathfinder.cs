using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{

    PathnodeGrid grid; 

    //private PathNode currentNode;
    private PathNode intNode;
    private PathNode endNode;

    //private List<PathNode> path;

    
    private void Awake()
    {
        grid = GetComponent<PathnodeGrid>();
    }


    public void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        PathNode startNode = grid.GetNode(startPos);
        PathNode targetNode = grid.GetNode(targetPos);

        List<PathNode> openNodes = new List<PathNode>();
        HashSet<PathNode> closedNodes = new HashSet<PathNode>();

        openNodes.Add(startNode);

        /*int GetDistanceBetweenNodes(PathNode nodeA, PathNode nodeB)
        {

        }*/

        while(openNodes.Count > 0)
        {
            PathNode currentNode = openNodes[0];
            for (int i = 1; i < openNodes.Count; i++)
            {

                if (openNodes[i].f_Cost < currentNode.f_Cost || (openNodes[i].f_Cost == currentNode.f_Cost && openNodes[i].h_Cost < currentNode.h_Cost ))
                {
                    currentNode = openNodes[i];
                }
            }

            openNodes.Remove(currentNode);
            closedNodes.Add(currentNode);

            if(currentNode == targetNode)
            {
                return;
            }
            foreach(PathNode neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedNodes.Contains(neighbour))
                {
                    continue;
                }
                else {
                    openNodes.Add(neighbour);
                }
            }

        }
    }
}
