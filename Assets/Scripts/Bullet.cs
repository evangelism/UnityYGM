//-----------------------------------------
//   Bullet.cs
//
//   Jason Walters
//   http://glitchbeam.com
//   @jasonrwalters
//
//   last edited on 3/23/2015
//-----------------------------------------

using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
    public float speedDiff;
    public float destroySeconds;

    private Rigidbody rigid;

	// Use this for initialization
	void Start () 
    {
        // cache components
        rigid = GetComponent<Rigidbody>();

        // destroy object after x seconds
        Destroy(this.gameObject, destroySeconds);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        // add forward force to the bullet
        rigid.velocity = transform.forward * (speedDiff + GameController.gameSpeed);
	}

    void OnTriggerEnter(Collider coll)
    {
        // if bullet collides with anything, destroy it
        Destroy(this.gameObject);
    }
}
