using UnityEngine;
using System.Collections;

public class Spiral : MonoBehaviour
{

    public bool move = false;
    private Vector3 destination;

    private GameManager gameManager;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

  
    // Update is called once per frame
    void Update()
    {
        if (gameManager.pickUpsCollected == gameManager.pickUps - 1)
        {
            move = true;
            destination = transform.position - new Vector3(0, 1.5f, 0);
        }

     
        if (move)
        {
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.NextLVL();
        }
    }

}
