using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    Rigidbody sphere;

	// Use this for initialization
	void Start () {
        sphere = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        sphere.AddForce(Input.acceleration.x * 35, 0, -(Input.acceleration.z + 0.5f) * 35);
        
	}
}
