using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tilt : MonoBehaviour {

    private Quaternion localRotation; // 
    public float speed = 1.0f; // ajustable speed from Inspector in Unity editor

    private Text text1, text2, text3;

    // Use this for initialization
    void Start()
    {
        Screen.autorotateToLandscapeLeft = true;

        Screen.autorotateToLandscapeRight = true;

        Screen.autorotateToPortrait = false;

        Screen.autorotateToPortraitUpsideDown = false;

        Screen.orientation = ScreenOrientation.AutoRotation;

        // copy the rotation of the object itself into a buffer
        localRotation = transform.rotation;

        //text1 = GameObject.Find("Text1").GetComponent<Text>();
        //text2 = GameObject.Find("Text2").GetComponent<Text>();
        //text3 = GameObject.Find("Text3").GetComponent<Text>();
    }


    void Update() // runs 60 fps or so
    {
        float curSpeed = Time.deltaTime * speed;

        localRotation.z = -(Input.acceleration.x);// *curSpeed;
        localRotation.x = Input.acceleration.y;// *curSpeed;

        // then rotate this object accordingly to the new angle
        transform.rotation = Quaternion.Lerp(transform.rotation, localRotation, curSpeed);

        //text1.text = Input.acceleration.x.ToString();
        //text2.text = Input.acceleration.y.ToString();
        //text3.text =  (-Input.acceleration.z).ToString();
    }
}
