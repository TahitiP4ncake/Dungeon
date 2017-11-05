using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

	
	
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Backspace))
        {
            Scene _scene = SceneManager.GetActiveScene();
            SceneManager.LoadSceneAsync(_scene.buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
