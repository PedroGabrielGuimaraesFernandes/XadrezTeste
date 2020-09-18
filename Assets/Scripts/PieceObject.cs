using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PieceObject : MonoBehaviour
{
    [SerializeField] private PiecesManager piecesManager;
    public enum PieceColor {White, Black }
    public PieceColor pieceColor;

    public Sprite pieceSpriteWhite;
    public Sprite pieceSpriteBlack;
    //public TileBase pieceTile;

    [SerializeField] private int x = 0;
    [SerializeField] private int y = 0;

    public void Start()
    {
        piecesManager.SetPiecePosition(this, x, y);
    }

    public int GetX()
    {
        return x;
    }

    public int GetY()
    {
        return y;
    }

    public void SetXY(int newX, int newY)
    {
        x = newX;
        y = newY;
    }

    public void MovePiece(int x, int y)
    {
        SetXY(x, y);
        transform.position = new Vector3(x + .5f, y + .5f);
    }
}
