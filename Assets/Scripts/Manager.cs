using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

	public PlayerController player;

	public RoomGenerator generator;

	[Space]

	public List<int> x;
	public List<int> y;

	public int stantardSize;

	public int activeLevel;

	void Start()
	{
		LoadLevel ();
	}

	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Backspace))
        {
            Restart();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


    }

    public void Restart()
    {
        Scene _scene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(_scene.buildIndex);
    }

	public void NextLevel()
	{
		player.controls = false;
		activeLevel++;
		LoadLevel ();
		player.controls = true;

	}

	void LoadLevel()
	{

		if (activeLevel < x.Count) {
			generator.x = x [activeLevel];
			generator.y = y [activeLevel];
		}
		else
		{
			generator.x = 	Random.Range(stantardSize-5,stantardSize+5);
			generator.y =	Random.Range(stantardSize-5,stantardSize+5);
		}

		generator.DrawRoom ();
	}
}
