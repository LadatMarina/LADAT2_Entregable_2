using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    public int value;
    public Sprite blackTileSprite;
    public Sprite whiteTileSprite;

    public List<Vector3> blackTilePositionsList;
    public List<Vector3> whiteTilePositionsList;

    private bool chessboardIsCreated;

    private int blackValue;
    private int whiteValue;
    
    void Start()
    {
        
        //bones tardes
        

    }

    void Update()
    {
        
        if (value != 0 && chessboardIsCreated == false) //si hi ha un valor de tampany per es chessboard...
        {
            //set camera in the center of the chessBoard
            Camera.main.transform.position = new Vector3(value / 2, value / 2, -10);

            for(int y = value; y>0; y--)
            {
                if(y%2 == 0) //si és parell,
                {
                    blackValue = value;
                    whiteValue = value - 1;
                }
                else
                {
                    blackValue = value-1;
                    whiteValue = value;
                }

                for(int x= blackValue; x > 0; x-=2) //mentre i sigui major que 0, anam baixant 2
                {
                    //creació de sa tile black
                    GameObject blackTile = new GameObject("black Tile");
                    SpriteRenderer blackSpriteRenderer = blackTile.AddComponent<SpriteRenderer>();
                    blackSpriteRenderer.sprite = blackTileSprite;

                    //set tile position
                    blackTile.transform.position = new Vector3(x, y, 0);

                    //save tile position into the list
                    blackTilePositionsList.Add(blackTile.transform.position);

                    Debug.Log(blackTile.transform.position);
                }
                for(int x = whiteValue; x>0; x -= 2)
                {
                    //creació de sa tile white
                    GameObject whiteTile = new GameObject("white Tile");
                    SpriteRenderer whiteSpriteRenderer = whiteTile.AddComponent<SpriteRenderer>();
                    whiteSpriteRenderer.sprite = whiteTileSprite;

                    //set tile position
                    whiteTile.transform.position = new Vector3(x, y, 0);

                    //save tile position into the list
                    whiteTilePositionsList.Add(whiteTile.transform.position);

                    Debug.Log(whiteTile.transform.position);
                }
                chessboardIsCreated = true;
            }
            
        }
    }
}
