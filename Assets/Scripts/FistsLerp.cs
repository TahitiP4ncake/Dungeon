using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistsLerp : MonoBehaviour {

    public GameObject target;

    public Rigidbody rb;

    public float lerpSpeed;

    void FixedUpdate () 
	{
        rb.MovePosition(Vector3.Lerp( transform.position, target.transform.position,lerpSpeed));
        rb.MoveRotation ( Quaternion.Slerp(transform.rotation, target.transform.rotation,lerpSpeed));
    }
}
