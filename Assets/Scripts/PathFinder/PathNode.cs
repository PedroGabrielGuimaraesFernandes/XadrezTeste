using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    [Header("Cordenadas do node noi grid 2D")]
    public int posX;
    public int posY;

    [Header("Diz se a casa permite movimento")]
    public bool walkable;

    //Custo para andar da celula inicial para a celula
    public int g_Cost;
    //Custo para andar da celula até a celula final
    public int h_Cost;
    //Custo para andar da celula até a celula final
    public int f_Cost {
        get
        {
            return g_Cost + h_Cost;
        }
    }

    public PathNode(int _posX, int _posY, bool _walkable)
    {
        posX = _posX;
        posY = _posY;
        walkable = _walkable;
        Debug.Log("Node( x = " + posX + ", y =" + posY + ", walkable = " + walkable + ")");
    }

    public void CalculateG_Cost(Vector3 startPos)
    {
        g_Cost = 0;
    }

    public void CalculateH_Cost(Vector3 targetPos)
    {
        h_Cost = 0;
    }

    /*public PathNode GetLowestF_Cost(List<PathNode> pathNodes)
    {
        PathNode currentNode = null;
        foreach(PathNode node in pathNodes){
            if(currentNode == null)
            {
                currentNode = node;
            }

            if (node.f_Cost < currentNode.f_Cost)
            {
                currentNode = node;
            }
        }

        return currentNode;
    }*/
}
