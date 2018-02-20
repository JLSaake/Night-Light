using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Maze mazePrefab;
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerPrefab;
    public PlayerProjectile projectilePrefab;


    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerInstance;
    private Maze mazeInstance;

    // TODO: Projectile pooling (20 max?)

	// Use this for initialization
	void Start () {
        StartCoroutine(BeginGame());
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire2") && playerInstance != null)
        {
            PlayerProjectile projectile = Instantiate(projectilePrefab) as PlayerProjectile;
            projectile.FireProjectile(playerInstance);
            
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
