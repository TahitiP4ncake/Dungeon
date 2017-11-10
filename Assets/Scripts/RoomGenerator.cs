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

    
    public List<GameObject> mur;
    public List<GameObject> sol;

	public GameObject colonne;

    public GameObject porte;

	public GameObject torche;

    #endregion

    [Space]

    public List<GameObject> assets;

	//Arrays

	public bool[,] arrayColonnes;

    bool[,] arrayPortesX;

    bool[,] arrayPortesY;



    [Space]

	public int minSize;

	public int variationMax;



    int direction; //0 horizontal / 1 vertical

    public int iteration;

    int variation;

    List<List<int>> listMur;

    List<List<int>> tampon = new List<List<int>>();

    [Space]

	public bool generateOnStart;

    void Start () 
	{
		if(generateOnStart)
       DrawRoom();
	}

	/*
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
        iteration = 0;

        arrayColonnes = new bool[x+1, y+1];
        arrayPortesX = new bool[x + 1, y + 1];
        arrayPortesY = new bool[x + 1, y + 1];
        listMur = new List<List<int>>();

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
                    


                        arrayColonnes[i, o] = true;
                    

                    //if (bordX && o<y)
                    
                       // MurX(i, o);
                    //if (bordY && i<x)
                        //MurY(i, o);
                    
                    bordY = false;
                }
                else
                {
                    arrayColonnes[i, o] = false ;
                }

                if(i!=x && o!=y)
                Sol(i, o);

            }
            bordX = false;
        }

        Divide((int)origin.x, (int)origin.y, (int)origin.x + x, (int)origin.z + y);

        //SetDoors();

        CreateRooms();

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

        assets.Add(Instantiate(sol[_choice], new Vector3(_x, 0, _y), Quaternion.Euler(new Vector3(0, -90, 0))));
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
        //print("tentative division");
        

        int _x = endX-startX;
        int _y = endY-startY;



        //direction = Random.Range(0, 2);

        direction = 0;

		variation = Random.Range(-variationMax, variationMax+1);

        switch (direction)
        {
            case 0:  //Try divide up

                if ((startY +_y / 2 - Mathf.Abs(variation) + minSize ) < endY && (startY + _y / 2 - Mathf.Abs(variation) - minSize) > startY)
                {
                    
                    iteration += 1;

                    

                    _y = _y / 2  + variation + startY;
                    //print("je divise y a " + _y);
                    for (int i = startX + 1; i < endX; i++)
                    {
                        arrayColonnes[i, _y] = true;
                    }
					/*
                    List<int> _mur = new List<int>();
                    _mur.Add(startX);
                    _mur.Add(_y);
                    _mur.Add(endX);
                    _mur.Add(_y);
                    listMur.Add(_mur);
                    */
				int doorX = Random.Range(startX,endX);
					int doorY = _y;
					arrayPortesY [doorX, doorY] = true;

                    Divide(startX, startY, endX, _y);
                    
                    Divide(startX, _y, endX, endY);


                }
                else if ((startX + _x / 2 - Mathf.Abs(variation) + minSize) < endX &&(startX +  _x / 2 - Mathf.Abs(variation) - minSize) > startX)
                {
                    //print("je divise x");
                    iteration += 1;

                    _x = _x / 2 + variation+startX;

                    for (int i = startY + 1; i < endY; i++)
                    {
                        arrayColonnes[_x, i] = true;
                    }
					
				/*
                    List<int> _mur = new List<int>();
                    _mur.Add(_x);
                    _mur.Add(startY);
                    _mur.Add(_x);
                    _mur.Add(endX);
                    listMur.Add(_mur);
                  */

				int doorX = _x;
				int doorY = Random.Range(startY,endY);
				arrayPortesX [doorX, doorY] = true;

                    Divide(startX, startY, _x, endY);
                    Divide( _x, startY, endX, endY);
                }
                else
                {
                    //print(_x + " " + _y);
                    //Debug.Log("Can't Divide more");
                    return;
                }



                //la faut lancer les deux autres divide avec les bonnes coordonnées 

                break;

            case 1: //Try divide right

                if ((startX + _x / 2 - Mathf.Abs(variation) + minSize )< endX && (startX + _x / 2 - Mathf.Abs(variation) - minSize )> startX)
                {
                    _x = _x / 2 + variation + startX;
                    for (int i = startY + 1; i < endY; i++)
                    {
                        arrayColonnes[_x, i] = true;
                    }
					
				/*
                    List<int> _mur = new List<int>();
                    _mur.Add(_x);
                    _mur.Add(startY);
                    _mur.Add(_x);
                    _mur.Add(endX);
                    listMur.Add(_mur);
                   */

					int doorX = _x;
				int doorY =Random.Range(startY,endY);
					arrayPortesX [doorX, doorY] = true;

                    Divide(startX, startY, _x, endY);
                    Divide(_x, startY, endX, endY);

                }
                else if ((startY + _y / 2 - Mathf.Abs(variation) + minSize) < endY &&(startY + _y / 2 - Mathf.Abs(variation) - minSize) > startY)
                {
                    _y = _y / 2 +variation + startY;
                    for (int i = startX + 1; i < endX; i++)
                    {
                        arrayColonnes[i,_y] = true;
                    }

					/*
                    List<int> _mur = new List<int>();
                    _mur.Add(startX);
                    _mur.Add(_y);
                    _mur.Add(endX);
                    _mur.Add(_y);
                    listMur.Add(_mur);
					*/

				int doorX = Random.Range(startX,endX);
				int doorY = _y;
				arrayPortesY [doorX, doorY] = true;

                    Divide(startX, startY, endX, _y);
                    Divide(startX, _y, endX, endY);

                }
                else
                {
                    //Debug.Log("Can't Divide more");
                }

                //relancer les divisions

                break;

            default:

                Debug.Log("Couldn't decide which way to go");

                break;
        }

       
		//coupe à peu près au milieu
		//place des colonnes sur le x de la découpe et les y de la room (ou l'inverse)

	}

    void CreateRooms()
    {
        for (int i = 0; i < x+1; i++)
        {
            for (int o = 0; o < y + 1; o++)
            {

                
                    if (arrayColonnes[i, o])
                    {
                        //print(aColonnes[i, o]);
                        assets.Add(Instantiate(colonne, new Vector3(i, 0, o), colonne.transform.rotation)); // on créer les colonnes


					if(CheckWallX (i,o))
					{
						if(arrayPortesX[i,o])
						{
							DoorX (i, o);
						}
						else
						{
							MurX (i, o);	
						}


					}


					if(CheckWallY (i,o))
					{
						if(arrayPortesY[i,o])
						{
							DoorY (i, o);
						}
						else
						{
							MurY (i, o);	
						}
					}

                    
                    }
                
            }
        }
	}
    

    bool CheckWallY(int _x, int _y)
    {
        if(_x>=origin.x && _x <x && _y>=origin.y && _y<=y)
        {
            if(arrayColonnes[_x+1,_y] ==true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            
            print("Point not in the room");
            return false;
        }
    }

    bool CheckWallX(int _x, int _y)
    {
        if (_x >= origin.x && _x <= x && _y >= origin.y && _y < y)
        {
            if (arrayColonnes[_x, _y+1])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {

            //print("Point not in the room");
            return false;
        }
    }

    void SetDoors()
    {

        print(listMur.Count);
        foreach (List<int> _listMur in listMur)
        {
            /*
            int _x = (int)Random.Range((float)_listMur[0], (float)_listMur[2]);
            int _y = (int)Random.Range((float)_listMur[1], (float)_listMur[3]);
            */
            int _x = (_listMur[0] + _listMur[2]) / 2;
            int _y = (_listMur[1] + _listMur[3]) / 2;

            if(_listMur[0]==_listMur[2])
                arrayPortesX[_x,_y] = true;
            else
                arrayPortesY[_x, _y] = true;
            //print(_x + " "+ _y);
        }
        
    }

    void DoorX(int _x, int _y)
    {
        assets.Add(Instantiate(porte, new Vector3(_x, 0, _y), Quaternion.Euler(new Vector3(0, -90, 0))));


    }

    void DoorY(int _x, int _y)
    {
        assets.Add(Instantiate(porte, new Vector3(_x, 0, _y), colonne.transform.rotation));
    }
}
