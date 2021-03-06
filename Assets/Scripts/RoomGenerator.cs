﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class RoomGenerator : MonoBehaviour
{

    enum orientation { nord, sud, est, west };

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

    public SkeletonBehaviour skeleton;

    [Space]

    public List<GameObject> assets;

    List<GameObject> solNavMesh;

	List<GameObject> newGround;

	List<NavMeshSurface> groundSurface;

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

    public bool addTorches;

    public bool generateOnStart;

	int layer = 11;
	LayerMask layerMask;

    public GameObject startOfLevel;

	GameObject spawn;

    public GameObject endOfLevel;

	public GameObject player;

    public PlayerController controller;

    [Space]

    public int difficulty;


    void Start()
    {
		/*
        if (generateOnStart)
            DrawRoom();

		PlayerPosition ();
		*/
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

		layerMask = 1 << layer;

        iteration = 0;

        arrayColonnes = new bool[x + 1, y + 1];
        arrayPortesX = new bool[x + 1, y + 1];
        arrayPortesY = new bool[x + 1, y + 1];
		solNavMesh = new List<GameObject> ();
        newGround = new List<GameObject>();
        groundSurface = new List<NavMeshSurface>();
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

                if (bordX || bordY)
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
                    arrayColonnes[i, o] = false;
                }

                if (i != x && o != y)
                    Sol(i, o);

            }
            bordX = false;
        }

        Divide((int)origin.x, (int)origin.y, (int)origin.x + x, (int)origin.z + y);

        //SetDoors();

        CreateRooms();


        SetParent();

		MakeMesh (solNavMesh, true);

		StartCoroutine(BakeNavMesh());

        SetPlayer();

		PlayerPosition ();
            
    }

    void Colonne(int _x, int _y)
    {
        assets.Add(Instantiate(colonne, new Vector3(_x, 0, _y), colonne.transform.rotation));
    }

    void MurY(int _x, int _y)
    {
        int _choice = Random.Range(0, mur.Count);
        assets.Add(Instantiate(mur[_choice], new Vector3(_x, 0, _y), Quaternion.Euler(new Vector3(0, -90, 0))));

    }

    void MurX(int _x, int _y)
    {
        int _choice = Random.Range(0, mur.Count);
        assets.Add(Instantiate(mur[_choice], new Vector3(_x, 0, _y), colonne.transform.rotation));
    }

    void Sol(int _x, int _y)
    {
        int _choice = Random.Range(0, sol.Count);
        GameObject _sol = Instantiate(sol[_choice], new Vector3(_x, 0, _y), Quaternion.Euler(new Vector3(0, -90, 0)));
        assets.Add(_sol);
		solNavMesh.Add (_sol);
        

    }

    public void EraseRoom()
    {
        foreach (GameObject _asset in assets)
        {
            Destroy(_asset);
        }
        assets.Clear();
    }

    void SetParent() //Groupe tout les objets en enfant du générateur
    {
        foreach (GameObject _asset in assets)
        {
            _asset.transform.SetParent(gameObject.transform);
            _asset.isStatic = true;
        }
    }

	void MakeMesh(List<GameObject> _assets, bool isGround) //Unifie les mesh filter d'une liste, 
    {

        //Alors, pour l'instant je suis limité à des carrés de 13/14
        //Au dela de ça le mesh a trop de vertex
        //va falloir que je change ça
        //ptn




        List<int> groundPartition = new List<int>();
        int _temp = _assets.Count;
        
        while (_temp>180)
        {
            _temp -= 180;
            groundPartition.Add(180);
           
        }
        groundPartition.Add(_temp);
        
		/*
        for (int i = 0; i < groundPartition.Count; i++)
        {
            print(groundPartition[i]);
        } 
            
*/
            int firstMesh = 0;
            
        

        for (int p = 0; p < groundPartition.Count; p++) // boucle for pour faire plusieurs mesh (on évite le bug des 64k vertices)
        {
            //print(p);


            MeshFilter[] meshFilters = new MeshFilter[groundPartition[p]];



            for (int o = 0; o < groundPartition[p]; o++)
            {
                meshFilters[o] = _assets[o+firstMesh].GetComponent<MeshFilter>();
            }

            CombineInstance[] combine = new CombineInstance[meshFilters.Length];
            int i = 0;
            while (i < meshFilters.Length)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                meshFilters[i].gameObject.SetActive(false);
                i++;
            }
            _assets[p].transform.GetComponent<MeshFilter>().sharedMesh = new Mesh();
            _assets[p].transform.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine);
            _assets[p].transform.gameObject.SetActive(true);
            _assets[p].transform.localPosition = Vector3.zero;
            _assets[p].transform.localEulerAngles = Vector3.zero;
            Destroy(_assets[p].GetComponent<BoxCollider>());
            _assets[p].AddComponent<BoxCollider>();

            if (isGround)
            {
                newGround.Add( _assets[p]);
                groundSurface.Add( newGround[p].AddComponent<NavMeshSurface>());
                groundSurface[p].layerMask = layerMask;
            }

            firstMesh += 180;
        }
		//_assets [0].transform.GetComponent<MeshCollider> ().sharedMesh = _assets [0].GetComponent<MeshFilter> ().sharedMesh;
    }

    void Divide(int startX, int startY, int endX, int endY)
    {



        int _x = endX - startX;
        int _y = endY - startY;



        direction = Random.Range(0, 2); // on choisit si on découpe en x ou en y en premier



        variation = Random.Range(-variationMax, variationMax + 1);

        switch (direction)
        {
            case 0:  //Try divide up

                if ((startY + _y / 2 - Mathf.Abs(variation) + minSize) < endY && (startY + _y / 2 - Mathf.Abs(variation) - minSize) > startY)
                {

                    iteration += 1;



                    _y = _y / 2 + variation + startY;

                    for (int i = startX + 1; i < endX; i++)
                    {
                        arrayColonnes[i, _y] = true;
                    }

                    int doorX = Random.Range(startX, endX);
                    int doorY = _y;
                    arrayPortesY[doorX, doorY] = true;

                    Divide(startX, startY, endX, _y);

                    Divide(startX, _y, endX, endY);


                }
                else if ((startX + _x / 2 - Mathf.Abs(variation) + minSize) < endX && (startX + _x / 2 - Mathf.Abs(variation) - minSize) > startX)
                {

                    iteration += 1;

                    _x = _x / 2 + variation + startX;

                    for (int i = startY + 1; i < endY; i++)
                    {
                        arrayColonnes[_x, i] = true;
                    }



                    int doorX = _x;
                    int doorY = Random.Range(startY, endY);
                    arrayPortesX[doorX, doorY] = true;

                    Divide(startX, startY, _x, endY);
                    Divide(_x, startY, endX, endY);
                }
                else
                {
                    if (startX > 3 || startY > 3) //Je vérifie que ce n'est pas la salle de départ
                    {
                        
                        int _choice = Random.Range(0, 4);
                        if (_choice <= difficulty)          //Je check en fonction de la difficulté
                        {
                            for (int i = startX; i < endX; i++)
                            {
                                for (int o = startY; o < endY; o++)
                                {
                                    _choice = Random.Range(0, 4);
                                    if (_choice <= difficulty)
                                    {
                                        GameObject _skeleton = Instantiate(skeleton.gameObject, new Vector3(i + .5f, 0, o + .5f), Quaternion.Euler(new Vector3(0, Random.Range(-90, 90), 0)));
                                        SkeletonBehaviour _skeletonBehaviour = _skeleton.GetComponent<SkeletonBehaviour>();
                                        _skeletonBehaviour.player = player;
                                        assets.Add(_skeleton);
                                    }
                                }
                            }
                        }
                    }
                    return;
                }



                //la faut lancer les deux autres divide avec les bonnes coordonnées 

                break;

            case 1: //Try divide right

                if ((startX + _x / 2 - Mathf.Abs(variation) + minSize) < endX && (startX + _x / 2 - Mathf.Abs(variation) - minSize) > startX)
                {
                    _x = _x / 2 + variation + startX;
                    for (int i = startY + 1; i < endY; i++)
                    {
                        arrayColonnes[_x, i] = true;
                    }



                    int doorX = _x;
                    int doorY = Random.Range(startY, endY);
                    arrayPortesX[doorX, doorY] = true;

                    Divide(startX, startY, _x, endY);
                    Divide(_x, startY, endX, endY);

                }
                else if ((startY + _y / 2 - Mathf.Abs(variation) + minSize) < endY && (startY + _y / 2 - Mathf.Abs(variation) - minSize) > startY)
                {
                    _y = _y / 2 + variation + startY;
                    for (int i = startX + 1; i < endX; i++)
                    {
                        arrayColonnes[i, _y] = true;
                    }



                    int doorX = Random.Range(startX, endX);
                    int doorY = _y;
                    arrayPortesY[doorX, doorY] = true;

                    Divide(startX, startY, endX, _y);
                    Divide(startX, _y, endX, endY);

                }
                else
                {
                    if (startX > 3 || startY > 3)
                    {

                        int _choice = Random.Range(0, 4);
                        if (_choice <= difficulty)
                        {
                            for (int i = startX; i < endX; i++)
                            {
                                for (int o = startY; o < endY; o++)
                                {
                                    _choice = Random.Range(0, 4);
                                    if (_choice <= difficulty)
                                    {
                                        GameObject _skeleton = Instantiate(skeleton.gameObject, new Vector3(i + .5f, 0, o + .5f), Quaternion.Euler(new Vector3(0, Random.Range(-90, 90), 0)));
                                        SkeletonBehaviour _skeletonBehaviour = _skeleton.GetComponent<SkeletonBehaviour>();
                                        _skeletonBehaviour.player = player;
                                        assets.Add(_skeleton);
                                    }
                                }
                            }
                        }
                    }
                    return;

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
        for (int i = 0; i < x + 1; i++)
        {
            for (int o = 0; o < y + 1; o++)
            {


                if (arrayColonnes[i, o])
                {

                    assets.Add(Instantiate(colonne, new Vector3(i, 0, o), colonne.transform.rotation)); // on créer les colonnes


                    if (CheckWallY(i, o)) //on vérifie si on doit créer un mur
                    {
                        if (arrayPortesX[i, o]) //on vérifie si no doit créer une porte 
                        {
                            DoorY(i, o);

                            if (!CheckWallX(i, o))  //Si on créer une porte, on essaye de créer des torches
                                SetTorche(orientation.est, i, o);
                            if (!CheckBackWallX(i, o))
                                SetTorche(orientation.west, i, o);

                            if (!CheckWallX(i, o + 1))
                                SetTorche(orientation.west, i, o + 1);
                            if (!CheckBackWallX(i, o + 1))
                                SetTorche(orientation.est, i, o+1);


                        }
                        else
                        {
                            MurY(i, o);	//Si pas de porte, on met un mur

                            if (addTorches)
                            {

                                int _choice = Random.Range(0, 4);
                                if (_choice == 0)
                                {
                                    if (!CheckWallY(i, o))
                                        SetTorche(orientation.west, i, o);
                                    if (!CheckBackWallY(i, o))
                                        SetTorche(orientation.est, i, o);
                                }
                            }
                        }


                    }


                    if (CheckWallX(i, o))
                    {
                        if (arrayPortesY[i, o])
                        {
                            DoorX(i, o);

                            if (!CheckWallY(i, o))
                                SetTorche(orientation.nord, i, o);
                            if (!CheckBackWallY(i, o))
                                SetTorche(orientation.sud, i, o);

                            if (!CheckWallY(i + 1, o))
                                SetTorche(orientation.nord, i + 1, o);
                            if (!CheckBackWallY(i + 1, o))
                                SetTorche(orientation.sud, i + 1, o);

                        }
                        else
                        {
                            MurX(i, o);

                            if (addTorches)
                            {

                                int _choice = Random.Range(0, 4);
                                if (_choice == 0)
                                {
                                    if (!CheckWallX(i, o))
                                        SetTorche(orientation.nord, i, o);
                                    if (!CheckBackWallX(i, o))
                                        SetTorche(orientation.sud, i, o);
                                }
                            }
                        }
                    }


                }

            }
        }
    }

    #region CheckWalls

    bool CheckWallX(int _x, int _y)
    {
        if (_x >= origin.x && _x < x && _y >= origin.y && _y <= y)
        {
            if (arrayColonnes[_x + 1, _y] == true)
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

    bool CheckWallY(int _x, int _y)
    {
        if (_x >= origin.x && _x <= x && _y >= origin.y && _y < y)
        {
            if (arrayColonnes[_x, _y + 1])
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

    bool CheckBackWallY(int _x, int _y)
    {
        if (_x >= origin.x && _x <= x && _y > origin.y && _y <= y)
        {
            if (arrayColonnes[_x, _y - 1])
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

    bool CheckBackWallX(int _x, int _y)
    {
        if (_x > origin.x && _x <= x && _y >= origin.y && _y <= y)
        {
            if (arrayColonnes[_x - 1, _y] == true)
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

    #endregion

    void SetDoors() // je crois que je n'utilise plus cette fonction
    {               // elle sevrait à placer les portes mais j'ai utilisé un moyen plus direct

        print(listMur.Count);
        foreach (List<int> _listMur in listMur)
        {

            int _x = (_listMur[0] + _listMur[2]) / 2;
            int _y = (_listMur[1] + _listMur[3]) / 2;

            if (_listMur[0] == _listMur[2])
                arrayPortesX[_x, _y] = true;
            else
                arrayPortesY[_x, _y] = true;

        }

    }

    void DoorY(int _x, int _y)
    {
        assets.Add(Instantiate(porte, new Vector3(_x, 0, _y), Quaternion.Euler(new Vector3(0, -90, 0))));
        //Instantiate(porte, new Vector3(_x, 0, _y), Quaternion.Euler(new Vector3(0, -90, 0)));
    }

    void DoorX(int _x, int _y)
    {
        assets.Add(Instantiate(porte, new Vector3(_x, 0, _y), colonne.transform.rotation));
        //Instantiate(porte, new Vector3(_x, 0, _y), colonne.transform.rotation);
    }

    void SetTorche(orientation _direction, int _x, int _y)
    {
        switch (_direction)
        {
            case orientation.west:
                assets.Add(Instantiate(torche, new Vector3(_x, 0, _y), Quaternion.Euler(new Vector3(0, -90, 0))));
                break;
            case orientation.est:
                assets.Add(Instantiate(torche, new Vector3(_x, 0, _y), Quaternion.Euler(new Vector3(0, 90, 0))));
                break;
            case orientation.sud:
                assets.Add(Instantiate(torche, new Vector3(_x, 0, _y), colonne.transform.rotation));
                break;
            case orientation.nord:
                assets.Add(Instantiate(torche, new Vector3(_x, 0, _y), Quaternion.Euler(new Vector3(0, 180, 0))));
                break;
        }
    }

    public IEnumerator BakeNavMesh() //Je bake/rebake le nav mesh agent pour permettre aux squelettes de passer les portes cassées
    {
        //print("on casse des portes");
		yield return null;

        foreach (NavMeshSurface _surface in groundSurface)
        {
            _surface.BuildNavMesh();
            yield return new WaitForSeconds(.1f);
        }
        
		
		//ça marche à runtim quand je regénère la room
		//je fais spawner les squelettes après la génération du navmesh
    } 

    void SetPlayer()
    {
		spawn = Instantiate(startOfLevel, new Vector3(origin.x + .5f, origin.y, origin.z + .5f), startOfLevel.transform.rotation); //Set le spawn à cet endroit làn faut bouger le player dessus
		assets.Add(spawn);

        assets.Add(Instantiate(endOfLevel, new Vector3(origin.x + Random.Range(x - x / 2, x)+.5f, origin.y, origin.z +  Random.Range(y - y / 2, y)+.5f), endOfLevel.transform.rotation));
    }

	public void PlayerPosition()
	{
		player.transform.position = spawn.transform.position+Vector3.up;

		player.transform.rotation = spawn.transform.rotation;
	}
}
