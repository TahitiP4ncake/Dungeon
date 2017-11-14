using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistPunch : MonoBehaviour {

    bool punch;

    float timer;

    public GameObject player;

	public PlayerController controller;

	public RoomGenerator gen;

	public GameObject cam;

	public GameObject aim1;
	public GameObject aim2;

    public BoxCollider fistCol;
	public BoxCollider headCol;

	float range;

    SoundManager son;

	RaycastHit hit;


    private void Start()
    {
        son = SoundManager.Instance;

		range = controller.range;
        fistCol.enabled = false;
    }

    public void Punch()
    {
        
        punch = true;
        timer = .2f;

        son.Play(son.punchAir);

        fistCol.enabled = true;
		headCol.enabled = true;

        /*
        Vector3 _direction1 = aim1.transform.position - cam.transform.position;
		Vector3 _direction2 = aim2.transform.position - cam.transform.position;
        

		Debug.DrawRay (cam.transform.position,_direction1.normalized*range,Color.white,.5f);
		Debug.DrawRay (cam.transform.position,_direction2.normalized*range,Color.white,.5f);

		if(Physics.Raycast(cam.transform.position,_direction1,out hit, range))
		{
			if(hit.collider.tag=="Breakable")
			{
				hit.collider.gameObject.GetComponent<Destructible>().Break(player);
				son.Play(son.punchWood);
				son.Play(son.kick);

				Invoke("UpdateNavMesh",.5f);
			}
		}
		else if(Physics.Raycast(cam.transform.position,_direction2,out hit, range))
		{
			if(hit.collider.tag=="Breakable")
			{
				hit.collider.gameObject.GetComponent<Destructible>().Break(player);
				son.Play(son.punchWood);
				son.Play(son.kick);

				Invoke("UpdateNavMesh",.5f);
			}
		}
        */
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                fistCol.enabled = false;
				headCol.enabled = false;
                punch = false;
            }
        }

    }
    /*
    void OnCollisionEnter(Collision collision)
    {
       

        if(punch && collision.gameObject.tag=="Breakable")
        {
            collision.gameObject.GetComponent<Destructible>().Break(player);
            son.Play(son.punchWood);
            son.Play(son.kick);

			Invoke("UpdateNavMesh",.5f);
            
        }
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (punch && other.gameObject.tag == "Breakable")
        {
            other.gameObject.GetComponent<Destructible>().Break(player);
            son.Play(son.punchWood);

            StartCoroutine(controller.shaker.Shake(.02f,.2f));

            /*son.Play(son.kick);
            
            if(other.gameObject.GetComponent<SkeletonBehaviour>()==null)
            Invoke("UpdateNavMesh", .2f);
            */
        }
    }

    void UpdateNavMesh()
	{
		StartCoroutine(gen.BakeNavMesh ());
	}
}
