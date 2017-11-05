using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistPunch : MonoBehaviour {

    bool punch;

    float timer;

    public GameObject player;

    SoundManager son;

    private void Start()
    {
        son = SoundManager.Instance;
    }

    public void Punch()
    {
        
        punch = true;
        timer = .1f;
        son.Play(son.punchAir);
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                punch = false;
            }
        }

    }

    void OnCollisionEnter(Collision collision)
    {
       

        if(punch && collision.gameObject.tag=="Breakable")
        {
            collision.gameObject.GetComponent<Destructible>().Break(player);
            son.Play(son.punchWood);
            son.Play(son.kick);
            
        }
    }
}
