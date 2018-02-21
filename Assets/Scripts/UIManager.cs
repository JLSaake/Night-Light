using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text lightNumber;
    public Text secondsNumber;
    public Text ghostsNumber;

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
        lightNumber.gameObject.SetActive(true);
        secondsNumber.gameObject.SetActive(true);
        ghostsNumber.gameObject.SetActive(true);
    }

    public void LoadingGameUI()
    {
        lightNumber.gameObject.SetActive(false);
        secondsNumber.gameObject.SetActive(false);
        ghostsNumber.gameObject.SetActive(false);
    }


}
