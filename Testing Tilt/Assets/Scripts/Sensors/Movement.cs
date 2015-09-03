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

    private Queue<Vector3> ZdataQueue = new Queue<Vector3>();

    private Queue<Vector3> AccelerationQueue = new Queue<Vector3>();

    private const int sampleSize = 10;

    private float xfilter, yfilter, zfilter;

    private Vector3 accel = Vector3.zero, prevAccel = Vector3.zero;

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
            ZdataQueue.Enqueue(Input.gyro.userAcceleration);
            AccelerationQueue.Enqueue(Input.acceleration);
        }

	}
	
	// Update is called once per frame
	void Update ()     {
        xfilter = Mathf.Abs(Xfilter().x);
        yfilter = Mathf.Abs(Yfilter().y);
        zfilter = Mathf.Abs(Zfilter().z);
        accel = Input.acceleration;

        if (Xfilter().x > 0.15 && Orientation(AccelerationQueue, 'x') == 1)
        {
            sphere.AddForce(25, 0, 0);
        }
        else if (Xfilter().x < -0.15 && Orientation(AccelerationQueue, 'x') == -1)
        {
            sphere.AddForce(-25, 0, 0);
        }


        if (Zfilter().z > -0.25 && Zfilter().z < -0.15 && Orientation(AccelerationQueue, 'y') == 1) // Bend Forward
        {
            sphere.AddForce(0, 0, 25);
        }
        else if (Zfilter().z < 0.25 && Zfilter().z > 0.18 && Orientation(AccelerationQueue, 'y') == -1) // Bend Backwards
        {
            sphere.AddForce(0, 0, -25);
        }

        

        //text1.text = (0 -0.1 -0.2 -0.3).ToString("0.00");
        //text2.text = Orientation(AccelerationQueue, 'x').ToString("0.00");
        //text3.text = Orientation(AccelerationQueue, 'y').ToString("0.00");

        //text1.text = PlayerPrefs.GetInt("Testing").ToString();
        //text2.text = Input.gyro.userAcceleration.y.ToString("0.00");
        //text3.text = Input.gyro.userAcceleration.z.ToString("0.00");

        //text1.text = Zfilter().z.ToString("0.00");
        //text2.text = Yfilter().y.ToString("0.00");
        //text3.text = Zfilter().z.ToString("0.00");


        //Camera.main.transform.rotation = Input.gyro.attitude;

        //Vector3 ea = Input.gyro.attitude.eulerAngles;

        //Camera.main.transform.eulerAngles = new Vector3(-ea[0], -ea[1], ea[2]);

        ManualControls();
        
	}

    private void ManualControls()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            sphere.AddForce(0, 0, 25);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            sphere.AddForce(-25, 0, 0);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            sphere.AddForce(0, 0, -25);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            sphere.AddForce(25, 0, 0);
        }
    }

    private int Orientation(Queue<Vector3> list, char axis)
    {
        AccelerationQueue.Enqueue(Input.acceleration);
        AccelerationQueue.Dequeue();

        short count = 0;
        float sum = 0;

        float[] arrayX = new float[sampleSize]; // -1 pros ta aristera, 1 pros ta deksia
        float[] arrayY = new float[sampleSize]; // -1 panw, 1 katw

        foreach (Vector3 v in list)
        {
            arrayX[count] = v.x;
            arrayY[count] = v.y;
            count += 1;
        }

        if (axis == 'x')
        {
            for (int i = 1; i < arrayX.Length; i++)
            {
                sum += arrayX[i] - arrayX[i - 1];
            }

            if (sum < -0.01 && Input.acceleration.x < -0.05)
            {
                return -1;
            }
            else if (sum > 0.01 && Input.acceleration.x > 0.05)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }
        else
        {
            for (int i = 1; i < arrayY.Length; i++)
            {
                sum += arrayY[i] - arrayY[i - 1];
            }

            if (sum < -0.01)
            {
                return -1;
            }
            else if (sum > 0.01)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        
    }

    private Vector3 Xfilter()
    {
        
        if (Mathf.Abs(Input.gyro.userAcceleration.x) > 0.03 && Mathf.Abs(Input.gyro.userAcceleration.x) < 0.8)
        {
            XdataQueue.Enqueue(Input.gyro.userAcceleration);
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

    private Vector3 Zfilter()
    {
        if (Mathf.Abs(Input.gyro.userAcceleration.z) > 0.07 && Mathf.Abs(Input.gyro.userAcceleration.z) < 0.14)
        {
            YdataQueue.Enqueue(Input.gyro.userAcceleration);
            YdataQueue.Dequeue();
        }
        //else
        //{
        //    YdataQueue.Enqueue(Vector3.zero);
        //    YdataQueue.Dequeue();
        //}


        Vector3 vFiltered = Vector3.zero;
        foreach (Vector3 v in YdataQueue)
            vFiltered += v;
        vFiltered /= sampleSize;

        return vFiltered;
    }

}
