using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomGenerator : MonoBehaviour {

    public int x;
    public int y;

    bool bordX;
    bool bordY;

    public Vector3 origin;

    #region Tileset

    public GameObject colonne;
    public List<GameObject> mur;
    public List<GameObject> sol;



    #endregion

    public List<GameObject> assets;

	//Arrays

	public bool[,] aColonnes;

	public int minSize;

    int direction; //0 horizontal / 1 vertical

    /*
    void Start () 
	{
        
	}
	
	void Update () 
	{
        CheckInput();
	}

    void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            EraseRoom();
            DrawRoom();
        }
        
    }
    */


    public void DrawRoom()
    {
        aColonnes = new bool[x+1, y+1];
        EraseRoom();

        for (int i = 0; i <= x; i++)
        {
            if (i == 0 || i == x)
                bordX = true;

            for (int o = 0; o <= y; o++)
            {
                if (o == 0 || o == y)
                    bordY = true;

                if(bordX||bordY)
                {
                    //Colonne(i,o);
                    //print(i + " " + o);
                    


                        aColonnes[i, o] = true;
                    

                    //if (bordX && o<y)
                    
                       // MurX(i, o);
                    //if (bordY && i<x)
                        //MurY(i, o);
                    
                    bordY = false;
                }
                else
                {
                    aColonnes[i, o] = false ;
                }

                if(i!=x && o!=y)
                Sol(i, o);

            }
            bordX = false;
        }

        Divide((int)origin.x, (int)origin.y, (int)origin.x + x, (int)origin.z + y);

        PutColonne();

        SetParent();
    }

    void Colonne(int _x, int _y)
    {
        assets.Add( Instantiate(colonne, new Vector3(_x, 0, _y), colonne.transform.rotation));
    }

    void MurX(int _x, int _y)
    {
        int _choice = Random.Range(0, mur.Count);
        assets.Add(Instantiate(mur[_choice], new Vector3(_x, 0, _y), Quaternion.Euler(new Vector3(0, -90, 0))));
        
    }

    void MurY(int _x, int _y)
    {
        int _choice = Random.Range(0, mur.Count);
        assets.Add(Instantiate(mur[_choice], new Vector3(_x, 0, _y), colonne.transform.rotation));
    }

    void Sol(int _x, int _y)
    {
        int _choice = Random.Range(0, sol.Count);

        assets.Add(Instantiate(sol[_choice], new Vector3(_x, 0, _y), colonne.transform.rotation));
    }

    public void EraseRoom()
    {
        foreach (GameObject _asset in assets)
        {
            DestroyImmediate(_asset);
        }
        assets.Clear();
    }

    void SetParent()
    {
        foreach (GameObject _asset in assets)
        {
            _asset.transform.SetParent(gameObject.transform);
        }
    }

    void MakeMesh(List<GameObject> _assets)
    {
        
        
    }

	void Divide(int startX,int startY, int endX,int endY)
	{
        int _x = endX-startX;
        int _y = endY-startY;

        
        direction = Random.Range(0, 2);
        
        switch(direction)
        {
            case 0:  //Try divide up

                if (_y / 2 > minSize)
                {
                    _y = _y / 2 + Random.Range(-1, 2);
                    for (int i = startX + 1; i < endX; i++)
                    {
                        aColonnes[i, _y] = true;
                    }
                }
                else if (_x / 2 > minSize)
                {
                    _x = _x / 2 + Random.Range(-1, 2);
                    for (int i = startY + 1; i < endY; i++)
                    {
                        aColonnes[_x, i] = true;
                    }
                }
                else
                {
                    Debug.Log("Can't Divide more");
                }
                break;

            case 1: //Try divide right

                if (_x / 2 > minSize)
                {
                    _x = _x / 2 + Random.Range(-1, 2);
                    for (int i = startY + 1; i < endY; i++)
                    {
                        aColonnes[_x, i] = true;
                    }
                }
                else if (_y / 2 > minSize)
                {
                    _y = _y / 2 + Random.Range(-1, 2);
                    for (int i = startX + 1; i < endX; i++)
                    {
                        aColonnes[i,_y] = true;
                    }
                }
                else
                {
                    Debug.Log("Can't Divide more");
                }
                break;

            default:

                Debug.Log("Couldn't decide which way to go");

                break;
        }

       
		//coupe à peu près au milieu
		//place des colonnes sur le x de la découpe et les y de la room (ou l'inverse)

	}

    void PutColonne()
    {
        for (int i = 0; i < x+1; i++)
        {
            for (int o = 0; o < y + 1; o++)
            {

                
                    if (aColonnes[i, o])
                    {
                        print(aColonnes[i, o]);
                        assets.Add(Instantiate(colonne, new Vector3(i, 0, o), colonne.transform.rotation));
                    }
                
            }
        }
    }
}
