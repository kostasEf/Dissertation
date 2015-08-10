using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

    public Transform ball;

    Vector3 target;
  

	// Use this for initialization
	void Start () 
    {
        ball = GameObject.Find("Sphere").transform;
	}
	
	// Update is called once per frame
    void LateUpdate () 
    {
        transform.position = Vector3.Lerp(transform.position, ball.position + new Vector3(0, 5, -5), Time.deltaTime);
       // transform.position = new Vector3(ball.position.x, ball.position.y + 5, ball.position.z - 5);
       // transform.rotation = Quaternion.Lerp(transform.rotation, ball.transform.rotation, speed);
       // transform.LookAt(ball.position);
        
    }

}
