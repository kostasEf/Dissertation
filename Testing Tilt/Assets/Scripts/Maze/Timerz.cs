using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timerz : MonoBehaviour {

    public float seconds = 0;

    private Text text1;

    void Start()
    {
        text1 = GameObject.Find("Text1").GetComponent<Text>();
    }

    void Update()
    {
        text1.text = seconds.ToString("0");

        seconds += Time.deltaTime;
    }
}
