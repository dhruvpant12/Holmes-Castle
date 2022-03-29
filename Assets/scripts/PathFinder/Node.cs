using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node> 
{
    public bool iswalkable; // Checking is node is walkle or not
    public Vector3 nodeposition; // Position of the node on the world map.

    public int gridX;
    public int gridY;

    public int gcost; // cost to neighbour
    public int hcost; // cost of heuritics to goal.
        public int fcost // final cost for evaluation.  
    {
        get
        {
            return gcost + hcost;
        }
    }

    public Node parent;
    int heapindex;
    public Node(bool _iswalkable, Vector3 _nodeposition, int _gridX, int _gridY)
    {
        iswalkable = _iswalkable;
        nodeposition = _nodeposition;
        gridX = _gridX;
        gridY = _gridY;

    }

    public int HeapIndex
    {
        get
        {
            return heapindex;
        }
        set
        {
            heapindex = value;
        }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fcost.CompareTo(nodeToCompare.fcost);
        if(compare == 0)
        {
            compare = hcost.CompareTo(nodeToCompare.hcost);
        }
        return -compare;
    }
}
