using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public Transform seeker, target;
    Gridd grid;

    private void Awake()
    {
        grid = GetComponent<Gridd>();
    }

    private void Update()
    {
        Path(seeker.position, target.position);
    }
    void Path(Vector3 startingposition,Vector3 targetposition)
    {
        Node enemyposition = grid.nodeFromWorldMap(startingposition); //startposition of NPC
        Node endposition= grid.nodeFromWorldMap(targetposition); // targetposition of player

        Heap<Node> openset = new Heap<Node>(grid.Maxsize); // This will hold all the node to be explored.
        HashSet<Node> closedset = new HashSet<Node>(); // This will hold node deemed worthy of exloration.
        openset.Add(enemyposition); // Enemy starting position.

        while(openset.Count>0)
        {
            Node currentNode = openset.RemoveFirst();
            
            closedset.Add(currentNode);

            if(currentNode == endposition)
            {
                TracingPath(enemyposition, endposition);
                return;
            }

            foreach(Node neighbour in grid.Neighbour(currentNode))
            {
                if (!neighbour.iswalkable || closedset.Contains(neighbour))
                {
                    continue;
                }

                int costToNeighbour = currentNode.gcost + GetDistance(currentNode, neighbour); //This will return back the value of distance between nodes
                if(costToNeighbour < neighbour.gcost || !openset.Contains(neighbour))
                {
                    neighbour.gcost = costToNeighbour; 
                    neighbour.hcost = GetDistance(neighbour, endposition); // This will get the heuristic of the node towards the target position.
                    neighbour.parent = currentNode; // This will help in understand from where the path is coming from. 

                    if (!openset.Contains(neighbour))
                        openset.Add(neighbour);
                }
            }


        }
    }

    void TracingPath(Node initialNode, Node targetNode)
    {
        List<Node> finalpath = new List<Node>(); // This will hold all the nodes that are making up the final path.
        Node currentnode = targetNode;

        while(currentnode != initialNode)
        {
            finalpath.Add(currentnode);
            currentnode = currentnode.parent;
        }

        finalpath.Reverse(); //Initiall the path is stored in reverse direction . Starting from target to NPC. we need to set path as NPC to target.

        grid.path = finalpath; 
    }

    //This will measure the distance between nodes.
    int GetDistance(Node A, Node B)
    {
        int distanceX = Mathf.Abs(A.gridX - B.gridX);
        int distanceY = Mathf.Abs(A.gridY - B.gridY);

        if (distanceX > distanceY)  
            return 14 * distanceY + 10 * (distanceX - distanceY);
        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
}
