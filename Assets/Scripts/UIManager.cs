﻿using System.Collections;
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
    public RawImage minimap;

    public GameObject preGameUI;
    public Text generatingMazeText;
    public Button startButton;

    public GameObject pauseUI;
    public Image backdropPanel;
    public Button controlsPauseButton;

    public Image blindPanel;
    public Image minimapDarkPanel;
    public Image playerDarkPanel;

    private bool gameUIOn;

	// Use this for initialization
	void Awake () {
        ClearUI();
	}
	
	// Update is called once per frame
	void Update () {
        if (Scoring.GetLight() <= 10)
        {
            minimapDarkPanel.color = new Color(minimapDarkPanel.color.r,
                                               minimapDarkPanel.color.g,
                                               minimapDarkPanel.color.b,
                                               ((1 - ((float)Scoring.GetLight() / 10)) *2));
        }
        if (Scoring.GetLight() <= 5)
        {
            playerDarkPanel.color = new Color(playerDarkPanel.color.r,
                                               playerDarkPanel.color.g,
                                               playerDarkPanel.color.b,
                                               (.7f - (float)Scoring.GetLight() / 5));
        }
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
        minimap.gameObject.SetActive(true);
    }

    public void GameUIOff()
    {
        inGameUI.gameObject.SetActive(false);
        lightNumber.gameObject.SetActive(false);
        secondsNumber.gameObject.SetActive(false);
        ghostsNumber.gameObject.SetActive(false);
        minimap.gameObject.SetActive(false);
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
        startButton.gameObject.SetActive(true);
        generatingMazeText.gameObject.SetActive(false);
    }

    public void PregameUIButtonOff()
    {
        generatingMazeText.gameObject.SetActive(true);
        startButton.gameObject.SetActive(false);
    }

    public void PregameUIOff()
    {
        preGameUI.gameObject.SetActive(false);
    }

    public void PregameUIOn()
    {
        preGameUI.gameObject.SetActive(true);
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
        blindPanel.gameObject.SetActive(false);
    }
    // TODO: RESTART & QUIT (with respective confirmation panels)

    public IEnumerator Blind(float seconds)
    {
        blindPanel.gameObject.SetActive(true);
        Debug.Log("Blinding");
        yield return new WaitForSeconds(seconds);
        blindPanel.gameObject.SetActive(false);
    }

}
