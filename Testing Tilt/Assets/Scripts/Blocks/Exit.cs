using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {

    public bool move = false;
    private Vector3 destination; 

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

        
        if (move)
	    {
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime);

            if (transform.localPosition.y < -0.4)
            {
                Destroy(gameObject);
            }
	    }

        
        
	}

    void OnTriggerEnter(Collider other)
    {
        move = true;
        destination = transform.position - new Vector3(0, 2.5f, 0);
    }
}
