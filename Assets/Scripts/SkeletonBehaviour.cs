using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonBehaviour : MonoBehaviour {

    enum AnimationState { Idle,Attack,Walk};

    public GameObject player;

    public NavMeshAgent nav;

    public bool alive;

    public Animator anim;

    public bool following;

    public bool attacking;

    Vector3 direction;

    int layer = 11;
    LayerMask layerMask;

    RaycastHit hit;

    public float distanceToHit;

    public Collider col;

    private void Start()
    {
        Invoke("StartSkeleton", .5f);
        
    }

    void StartSkeleton()
    {
        StartCoroutine(Idle());
    }

    IEnumerator CheckPlayer()
    {
        while(alive)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > 5)
                break;

            if (!following) //Si il n'a pas repéré le joueur
            {
                direction = player.transform.position - transform.position;
                if (Physics.Raycast(transform.position, direction.normalized*10, out hit))
                {
                    if (hit.collider.tag == "Player")
                    {
                        following = true;
                        PlayAnimation(AnimationState.Walk);
                        nav.destination = player.transform.position;
                    }
                }
            }
            else
            {
                if (!attacking)
                {
                    nav.destination = player.transform.position; //Update position joueur

                    if (Vector3.Distance(transform.position, player.transform.position) < distanceToHit)
                    {
                        attacking = true;
                        nav.isStopped = true;
                        PlayAnimation(AnimationState.Attack);
                    }
                }
                
                //check distance pour mettre des patate
            }
            

            yield return new WaitForSeconds(.1f);
        }


        StartCoroutine(Idle());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Player")
        {
            player.GetComponent<PlayerController>().Hurt();
        }
    }

    void PlayAnimation(AnimationState _state)
    {
        anim.SetTrigger(_state.ToString());
    }

    public void Hit()
    {
        col.enabled = false;
    }

    public void ArmOn()
    {
        col.enabled = true;
        attacking = false;
        nav.isStopped = false;
        
    }

    IEnumerator Idle()
    {
        while(Vector3.Distance(transform.position,player.transform.position)>5)
        {

            yield return new WaitForSecondsRealtime(.2f);
        }

        StartCoroutine(CheckPlayer());
    }
}
