//-----------------------------------------
//   Danger.cs
//
//   Jason Walters
//   http://glitchbeam.com
//   @jasonrwalters
//
//   last edited on 1/23/2015
//-----------------------------------------

using UnityEngine;
using System.Collections;

public class Danger : MonoBehaviour 
{
    public float speedDiff;
    public float destroySeconds;
    public float destroyScore;
    public GameObject explosion;

    // Use this for initialization
    void Start()
    {
        // destroy object after x seconds
        Destroy(this.gameObject, destroySeconds);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // add forward force to the object
        rigidbody.velocity = transform.forward * -(speedDiff + GameController.gameSpeed);
    }

    void Destruction()
    {
        // increase the game score by 1
        GameController.gameScore += destroyScore;

        // play destruction sound
        audio.Play();

        // spawn explosion effect
        Instantiate(explosion, transform.position, transform.rotation);

        // disable render and collider
        renderer.enabled = false;
        collider.enabled = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            Destruction();
        }
    }
}
