using UnityEngine;
using System.Collections;

public class Entrance : MonoBehaviour
{

    public bool move = false;
    private Vector3 destination;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (move)
        {
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime);

        }
    }

    void OnTriggerEnter(Collider other)
    {
        move = true;
        destination = transform.position + new Vector3(0, 1.5f, 0);
    }
}
