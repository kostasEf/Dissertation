using UnityEngine;
using System.Collections;

public class Spiral : MonoBehaviour
{

    public bool move = false;
    private Vector3 destination;

    private GameManager GameManager;

    // Use this for initialization
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

  
    // Update is called once per frame
    void Update()
    {
        if (GameManager.itemsPickedUp == GameManager.totalRoomNumber)
        {
            move = true;
            destination = transform.position - new Vector3(0, 1.5f, 0);
        }

     
        if (move)
        {
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime);

            if (transform.localPosition.y < -0.4)
            {
                Destroy(gameObject);
            }
        }
    }

}
