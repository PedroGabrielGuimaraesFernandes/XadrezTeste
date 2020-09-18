using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesManager : MonoBehaviour
{
    private BoardManager boardManager;
    //private Piece[,] boardPieces;

    private PieceObject[,] boardPieces;

    private List<PieceObject> eatemPieces = new List<PieceObject>(32);

    public PieceObject selectedPiece;

    private void Awake()
    {
        if (boardManager == null)
            boardManager = GetComponent<BoardManager>();

        boardPieces = new PieceObject[boardManager.boardWidth, boardManager.boardHeight];
    }

    public void PrintPieces()
    {
        for (int x = 0; x < boardPieces.GetLength(0); x++)
        {
            for (int y = 0; y < boardPieces.GetLength(1); y++)
            {
                string pieceName = (boardPieces[x, y] != null) ? boardPieces[x, y].name:"null";
                print("x: " + x + ", y: " + y + ", Piece:" + pieceName);
            }
        }
    }

    public void SetPiecePosition(PieceObject piece, int newX, int newY)
    {
        boardPieces[piece.GetX(), piece.GetY()] = null;
        piece.SetXY(newX, newY);
        boardPieces[newX, newY] = piece;
        piece.MovePiece(newX, newY);
        DeselectPiece();
    }

    public void GetXY(Vector3 piecePosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((piecePosition - transform.position/* posição de origem do grid*/).x );
        y = Mathf.FloorToInt((piecePosition - transform.position).y);
    }

    public void MovePiece(Vector3 checkPosition, PieceObject piece)
    {
        GetXY(checkPosition, out int x, out int y);
        if (CheckEspace(x, y))
        {
            if (piece.pieceColor != boardPieces[x, y].pieceColor)
            {
                EatPiece(piece, boardPieces[x, y], x, y);
            }
            else
            {
                Debug.Log("Não é possivel ir para casa pois já tem uma peça sua lá");
                SelectPiece(x, y);
            }
        }
        else
        {
            SetPiecePosition(piece, x, y);
        }
    }

    public bool CheckEspace(int x, int y)
    {
       
        if(boardPieces[x, y] != null)
        {
            //Fazer que não pode move pra cá ou que deve comer a peça
            return true;            
        }
        else
        {
            //Fazer mover
            return false;            
        }

    }

    private void EatPiece(PieceObject piece, PieceObject pieceToEat, int x, int y)
    {
        eatemPieces.Add(pieceToEat);
        pieceToEat.gameObject.SetActive(false);
        Debug.Log(piece.name + "(" + piece.GetX() + ", " + piece.GetY() + ") comeu " + pieceToEat.name + " (" + pieceToEat.GetX() + ", " + pieceToEat.GetY() + ")");
        SetPiecePosition(piece, x, y);
        DeselectPiece();
    }

    public void SelectPiece(int x, int y)
    {
        selectedPiece = boardPieces[x, y];
        if(selectedPiece != null)
        Debug.Log(selectedPiece.name + " foi selecionada");
    }

    public void SelectPiece(Vector3 worldPosition)
    {
        GetXY(worldPosition, out int x, out int y);
        if(CheckEspace(x, y))
        {
            if(selectedPiece == null || selectedPiece.pieceColor == boardPieces[x, y].pieceColor)
            SelectPiece(x, y);
        }
        else
        {
            DeselectPiece();
        }
    }

    public void DeselectPiece()
    {
        selectedPiece = null;
        Debug.Log("Não há peça selecionada");
    }
}
