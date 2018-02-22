using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private bool inPauseMain;
    public GameObject controlsPrefab;

    public GameObject inGameUI;
    public Text lightNumber;
    public Text secondsNumber;
    public Text ghostsNumber;

    public GameObject preGameUI;
    public Button startButton;

    public GameObject pauseUI;
    public Image backdropPanel;
    public Button controlsPauseButton;

	// Use this for initialization
	void Awake () {
        ClearUI();
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

    public void GameUIOn()
    {
        inGameUI.gameObject.SetActive(true);
        lightNumber.gameObject.SetActive(true);
        secondsNumber.gameObject.SetActive(true);
        ghostsNumber.gameObject.SetActive(true);
    }

    public void GameUIOff()
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

    public void PauseUIOn()
    {
        GameUIOff();
        inPauseMain = true;
        backdropPanel.gameObject.SetActive(true);
        pauseUI.gameObject.SetActive(true);
    }

    public void PauseUIOff()
    {
        GameUIOn();
        inPauseMain = false;
        backdropPanel.gameObject.SetActive(false);
        pauseUI.gameObject.SetActive(false);
    }

    public void PauseToControls()
    {
        pauseUI.gameObject.SetActive(false);
        controlsPrefab.gameObject.SetActive(true);
        controlsPauseButton.gameObject.SetActive(true);
        inPauseMain = false;
        // Add return button on
    }

    public void PauseFromControls()
    {
        pauseUI.gameObject.SetActive(true);
        controlsPrefab.gameObject.SetActive(false);
        controlsPauseButton.gameObject.SetActive(false);
        inPauseMain = true;        
    }

    public bool InPauseMain()
    {
        return inPauseMain;
    }

    public void ClearUI()
    {
        inPauseMain = false;
        controlsPauseButton.gameObject.SetActive(false);
        pauseUI.gameObject.SetActive(false);
        preGameUI.gameObject.SetActive(false);
        inGameUI.gameObject.SetActive(false);
        backdropPanel.gameObject.SetActive(false);
        controlsPrefab.gameObject.SetActive(false);
    }
    // TODO: RESTART & QUIT (with respective confirmation panels)
}
