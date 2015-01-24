//-----------------------------------------
//   Bullet.cs
//
//   Jason Walters
//   http://glitchbeam.com
//   @jasonrwalters
//
//   last edited on 1/23/2015
//-----------------------------------------

using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
    public float speedDiff;
    public float destroySeconds;

	// Use this for initialization
	void Start () 
    {
        // play bullet sound
        audio.Play();

        // destroy object after x seconds
        Destroy(this.gameObject, destroySeconds);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        // add forward force to the bullet
        rigidbody.velocity = transform.forward * (speedDiff + GameController.gameSpeed);
	}

    void OnTriggerEnter(Collider col)
    {
        // if bullet collides with anything, destroy it
        Destroy(this.gameObject);
    }
}
