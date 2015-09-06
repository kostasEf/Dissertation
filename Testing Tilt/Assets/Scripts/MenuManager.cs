using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour {

    //------Start Menu------//
    public GameObject playButton;
    public GameObject modeButton;
    public GameObject controlsButton;
    //------Mode Menu------//
    public GameObject normalButton;
    public GameObject hardButton;
    public GameObject insaneButton;
    //------Controls Menu------//
    public GameObject tiltButton;
    public GameObject rehabButton;

    //------Not Related------//
    private GameManager GameManager;

	// Use this for initialization
	void Start () {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.menuState == 0)
        {
            playButton.SetActive(true);
            modeButton.SetActive(true);
            controlsButton.SetActive(true);
        }
        else if (GameManager.menuState == 1)
        {
            normalButton.SetActive(true);
            hardButton.SetActive(true);
            insaneButton.SetActive(true);
        }
        else if (GameManager.menuState == 2)
        {
            tiltButton.SetActive(true);
            rehabButton.SetActive(true);
        }
        else if (GameManager.menuState == 3)
        {
            playButton.SetActive(false);
            modeButton.SetActive(false);
            controlsButton.SetActive(false);

            normalButton.SetActive(false);
            hardButton.SetActive(false);
            insaneButton.SetActive(false);

            tiltButton.SetActive(false);
            rehabButton.SetActive(false);
        }
	}

    public void play()
    {
        GameManager.menuState = 3;
        GameManager.RestartGame();
        playButton.SetActive(false);
        modeButton.SetActive(false);
        controlsButton.SetActive(false);
    }

    public void mode()    {
        GameManager.menuState = 1;
        playButton.SetActive(false);
        modeButton.SetActive(false);
        controlsButton.SetActive(false);
        GameManager.RestartGame();
    }

    public void controls()    {
        GameManager.menuState = 2;
        playButton.SetActive(false);
        modeButton.SetActive(false);
        controlsButton.SetActive(false);
        GameManager.RestartGame();
    }

    public void normal() {
        GameManager.menuState = 0;
        normalButton.SetActive(false);
        hardButton.SetActive(false);
        insaneButton.SetActive(false);
        GameManager.RestartGame();
    }

    public void hard() {
        GameManager.menuState = 0;
        normalButton.SetActive(false);
        hardButton.SetActive(false);
        insaneButton.SetActive(false);
        GameManager.RestartGame();
    }

    public void insane() {
        GameManager.menuState = 0;
        normalButton.SetActive(false);
        hardButton.SetActive(false);
        insaneButton.SetActive(false);
        GameManager.RestartGame();
    }

    public void tilt() {
        GameManager.menuState = 0;
        tiltButton.SetActive(false);
        rehabButton.SetActive(false);
        GameManager.RestartGame();
    }

    public void rehab() {
        GameManager.menuState = 0;
        tiltButton.SetActive(false);
        rehabButton.SetActive(false);
        GameManager.RestartGame();
    }
}
