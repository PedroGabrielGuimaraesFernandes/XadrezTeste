using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PedroG.UsefulFuncs;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private PiecesManager piecesManager;

    public int boardWidth;
    public int boardHeight;

    // Start is called before the first frame update
    void Start()
    {
        if (piecesManager == null)
            piecesManager = GetComponent<PiecesManager>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            piecesManager.PrintPieces();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            piecesManager.DeselectPiece();
        }

            Vector3 mousePos = UsefulFunctions.GetMouseWorldPosition();
        if (Input.GetMouseButtonDown(0))
        {

            piecesManager.GetXY(mousePos, out int x, out int y);
            if (x >= 0 && y >= 0 && x < boardWidth && y < boardHeight) {
                if (piecesManager.selectedPiece != null) {
                    piecesManager.MovePiece(mousePos, piecesManager.selectedPiece);                    
                }
                else
                {
                    piecesManager.SelectPiece(mousePos);
                }
            }
        }
    }
}
