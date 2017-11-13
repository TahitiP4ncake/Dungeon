using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

    public List<GameObject> broken;

    public float force;

	public bool skeleton;

    public void Break(GameObject _player)
    {
		if (skeleton) 
		{
			GetComponent<Collider> ().enabled = false;
			foreach (GameObject _part in broken) {
				_part.SetActive (true);
				_part.transform.SetParent (null);
				_part.GetComponent<Rigidbody> ().AddForce ((_player.transform.forward).normalized * force * Random.Range (.9f, 1.5f), ForceMode.VelocityChange);
				print ("cassé");
			}

		} 

		else 
		{

			foreach (GameObject _part in broken) {
				GameObject _brokenPart = Instantiate (_part, transform.position, transform.rotation);
				_brokenPart.GetComponent<Rigidbody> ().AddForce ((_player.transform.forward).normalized * force * Random.Range (.9f, 1.5f), ForceMode.VelocityChange);
			}


		}

        Destroy(gameObject);
    }
}
