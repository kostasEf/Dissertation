using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class gyroscope : MonoBehaviour {

    private float velocity;
    private Vector3 accelerationOld, accelerationNew = Vector3.zero;

    private Text text1, text2, text3;

    private Rigidbody sphere;
    

    private float min = 0, max = 0;

    bool slideLeft = false;
    bool slideRight = false;
    float speed = 0;

    void Start()
    {
        sphere = GetComponent<Rigidbody>();

        Input.gyro.enabled = true;
       

        text1 = GameObject.Find("Text1").GetComponent<Text>();
        text2 = GameObject.Find("Text2").GetComponent<Text>();
        text3 = GameObject.Find("Text3").GetComponent<Text>();
    }

    void Update()
    {
        accelerationOld = accelerationNew;
        accelerationNew = Input.gyro.userAcceleration;
        
        

        //push();
        //Slide();
        //Roll();

        

        text1.text = Input.acceleration.y.ToString("0.00");
        //text2.text = AverageAcceleration().ToString("0.00");
        //text3.text = accelerationOld.x.ToString("0.00");

        


    }

    
    
    private void push()
    {
        if (Mathf.Abs(accelerationOld.x) > 0.045f && Input.acceleration.x > 0.4)
        {
            transform.position += new Vector3(Mathf.Abs(accelerationOld.x) * 2, 0, 0);
        }
        else if (Mathf.Abs(accelerationOld.x) > 0.045f && Input.acceleration.x < -0.4)
        {
            transform.position += new Vector3(-Mathf.Abs(accelerationOld.x) * 2, 0, 0);
        }
    }

    private void Slide()
    {

        
        if (Mathf.Abs(accelerationOld.x) > 0.045f && Input.acceleration.x > 0.4)
        {
            slideRight = false;
            slideLeft = true;
            speed = Mathf.Abs(accelerationOld.x);
        }
        else if (Mathf.Abs(accelerationOld.x) > 0.045f && Input.acceleration.x < -0.4)
        {
            slideLeft = false;
            slideRight = true;
            speed = Mathf.Abs(accelerationOld.x);
        }

       

        if (slideLeft)
        {
            transform.position += new Vector3(speed * Mathf.Abs(accelerationOld.x) , 0, 0);
        }
        else if (slideRight)
        {
            transform.position += new Vector3(-speed * Mathf.Abs(accelerationOld.x) , 0, 0);
        }
        

        
    }

    private void Roll()
    {
        if (Mathf.Abs(accelerationOld.x) > 0.045f && Input.acceleration.x > 0.4)
        {
            sphere.AddForce(Vector3.left * -150);
        }
        else if (Mathf.Abs(accelerationOld.x) > 0.045f && Input.acceleration.x < -0.4)
        {
            sphere.AddForce(Vector3.left * 150);
        }
    }
}
