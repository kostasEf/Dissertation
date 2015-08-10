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

    bool slideLeft = false;
    bool slideRight = false;
    float speed = 0;

    void Start()
    {
        sphere = GetComponent<Rigidbody>();

        pos = transform.position;

        Input.gyro.enabled = true;
        Screen.autorotateToLandscapeLeft = false;

        Screen.autorotateToLandscapeRight = false;

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

        //push();
        //Slide();
        Roll();

        

       

        text1.text = Input.acceleration.z.ToString();
        //text2.text = Input.gyro.userAcceleration.z.ToString();
        //text3.text = Input.gyro.userAcceleration.z.ToString();


    }
    //I sfera stamataei meta tin kinisi
    private void push()
    {
        if (Mathf.Abs(acceleration.x) > 0.045f && Input.acceleration.x > 0.4)
        {
            transform.position += new Vector3(Mathf.Abs(acceleration.x) * 2, 0, 0);
        }
        else if (Mathf.Abs(acceleration.x) > 0.045f && Input.acceleration.x < -0.4)
        {
            transform.position += new Vector3(-Mathf.Abs(acceleration.x) * 2, 0, 0);
        }
    }

    private void Slide()
    {

        
        if (Mathf.Abs(acceleration.x) > 0.045f && Input.acceleration.x > 0.4)
        {
            slideRight = false;
            slideLeft = true;
            speed = Mathf.Abs(acceleration.x);
        }
        else if (Mathf.Abs(acceleration.x) > 0.045f && Input.acceleration.x < -0.4)
        {
            slideLeft = false;
            slideRight = true;
            speed = Mathf.Abs(acceleration.x);
        }

       

        if (slideLeft)
        {
            transform.position += new Vector3(speed * Mathf.Abs(acceleration.x) , 0, 0);
        }
        else if (slideRight)
        {
            transform.position += new Vector3(-speed * Mathf.Abs(acceleration.x) , 0, 0);
        }
        

        
    }

    private void Roll()
    {
        if (Mathf.Abs(acceleration.x) > 0.045f && Input.acceleration.x > 0.4)
        {
            sphere.AddForce(Vector3.left * -15);
        }
        else if (Mathf.Abs(acceleration.x) > 0.045f && Input.acceleration.x < -0.4)
        {
            sphere.AddForce(Vector3.left * 15);
        }
    }
}
