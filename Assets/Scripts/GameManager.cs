using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Maze mazePrefab;
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerPrefab;
    public PlayerProjectile projectilePrefab;
    public Timer timer;
    public float maxTime; // In seconds

    public float fastWalk;
    public float slowWalk;

    private int prevLight;

    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerInstance;
    private Maze mazeInstance;
    private bool outOfTime;
    private bool gameActive;


	// Use this for initialization
	void Start () {
        StartCoroutine(BeginGame());
        prevLight = Scoring.GetLight();
	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetButtonDown("Fire2")) && (playerInstance != null) && (Scoring.GetLight() > 0))
        {
            PlayerProjectile projectile = Instantiate(projectilePrefab) as PlayerProjectile;
            projectile.FireProjectile(playerInstance);
            
        }
        if (prevLight != Scoring.GetLight())
        {
            prevLight = Scoring.GetLight();
            if (Scoring.GetLight() <= 0)
            {
                playerInstance.setWalkSpeed(slowWalk);
            }
            else
            {
                playerInstance.setWalkSpeed(fastWalk);
            }
        }

        if (gameActive)
        {
            //Debug.Log("Time Remaining: " + Mathf.Round(maxTime - timer.GetElapsedTime()) + " seconds");
            //Debug.Log("Ghosts Remaining: " + Ghost.GhostCountGet());
            //TODO: UI updating (light, time, and ghosts remaining goes here)

            if (Ghost.GhostCountGet() <=0)
            {
                Debug.Log("You Win!");
                timer.PauseTime();
                // Sethigh score (time shortest elapsed time)
            }
        }

        if (timer.GetElapsedTime() > maxTime && !outOfTime)
        {
            outOfTime = true;
            gameActive = false;
            Debug.Log("GameOver");
            //Invoke("RestartGame", 10); // TEMPORARY
        }
	}

    private IEnumerator BeginGame()
    {
        outOfTime = false;
        gameActive = false;
        mazeInstance = Instantiate(mazePrefab) as Maze;
        yield return StartCoroutine(mazeInstance.Generate());
        playerInstance = Instantiate(playerPrefab) as UnityStandardAssets.Characters.FirstPerson.FirstPersonController;
        timer.StartTime();
        gameActive = true;
    }

    private void RestartGame()
    {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        if (playerInstance != null)
        {
            Destroy(playerInstance.gameObject);
        }
        StartCoroutine(BeginGame());
    }
}
