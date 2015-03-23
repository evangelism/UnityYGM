//-----------------------------------------
//   Danger.cs
//
//   Jason Walters
//   http://glitchbeam.com
//   @jasonrwalters
//
//   last edited on 3/23/2015
//-----------------------------------------

using UnityEngine;
using System.Collections;

public class Danger : MonoBehaviour 
{
    public float speedDiff;
    public float destroySeconds;
    public float destroyScore;
    public GameObject explosion;
    public AudioClip soundExp;
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
    }

    void Destruction()
    {
        // increase the game score by 1
        GameController.gameScore += destroyScore;

        // play destruction sound
        audioSrc.clip = soundExp;
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
        if (coll.gameObject.tag == "Bullet")
        {
            Destruction();
        }
    }
}
