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
                    Colonne(i,o);

                    if (bordX && o<y)
                    
                        MurX(i, o);
                    if (bordY && i<x)
                        MurY(i, o);
                    
                    bordY = false;
                }

                if(i!=x && o!=y)
                Sol(i, o);

            }
            bordX = false;
        }
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
}
