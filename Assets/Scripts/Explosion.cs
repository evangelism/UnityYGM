//-----------------------------------------
//   Explosion.cs
//
//   Jason Walters
//   http://glitchbeam.com
//   @jasonrwalters
//
//   last edited on 1/23/2015
//-----------------------------------------

using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    public float speedDiff;
    public float destroySeconds;

	// Use this for initialization
	void Start () {

        // destroy object after x seconds
        Destroy(this.gameObject, destroySeconds);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        // add forward force to the Explosion
        rigidbody.velocity = transform.forward * -(speedDiff + GameController.gameSpeed);
	}
}
