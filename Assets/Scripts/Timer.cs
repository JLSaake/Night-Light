using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {


    private bool timeOn;
    private float startTime;
    private float timeElapsed;

	// Use this for initialization
	void Start () {
        timeElapsed = 0;
        startTime = 0;
        timeOn = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (timeOn)
        {
            timeElapsed = Time.timeSinceLevelLoad - startTime;
        }
	}

    public void StartTime()
    {
        startTime = Time.timeSinceLevelLoad;
        timeOn = true;
        Debug.Log("Starting");
    }

    public void PauseTime()
    {
        timeOn = false;
        Debug.Log("Pausing");
    }

    public void ResumeTime()
    {
        startTime = Time.timeSinceLevelLoad - timeElapsed;
        timeOn = true;
        Debug.Log("Resuming");
    }

    public float GetElapsedTime()
    {
        return timeElapsed;
    }

}
