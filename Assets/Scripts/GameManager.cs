using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Maze mazePrefab;
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerPrefab;
    public PlayerProjectile projectilePrefab;

    public float fastWalk;
    public float slowWalk;

    private int prevLight;

    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerInstance;
    private Maze mazeInstance;


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
	}

    private IEnumerator BeginGame()
    {
        mazeInstance = Instantiate(mazePrefab) as Maze;
        yield return StartCoroutine(mazeInstance.Generate());
        playerInstance = Instantiate(playerPrefab) as UnityStandardAssets.Characters.FirstPerson.FirstPersonController;
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
