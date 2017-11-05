using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour {

    public AudioSource son;

    public AudioClip punchWood;
    public AudioClip punchAir;
    public AudioClip kick;

    private static SoundManager singleton;

    private void Awake()
    {



        if (singleton != null && singleton != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
     
            singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public static SoundManager Instance
{
    get
    {
        if (singleton == null)
        {
            Debug.LogError("[SoundManager]: Instance does not exist!");
            return null;
        }

        return singleton;
    }
}

public void Play(AudioClip _son)
    {
        son.pitch = Random.Range(.9f, 1.1f);
        son.PlayOneShot(_son);
    }
	
	
}
