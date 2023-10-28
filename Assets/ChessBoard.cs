using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard   : MonoBehaviour
{
    public int value;
    public Sprite blackTileSprite;
    public Sprite whiteTileSprite;
    public Sprite appleSprite;

    public List<Vector3> blackTilePositionsList;
    public List<Vector3> whiteTilePositionsList;
    public List<Vector3> possiblePositionsToInstance;

    bool chessboardIsCreated;
    bool isOnTheWhiteTileVariable;
    //bool blackTilesAreFull = false;
    //bool whiteTilesAreFull = false;
    //bool canCreateApples = true;

    private int blackValue;
    private int whiteValue;

    Vector3 randomPositionFruit;

    int points;

    void Update()
    {
        CreateChessBoard();
        AddingListFunciton();
        StartCoroutine(InstanceApples()); 
    }
    

    private void CreateChessBoard()
    {

        if (value != 0 && chessboardIsCreated == false) //si hi ha un valor de tampany per es chessboard...
        {
            //set camera in the center of the chessBoard
            Camera.main.transform.position = new Vector3(value / 2, value / 2, -10);

            for (int y = value; y > 0; y--)
            {
                if (y % 2 == 0) //si és parell,
                {
                    blackValue = value;
                    whiteValue = value - 1;
                }
                else
                {
                    blackValue = value - 1;
                    whiteValue = value;
                }

                for (int x = blackValue; x > 0; x -= 2) //mentre i sigui major que 0, anam baixant 2
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
                for (int x = whiteValue; x > 0; x -= 2)
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

    private List<Vector3> AddingListFunciton()
    {
        possiblePositionsToInstance.AddRange(blackTilePositionsList);
        possiblePositionsToInstance.AddRange(whiteTilePositionsList);
        return possiblePositionsToInstance;
    }

    private IEnumerator InstanceApples()
    {
        for(int i = possiblePositionsToInstance.Count; i>=0; i--)
        {
            RandomPositionOfTheList();

            //create the apple
            GameObject appleFruitGO = new GameObject("apple Fruit");
            SpriteRenderer appleSpriteRenderer = appleFruitGO.AddComponent<SpriteRenderer>();
            appleSpriteRenderer.sprite = appleSprite;
            //Renderer appleRenderer = appleFruitGO.AddComponent<Renderer>();
            appleSpriteRenderer.sortingOrder = 10;
            appleFruitGO.transform.position = randomPositionFruit;

            if(isOnTheWhiteTileVariable == true)
            {
                points++;
            }
            else
            {
                points += 5;
            }

            yield return new WaitForSeconds(20f);
        }
    }


    public void RandomPositionOfTheList()
    {
        //choose the random index
        int randomIndex = Random.Range(0, possiblePositionsToInstance.Count);

        Debug.Log(randomIndex);
        //set the apple position to the random pos
        randomPositionFruit = possiblePositionsToInstance[randomIndex];

        //delete the position where the apple has been created off the list --> NO SE SI ME CONVÉ FER-HO AQUÍ PER S'ORDRE
        possiblePositionsToInstance.RemoveAt(randomIndex);

        isOnTheWhiteTileVariable = IsOnTheWhiteTile(randomIndex);
    }

    public bool IsOnTheWhiteTile(int index)
    {
        //si sa posició de sa poma està a sa llista white return true, else, return false
        if (whiteTilePositionsList[index] != null)
        {
            return true;
        }
        return false;
    } 


    /*
    private IEnumerator InstanceApples()
    {
        Debug.Log(blackTilePositionsList.Count);
        Debug.Log(whiteTilePositionsList.Count);

        //choose random pos of the list
        RandomPositionOfTheList();
        while (canCreateApples == true)
        { 
            //create the apple
            GameObject appleFruitGO = new GameObject("apple Fruit");
            SpriteRenderer appleSpriteRenderer = appleFruitGO.AddComponent<SpriteRenderer>();
            appleSpriteRenderer.sprite = appleSprite;
            //Renderer appleRenderer = appleFruitGO.AddComponent<Renderer>();
            appleSpriteRenderer.sortingOrder = 10;
            appleFruitGO.transform.position = randomPositionFruit; 

            yield return new WaitForSeconds(20f);
        }

    }


    public void RandomPositionOfTheList() //pensar amb s' = de es count
    {
        if (!(blackTilePositionsList.Count < 0 && whiteTilePositionsList.Count < 0)) //si no estan plenes...
        {
            int blackOrWhite = Random.Range(0, 1);
            if (blackOrWhite == 1)
            {
                RandomWhitePos();
            }
            else
            {
                RandomBlackPos();
            }

        }
        else if (!(blackTilePositionsList.Count < 0) && whiteTilePositionsList.Count < 0) // WHITE FULL
        {
            RandomBlackPos();
        }
        else if (blackTilePositionsList.Count < 0 && !(whiteTilePositionsList.Count < 0)) //BLACK FULL
        {
            RandomWhitePos();
        }
        else if(blackTilePositionsList.Count < 0 && whiteTilePositionsList.Count < 0)
        {
            canCreateApples = false;
        }
    }

    private void RandomWhitePos()
    {
        //choose the random index
        int randomWhiteListIndex = Random.Range(0, whiteTilePositionsList.Count);

        Debug.Log(randomWhiteListIndex);
        //set the apple position to the random pos
       randomPositionFruit = whiteTilePositionsList[randomWhiteListIndex];

        points++;

        //delete the position where the apple has been created off the list
        whiteTilePositionsList.RemoveAt(randomWhiteListIndex);

    }
    
    private void RandomBlackPos()
    {
        //choose the random index
        int randomBlackListIndex = Random.Range(0, blackTilePositionsList.Count);

        //set the apple position to the random pos
        randomPositionFruit = blackTilePositionsList[randomBlackListIndex];

        points += 2;

        //delete the position where the apple has been created off the list
        blackTilePositionsList.RemoveAt(randomBlackListIndex);
    }*/
}
