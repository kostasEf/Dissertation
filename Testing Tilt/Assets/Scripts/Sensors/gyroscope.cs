using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class gyroscope : MonoBehaviour {

    private float velocity;
    private Vector3 acceleration;

    private Text text1, text2, text3;

    private Rigidbody sphere;

    Vector3 pos;

    private float min = 0, max = 0;

    void Start()
    {
        sphere = GetComponent<Rigidbody>();

        pos = transform.position;

        Input.gyro.enabled = true;
        Screen.autorotateToLandscapeLeft = true;

        Screen.autorotateToLandscapeRight = true;

        Screen.autorotateToPortrait = false;

        Screen.autorotateToPortraitUpsideDown = false;

        Screen.orientation = ScreenOrientation.AutoRotation;

        text1 = GameObject.Find("Text1").GetComponent<Text>();
        text2 = GameObject.Find("Text2").GetComponent<Text>();
        text3 = GameObject.Find("Text3").GetComponent<Text>();
    }

    void Update()
    {
        acceleration = Input.gyro.userAcceleration;
        //if (Mathf.Abs(acceleration) < 0.015) acceleration = 0;
        //if (acceleration > 0) acceleration = 0;

        //velocity += acceleration * 10f * Time.deltaTime;
        //sphere.AddForce(Input.gyro.userAcceleration.x, 
        //                0, 
        //                Input.gyro.userAcceleration.z);

        transform.position = new Vector3(pos.x + Input.gyro.userAcceleration.x * 100 * Time.deltaTime,
            transform.position.y,
            pos.z + Input.gyro.userAcceleration.y * 100 * Time.deltaTime);

        //if (Input.gyro.userAcceleration.x > 0 && Mathf.Abs(acceleration) > 1)
        //{ text1.text = "positive"; }
        //else if (Input.gyro.userAcceleration.x < 0 && Mathf.Abs(acceleration) > 1)
        //{ text1.text = "negative"; }
        //else { text1.text = "nope"; }

        if (acceleration.z < 0 /*&& acceleration.z < min */) min = acceleration.z;
        if (acceleration.z > 0 /*&& acceleration.z > max */) max = acceleration.z;

        text1.text = min.ToString();
        text2.text = max.ToString();
        //text3.text = Input.gyro.userAcceleration.z.ToString();


    }
   
}
