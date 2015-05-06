//-----------------------------------------
//   PlayerController.cs
//
//   Jason Walters
//   http://glitchbeam.com
//   @jasonrwalters
//
//   Modified from PlayerController.cs by
//   Dmitry Soshnikov
//   @shwars
//-----------------------------------------

using UnityEngine;
using System.Collections;

public class PlayerControllerSimple : MonoBehaviour
{
	public bool invertYAxis;
	public float boundsWidth;
	public float boundsHeight;
	public float speed;
	public float tilt;
	public float bounce;
	public float bounceSpeed;
	
	public GameObject bullet;
	public AudioClip audioBullet;
	public float fireDistance;
	public GameObject explosion;
	public AudioClip audioExplosion;
	public float volume = 0.1f;
	
	public bool InPlay = true;
	
	private Vector3 defaultPos;
	private float inputHorizontal;
	private float inputVertical;
	private float seconds;
	
	private Rigidbody rigid;
	private AudioSource audioSrc;
	private Renderer rend;
	private Collider col;
	
	private GameObject head;
	
	void Start ()
	{
		// cache components
		rigid = GetComponent<Rigidbody>();
		audioSrc = GetComponent<AudioSource>();
		rend = GetComponent<Renderer>();
		col = GetComponent<Collider>();
		
		head = GameObject.Find ("ALPSHead");
		
		// fetch default position from our inspector
		defaultPos = transform.position;
	}
	
	void Update ()
	{
		// grabs input in update loop for best accuracy
		PlayerInput();
		
		if (InPlay)
		{
			// disable renderer and collider
			if (!rend.enabled) rend.enabled = true;
			if (!col.enabled) col.enabled = true;
			
			// fire button triggers bullets (space bar)
			if (Input.GetButtonDown("Jump"))
			{
				// handles bullet firing
				Fire(fireDistance);
			}
		}
		else
		{
			// disable renderer and collider
			if (rend.enabled) rend.enabled = false;
			if (col.enabled) col.enabled = false;
			// reset player position to default
			transform.position = defaultPos;
		}
	}
	
	void FixedUpdate ()
	{
		// run movement function to handle rigidbody physics
		Movement();
	}
	
	float norm(float angle)
	{
		if (angle > 180.0f)
			angle -= 360.0f;
		return angle / 180.0f;
	}
	
	void PlayerInput()
	{
		// fetch our input for movememnt
		inputHorizontal = Input.GetAxis("Horizontal");
		inputVertical = Input.GetAxis("Vertical");

		if (invertYAxis)
		{
			inputVertical *= -1;
		}
	}
	
	void Movement()
	{
		// update player velocity
		Vector3 input = new Vector3(inputHorizontal, inputVertical, 0.0f);
		rigid.velocity = input * speed;
		
		// create a hover effect using a sin wave
		float bounceY = rigid.position.y + bounce * Mathf.Sin(bounceSpeed * Time.time);
		
		// apply hover effect, and clamp player within bounds
		rigid.position = new Vector3(Mathf.Clamp(rigid.position.x, -boundsWidth, boundsWidth),
		                             Mathf.Clamp(bounceY, 0, boundsHeight),
		                             rigid.position.z);
		
		// apply tilt effect to our rotation
		float tiltX = rigid.velocity.y * -tilt;
		float tiltZ = rigid.velocity.x * -tilt;
		rigid.rotation = Quaternion.Euler(tiltX, 0.0f, tiltZ);
	}
	
	void Fire (float distance)
	{
		// play bullet-fire sound
		audioSrc.clip = audioBullet;
		audioSrc.volume = volume * 0.5f; // multiplying it by 50% because effect iss still too loud...
		audioSrc.Play();
		
		// define left firing position, add distance to spawn ahead of player
		Vector3 fireFromLeft = new Vector3(transform.position.x - distance,
		                                   transform.position.y,
		                                   transform.position.z + distance);
		
		// define right firing position, add distance to spawn ahead of player
		Vector3 fireFromRight = new Vector3(transform.position.x + distance,
		                                    transform.position.y,
		                                    transform.position.z + distance);
		
		// spawn 2 bullets
		Instantiate(bullet, fireFromLeft, transform.rotation);
		Instantiate(bullet, fireFromRight, transform.rotation);
	}
	
	void Destruction()
	{
		// play destruction sound
		audioSrc.clip = audioExplosion;
		audioSrc.volume = volume;
		audioSrc.Play();
		
		// spawn an explosion effect
		Instantiate(explosion, transform.position, transform.rotation);
		
		// change game state to game over
		GameControllerSimple.UpdateScore ();
		
	}
	
	void OnTriggerEnter(Collider coll)
	{
		// if player collides with a danger object...
		if (coll.gameObject.tag == "Danger")
		{
			// run destruction function
			Destruction();
		}
	}
}
