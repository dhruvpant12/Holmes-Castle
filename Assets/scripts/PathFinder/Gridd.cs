using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gridd : MonoBehaviour
{
     
    public LayerMask obstacles; // This will flag is a node is unpassable.
    public Vector2 gridsize; //This will be the size of the playable area.
    public float radiusOfNode; //This will decide how much area each node occupies.
    Node[,] grid;

    float nodeDiameter;
    int gridX, gridY;


    private void Start()
    {
        nodeDiameter = radiusOfNode * 2;
        gridX = Mathf.RoundToInt(gridsize.x / nodeDiameter); //This will return how many nodes are there on the x axis.
        gridY = Mathf.RoundToInt(gridsize.y / nodeDiameter); //This will return how many nodes are there on the y axis.
        CreateGrid();
    }

    public int Maxsize
    {
        get{
            return gridX * gridY;
        }
    }
    void CreateGrid()
    {
        grid = new Node[gridX, gridY];
        Vector3 worldSE = transform.position - Vector3.right * gridsize.x / 2 - Vector3.forward * gridsize.y /2; //This will provide the bottom left edge of the play area.

        for(int x=0;x < gridX; x++)
        {
            for(int y=0; y < gridY; y++)
            {
                Vector3 worldposition = worldSE + Vector3.right * (x * nodeDiameter + radiusOfNode) + Vector3.forward * (y * nodeDiameter + radiusOfNode); // This will start generating nodes from the bottom left corner of the map.
                bool ispassable = !(Physics.CheckSphere(worldposition, radiusOfNode, obstacles)); // Check if node is passable. From laymask , it will check if it is obstacle or not. Values assign in Unity editor.
                grid[x, y] = new Node(ispassable, worldposition, x, y);
            }
        }
    }


    //This will establish all the nodes that are connected to a particular node.
    public List<Node> Neighbour(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for(int x=-1; x <=1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if(x==0 && y==0)                 
                    continue;

                int neighbX = node.gridX + x;
                int neighY = node.gridY + y;

                if(neighbX >=0 && neighY < gridX && neighbX >=0 && neighY < gridY)
                {
                    neighbours.Add(grid[neighbX, neighY]);
                }

            }
        }

        return neighbours;
    }

    public Node nodeFromWorldMap(Vector3 wp)
    {
        float estimateX = (wp.x + gridsize.x / 2) / gridsize.x;
        float estimateY = (wp.z + gridsize.y / 2) / gridsize.y;
        estimateX = Mathf.Clamp01(estimateX); //Clamping values between 0 and 1
        estimateY = Mathf.Clamp01(estimateY); //Clamping values between 0 and 1
        int x = Mathf.RoundToInt((gridX - 1) * estimateX);
        int y = Mathf.RoundToInt((gridY - 1) * estimateY);
        return grid[x, y];
    }

    public List<Node> path;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridsize.x,1,gridsize.y));// Essentially , we are trying to pass X,Y and Z coordinates into the gizmo. Since gridsize is 2D , we are passing its y coordinate into the Z coordinate of the gizmo.
                                                                                      //Our world map allows movement in the X and the Z coordinate. So , the 2D grid Y coordinate is working as the Z coordinate in the world map.  
         
        if(grid != null)
        {
             
            foreach(Node n in grid)
            {

                Gizmos.color = (n.iswalkable) ? Color.white : Color.red; // If obstacle , color of cube is red . If not , then white.
                if(path != null)
                {
                    if (path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                    }
                }
                Gizmos.DrawCube(n.nodeposition, Vector3.one * (nodeDiameter - 0.1f));// THis will draw a cube in the Unity editor
            }
                   
        }
    }    
    

}
