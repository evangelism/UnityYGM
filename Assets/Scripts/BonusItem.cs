//-----------------------------------------
//   BonusItem.cs
//
//   Jason Walters
//   http://glitchbeam.com
//   @jasonrwalters
//
//   last edited on 1/23/2015
//-----------------------------------------

using UnityEngine;
using System.Collections;

public class BonusItem : MonoBehaviour 
{
    public float speedDiff;
    public float bounce;
    public float bounceSpeed;
    public float destroySeconds;
    public GameObject explosion;

    public AudioClip player;
    public AudioClip bullet;

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

        // create a hover effect using a sin wave
        float bounceY = rigidbody.position.y + bounce * Mathf.Sin(bounceSpeed * Time.time);

        // apply hover effect to this game object
        rigidbody.position = new Vector3(rigidbody.position.x, bounceY, rigidbody.position.z);
    }

    void Destruction()
    {
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
        if (col.gameObject.tag == "Player")
        {
            // play clip for player collision
            audio.clip = player;

            // when player collects, award by
            // increasing the game score by 10 points
            GameController.gameScore += 10.0f;

            // run desctuion function
            Destruction();
        }

        if (col.gameObject.tag == "Bullet")
        {
            // play clip for bullet collision
            audio.clip = bullet;

            // when bonus is shot, penalize the player by
            // decreasing the game score by 10 points
            GameController.gameScore -= 10.0f;

            // run desctuion function
            Destruction();
        }
    }
}
