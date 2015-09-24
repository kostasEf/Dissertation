using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PickUp : MonoBehaviour {

    private GameManager gameManager;
    public Text pickUps;

	// Use this for initialization
	void Start () {
        pickUps = GameObject.Find("PickUpNo").GetComponent<Text>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.pickUps++;
        pickUps.text = gameManager.pickUps.ToString();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Rotate(Vector3.up, Time.deltaTime * 70);
	}

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.pickUpsCollected++;
            Destroy(gameObject);
            gameManager.timer += 15;
            pickUps.text = (gameManager.pickUps - gameManager.pickUpsCollected).ToString();
        }

    }



}
