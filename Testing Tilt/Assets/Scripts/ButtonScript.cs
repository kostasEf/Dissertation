using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonScript : MonoBehaviour {

    public Image image;
    private bool fill = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (fill == true)
        {
            image.fillAmount += 0.01f;
        }
        else
        {
            image.fillAmount -= 0.01f;
        }

        
        image.color = new Vector4(image.color.r, image.color.g, image.color.b, Mathf.Lerp(image.color.a, 0, Time.deltaTime));
        
	}

    void OnTriggerEnter(Collider other)
    {
        fill = true;
    }

    void OnTriggerExit(Collider other)
    {
        fill = false;
    }
}
