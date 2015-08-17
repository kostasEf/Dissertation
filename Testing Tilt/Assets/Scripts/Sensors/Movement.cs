using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour {

    Rigidbody sphere;

    private const int speed = 5;

    private Text text1, text2, text3;

    private Queue<Vector3> XdataQueue = new Queue<Vector3>();

    private Queue<Vector3> YdataQueue = new Queue<Vector3>();

    private const int sampleSize = 5;

    private float xfilter, yfilter;

    private Vector3 accel;

    void Awake() {
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
	// Use this for initialization
	void Start () {

        sphere = GetComponent<Rigidbody>();

        Input.gyro.enabled = true;

        text1 = GameObject.Find("Text1").GetComponent<Text>();
        text2 = GameObject.Find("Text2").GetComponent<Text>();
        text3 = GameObject.Find("Text3").GetComponent<Text>();

        for (int i = 0; i < sampleSize; i++)
        {
            XdataQueue.Enqueue(Input.gyro.userAcceleration);
            YdataQueue.Enqueue(Input.gyro.userAcceleration);
        }

	}
	
	// Update is called once per frame
	void Update ()     {
        xfilter = Mathf.Abs(Xfilter().x);
        yfilter = Mathf.Abs(Yfilter().y);
        accel = Input.acceleration;

        if (xfilter > 0.05 && accel.x > 0.3)
        {
            sphere.AddForce((0.5f + Mathf.Abs(accel.x)) * speed, 0, 0);
        }else if (xfilter > 0.05 && accel.x < -0.3)
        {
            sphere.AddForce(-0.5f - Mathf.Abs(accel.x) * speed, 0, 0);
        }

        if (yfilter > 0.05 && accel.y > -0.4)
        {
            sphere.AddForce(0, 0, (0.5f + Mathf.Abs(accel.y)) * speed);
        }
        else if (yfilter > 0.05 && accel.y < -0.8)
        {
            sphere.AddForce(0, 0, -Mathf.Abs(accel.y) * speed);
        }
        

        //text1.text = AverageAcceleration().x.ToString("0.00");
        text1.text = Input.gyro.userAcceleration.y.ToString("0.00");
        text2.text = yfilter.ToString("0.00");
        text3.text = Input.acceleration.y.ToString("0.00");
        
	}

    private Vector3 Xfilter()    {
        if (Mathf.Abs(Input.gyro.userAcceleration.x) > 0.03 && Mathf.Abs(Input.gyro.userAcceleration.x) < 0.9)
            //&& Mathf.Abs(Input.gyro.userAcceleration.y) > 0.03 && Mathf.Abs(Input.gyro.userAcceleration.y) < 0.12)
        {
            XdataQueue.Enqueue(Input.gyro.userAcceleration);
            XdataQueue.Dequeue();
        }
        else
        {
            XdataQueue.Enqueue(Vector3.zero);
            XdataQueue.Dequeue();
        }
        

        Vector3 vFiltered = Vector3.zero;
        foreach (Vector3 v in XdataQueue)
            vFiltered += v;
        vFiltered /= sampleSize;
        return vFiltered;
    }

    private Vector3 Yfilter()
    {
        if (Mathf.Abs(Input.gyro.userAcceleration.y) > 0.03 && Mathf.Abs(Input.gyro.userAcceleration.y) < 0.22)
        {
            YdataQueue.Enqueue(Input.gyro.userAcceleration);
            YdataQueue.Dequeue();
        }
        else
        {
            YdataQueue.Enqueue(Vector3.zero);
            YdataQueue.Dequeue();
        }


        Vector3 vFiltered = Vector3.zero;
        foreach (Vector3 v in YdataQueue)
            vFiltered += v;
        vFiltered /= sampleSize;
        return vFiltered;
    }

}
