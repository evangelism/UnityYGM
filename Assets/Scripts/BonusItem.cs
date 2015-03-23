//-----------------------------------------
//   BonusItem.cs
//
//   Jason Walters
//   http://glitchbeam.com
//   @jasonrwalters
//
//   last edited on 3/23/2015
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
    public float volume;

    private Rigidbody rigid;
    private AudioSource audioSrc;
    private Renderer rend;
    private Collider col;

    // Use this for initialization
    void Start()
    {
        // cache components
        rigid = GetComponent<Rigidbody>();
        audioSrc = GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();

        // destroy object after x seconds
        Destroy(this.gameObject, destroySeconds);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // add forward force to the object
        rigid.velocity = transform.forward * -(speedDiff + GameController.gameSpeed);

        // create a hover effect using a sin wave
        float bounceY = rigid.position.y + bounce * Mathf.Sin(bounceSpeed * Time.time);

        // apply hover effect to this game object
        rigid.position = new Vector3(rigid.position.x, bounceY, rigid.position.z);
    }

    void Destruction()
    {
        // play destruction sonud
        audioSrc.volume = volume;
        audioSrc.Play();

        // spawn explosion effect
        Instantiate(explosion, transform.position, transform.rotation);

        // disable render and collider
        rend.enabled = false;
        col.enabled = false;
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            // play clip for player collision
            audioSrc.clip = player;

            // when player collects, award by
            // increasing the game score by 10 points
            GameController.gameScore += 10.0f;

            // run desctuion function
            Destruction();
        }

        if (coll.gameObject.tag == "Bullet")
        {
            // play clip for bullet collision
            audioSrc.clip = bullet;

            // when bonus is shot, penalize the player by
            // decreasing the game score by 10 points
            GameController.gameScore -= 10.0f;

            // run desctuion function
            Destruction();
        }
    }
}