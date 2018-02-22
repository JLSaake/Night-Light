using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject controlsPrefab;

    public GameObject inGameUI;
    public Text lightNumber;
    public Text secondsNumber;
    public Text ghostsNumber;

    public GameObject preGameUI;
    public Button startButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void UpdateNumbers(int light, float seconds, int ghosts)
    {
        lightNumber.text = light.ToString();
        secondsNumber.text = seconds.ToString();
        ghostsNumber.text = ghosts.ToString();
    }

    public void ChangeToGameUI()
    {
        inGameUI.gameObject.SetActive(true);
        lightNumber.gameObject.SetActive(true);
        secondsNumber.gameObject.SetActive(true);
        ghostsNumber.gameObject.SetActive(true);
    }

    public void LoadingGameUI()
    {
        inGameUI.gameObject.SetActive(false);
        lightNumber.gameObject.SetActive(false);
        secondsNumber.gameObject.SetActive(false);
        ghostsNumber.gameObject.SetActive(false);
    }

    public void ControlsUIOn()
    {
        controlsPrefab.gameObject.SetActive(true);
    }

    public void ControlsUIOff()
    {
        controlsPrefab.gameObject.SetActive(false);
    }
    
    public void PregameUIButtonOn()
    {
        preGameUI.gameObject.SetActive(true);
        preGameUI.gameObject.SetActive(true);
    }

    public void PregameUIButtonOff()
    {
        preGameUI.gameObject.SetActive(false);
        preGameUI.gameObject.SetActive(false);
    }
}
