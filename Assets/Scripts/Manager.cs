using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Manager : MonoBehaviour {

	public PlayerController player;

	public RoomGenerator generator;

    public GameObject gameOverText;

	[Space]

	public List<int> x;
	public List<int> y;

	public int stantardSize;

	public int activeLevel;

	void Start()
	{
		LoadLevel ();

        //StartCoroutine(ShakeText());
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
        activeLevel = -1;
        /*
        Scene _scene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(_scene.buildIndex);
        */

        NextLevel();
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

    IEnumerator ShakeText()
    {
        Vector3 _origin = gameOverText.transform.localPosition;

        float range = 5;

        while(true)
        {
            gameOverText.transform.localPosition = new Vector3(_origin.x + Random.Range(-range, range), _origin.y + Random.Range(-range, range), _origin.z);
            yield return null;
        }
    }
}
