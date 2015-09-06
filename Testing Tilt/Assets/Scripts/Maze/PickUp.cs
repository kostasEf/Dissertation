using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

    private GameManager gameManager;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.pickUps++;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, Time.deltaTime * 50);
	}
}
