using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

    public List<GameObject> broken;

    public float force;

    public void Break(GameObject _player)
    {
        foreach (GameObject _part in broken)
        {
            GameObject _brokenPart = Instantiate(_part, transform.position, transform.rotation);
            _brokenPart.GetComponent<Rigidbody>().AddForce((_player.transform.forward).normalized* force*Random.Range(.9f,1.5f), ForceMode.VelocityChange);
        }

        Destroy(gameObject);
    }
}
