//-----------------------------------------
//   SpawnObjects.cs
//
//   Jason Walters
//   http://glitchbeam.com
//   @jasonrwalters
//
//   last edited on 1/23/2015
//-----------------------------------------

using UnityEngine;
using System.Collections;

public class SpawnObjects : MonoBehaviour
{
    public GameObject[] danger;
    public float fireRate;
    public float spawnWidth;

    private float seconds;
	
	// Update is called once per frame
	void Update () 
    {
        // instantiates our danger objects
        Spawn(fireRate);
	}

    void Spawn(float rate)
    {
        // timer, adds up the delta time for seconds
        seconds += Time.deltaTime;
        // if seconds great than our rate as defined in the inspector
        if (seconds > rate)
        {
            // random x position
            float randomX = Random.Range(-spawnWidth, spawnWidth);
            Vector3 position = new Vector3
                (
                randomX,
                transform.position.y,
                transform.position.z
                );

            // randomly select an object in the danger array
            int randomObj = Random.Range(0, danger.Length);
            Instantiate(danger[randomObj], position, transform.rotation);

            // zero out the seconds variable
            seconds = 0;
        }
    }
}
