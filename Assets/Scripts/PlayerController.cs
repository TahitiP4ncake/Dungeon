using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Variables

	public RoomGenerator gen;

    public Manager manager;

    public GameObject cam;

    public CameraShake shaker;

    public GameObject head;

    public GameObject cameraLerper;

    public GameObject rightFist;
    public GameObject leftFist;

	public bool controls;

    float camX;
    float camY;

    public float ySpeed;
    public float xSpeed;

    public float minY;
    public float maxY;

    [Space]

    public float speed;

    float x;
    float y;

    Vector3 movement;

    public float lerpSpeed;

    public Rigidbody rb;

    [Space]

    public float tiltAngle;

    [Space]

    public Animator left;
    public Animator right;

    public FistPunch punch1;
    public FistPunch punch2;

    bool punched1;
    bool punched2;

    public float punchCooldown;

	public float range;

    [Space]

    public GameObject cam1;
    public GameObject cam2;

	[Space]

	public ParticleSystem transition;

    public PostEffect effect;
    public Material mat1;
    public Material mat2;

    #endregion

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

		Setup ();

    }

	void Setup()
	{
		controls = true;

		cameraLerper.transform.SetParent(null);
		rightFist.transform.SetParent(null);
		leftFist.transform.SetParent(null);
	}


    void Update () 
	{
		if(controls)
        CheckInputs();
	}

    void CheckInputs()
    {
        camX = Input.GetAxis("Mouse X");
        camY = Input.GetAxis("Mouse Y");

        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        Look();

        
        head.transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(head.transform.localEulerAngles.z, x * tiltAngle, Time.deltaTime * 5));

        if (Input.GetMouseButtonDown(0) && !punched1)
        {
            left.SetTrigger("Punch");
            rb.AddForce(transform.forward * speed, ForceMode.VelocityChange); //petit dash quand on punch ?
            punch1.Punch();
            punched1 = true;
            Invoke("PunchBack1", punchCooldown);
        }
        if (Input.GetMouseButtonDown(1) && !punched2)
        {
            right.SetTrigger("Punch");
            rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
            punch2.Punch();
            punched2 = true;
            Invoke("PunchBack2", punchCooldown);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            SwitchCamera();
        }

    }

    void FixedUpdate()
    {
        Move();

    }

    void Look()
    {
        transform.Rotate(transform.up, camX * xSpeed);
        cam.transform.Rotate(Vector3.right, -camY * ySpeed,Space.Self);
        
        //ça evite que la camera fasse des tours sur elle même vers le haut/bas

        Vector3 currentRotation = cam.transform.localRotation.eulerAngles;

        float angle = cam.transform.localEulerAngles.x;
        angle = (angle > 180) ? angle - 360 : angle;

        currentRotation.x = Mathf.Clamp(angle, minY, maxY);

        cam.transform.localRotation = Quaternion.Euler(currentRotation);
    }

    void Move()
    {
        movement = transform.forward * y + transform.right * x;
        movement = movement.normalized * speed;

        rb.velocity = Vector3.Lerp(rb.velocity, movement, Time.deltaTime*lerpSpeed); // a tweaker pour le feeling du poids
    }

    void SwitchCamera()
    {
        switch(cam1.activeSelf)
        {
            case true:
                cam1.SetActive(false);
                cam2.SetActive(true);
                break;
            case false:
                cam2.SetActive(false);
                cam1.SetActive(true);
                break;
        }
    }

    public void Die()
    {

        StartCoroutine(shaker.Shake(0.05f,.2f));
        effect.effect = mat2;
        Invoke("SetEffect", .2f);
        //manager.Restart();
    }

	void OnTriggerEnter(Collider other)
	{
		if(other.tag=="EndOfLevel")
		{

			cameraLerper.transform.SetParent(gameObject.transform);
			rightFist.transform.SetParent(gameObject.transform);
			leftFist.transform.SetParent(gameObject.transform);

			//transition.gameObject.SetActive (true);
			transition.Play ();

			other.gameObject.SetActive (false);
			Invoke ("NextLevel", 1.75f);

            StartCoroutine(shaker.Shake(.01f, 3));

            Invoke("StopParticles", 3);
		}
	}

	void NextLevel()
	{
		manager.NextLevel ();
		Setup ();
	}

    void StopParticles()
    {
        transition.Stop();
    }

    void SetEffect()
    {
        effect.effect = mat1;
    }

    void PunchBack1()
    {
        punched1 = false;
    }

    void PunchBack2()
    {
        punched2 = false;
    }

}
