using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    private GameManager GameManager;

	// Use this for initialization
	void Start () {
	    GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.name == "Cube")
        {
            collision.gameObject.SetActive(false);
            GameManager.pickUpsCollected++;
        }
        
    }

}
