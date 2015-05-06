using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour {
    public float rotationAngle = 45;
    public bool yAxis;
    public bool xAxis;
    public bool zAxis;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (yAxis)
            transform.Rotate(Vector3.up, rotationAngle * Time.deltaTime);

        if (xAxis)
            transform.Rotate(Vector3.right, rotationAngle * Time.deltaTime);

        if(zAxis)
            transform.Rotate(Vector3.forward, rotationAngle * Time.deltaTime);
	}
}
