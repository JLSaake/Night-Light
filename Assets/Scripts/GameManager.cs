using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public LevelManager levelManager;
    public UIManager uiManager;
    public Maze mazePrefab;
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerPrefab;
    public PlayerProjectile projectilePrefab;
    public Timer timer;
    public float maxTime; // In seconds
    public Camera startingCamera;

    public float fastWalk;
    public float slowWalk;

    private int prevLight;

    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerInstance;
    private Maze mazeInstance;
    private bool outOfTime;
    private bool gameActive;


    private static bool gamePaused;


	// Use this for initialization
	void Start () {
        gameActive = false;
        gamePaused = false;
        StartCoroutine(BeginGame());
        prevLight = Scoring.GetLight();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Pause") && gamePaused)
        {
            UnpauseGame();
        } else
        if (gameActive)
        {
            if (Input.GetButtonDown("Pause"))
            {
                PauseGame();
            }

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
            //Debug.Log("Time Remaining: " + Mathf.Round(maxTime - timer.GetElapsedTime()) + " seconds");
            //Debug.Log("Ghosts Remaining: " + Ghost.GhostCountGet());
            //TODO: UI updating (light, time, and ghosts remaining goes here)

            if (Ghost.GhostCountGet() <=0)
            {
                playerInstance.setMove(false);
                Debug.Log("You Win!");
                gameActive = false;
                timer.PauseTime();
                playerInstance.FreeCursor();
                levelManager.LoadLevel("06Win");
            }

            uiManager.UpdateNumbers(Scoring.GetLight(), (maxTime - timer.GetElapsedTime()), Ghost.GhostCountGet());
        }

        if (timer.GetElapsedTime() > maxTime && !outOfTime)
        {
            playerInstance.setMove(false);
            outOfTime = true;
            gameActive = false;
            Debug.Log("GameOver");
            playerInstance.FreeCursor();
            levelManager.LoadLevel("05Lose");
        }



    }

    private IEnumerator BeginGame()
    {
        startingCamera.gameObject.SetActive(true);
        uiManager.ControlsUIOn();
        uiManager.LoadingGameUI();
        outOfTime = false;
        gameActive = false;
        gamePaused = false;
        mazeInstance = Instantiate(mazePrefab) as Maze;
        yield return StartCoroutine(mazeInstance.Generate());
        uiManager.PregameUIButtonOn();

        // TODO: remove start maze from here and hook up to button
        // StartMaze();
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

    private void PauseGame()
    {
        gameActive = false;
        gamePaused = true;
        timer.PauseTime();
        playerInstance.setMove(false);
        uiManager.PauseUIOn();
    }

    public void UnpauseGame()
    {
        gameActive = true;
        gamePaused = false;
        timer.ResumeTime();
        playerInstance.setMove(true);
        uiManager.PauseUIOff();
    }

    public void StartMaze()
    {
        uiManager.PregameUIButtonOff();
        startingCamera.gameObject.SetActive(false);
        uiManager.ControlsUIOff();
        playerInstance = Instantiate(playerPrefab) as UnityStandardAssets.Characters.FirstPerson.FirstPersonController;
        uiManager.ChangeToGameUI();
        timer.StartTime();
        gameActive = true;
    }

}
