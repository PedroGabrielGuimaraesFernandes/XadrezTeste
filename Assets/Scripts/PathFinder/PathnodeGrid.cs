using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathnodeGrid : MonoBehaviour
{
    //Tilemap que ocntem a parte visual
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase tileWhite;

    public PathNode[,] pathNodes;


    [Header("Posição de origem do grid")]
    public Vector3 originPosition = Vector3.zero;
    [Header("Largura do grid")]
    public int gridWidth = 1;
    [Header("Altura do grid")]
    public int gridHeight = 1;
    [Header("Tamanho de um node")]
    public float nodeSize = 1; // o raaio do  node é metade desse valor
    private float nodeRadius;

    private int gridX, gridY;

    private void Start()
    {
        nodeRadius = nodeSize / 2;
        CreateGrid(gridWidth, gridHeight, originPosition);
    }

    private void CreateGrid(int width, int height, Vector3 origin)
    {
        pathNodes = new PathNode[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                bool walkable = false;
                if(tilemap.GetTile( GetWorldPositionInt(x,y)) == tileWhite)
                walkable = true;
                Debug.Log(x + ", " + y);
                pathNodes[x, y] = new PathNode(x, y, walkable); 
            }
        }
    }

    //Transforma o X e o Y do grid em um ponto do mundo
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * nodeSize + originPosition;
    }

    public Vector3Int GetWorldPositionInt(int x, int y)
    {
        Vector3 position = new Vector3(x, y) * nodeSize + originPosition;
        return new Vector3Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y), 0);
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        //Se o cellsize for 10 então tudo de 0 a 10 sera dentro do grid 0, de 10 a 20 sera no grid 1...
        x = Mathf.FloorToInt((worldPosition - originPosition).x / nodeSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / nodeSize);
    }

    public PathNode GetNode(int x, int y)
    {
        return pathNodes[x, y];
    }

    public PathNode GetNode(Vector3 nodePos)
    {
        GetXY(nodePos, out int x, out int y);
        return GetNode(x, y);
    }

    public List<PathNode> GetNeighbours(PathNode node)
    {
        List<PathNode> neighbours = new List<PathNode>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                //Se for 0 e 0 significa que é o node
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.posX + x;
                int checkY = node.posY + y;

                if (checkX >= 0 && checkX < gridWidth && checkY >= 0 && checkY < gridHeight)
                {
                    neighbours.Add(pathNodes[checkX, checkY]);
                }
            }
        }
            return neighbours;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireCube(new Vector3((originPosition.x + gridWidth)/2,(originPosition.y + gridHeight)/2), new Vector3(gridWidth,gridWidth));

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if(pathNodes != null)
                    if (pathNodes[x, y] != null)
                        Gizmos.color = (pathNodes[x, y].walkable) ? Color.blue : Color.red;
                
                Gizmos.DrawSphere(GetWorldPosition(x, y) + new Vector3(nodeSize / 2, nodeSize / 2), nodeSize * .25f);
                Gizmos.color = Color.white;
                Gizmos.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y));
                Gizmos.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1));
                //Gizmos.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x +1, y + 1));
                //Gizmos.DrawLine(GetWorldPosition(x+1,y), GetWorldPosition(x, y+1));                
            }
        }

        Gizmos.DrawLine(GetWorldPosition(0, gridHeight), GetWorldPosition(gridWidth, gridHeight));
        Gizmos.DrawLine(GetWorldPosition(gridWidth, 0), GetWorldPosition(gridWidth, gridHeight));
    }
}
