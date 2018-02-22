using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

    public static int ghostCount;
    public enum GhostType { RED, BLUE, GREEN };

    public GhostType colorGhost;
    public int health;
    public float cooldown;
    public int lightDrain;

    private bool onCooldown;
    private GameManager gameManager;
    private Camera playerMapCamera;

	// Use this for initialization
	void Start () {
        onCooldown = false;
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("No GameManager found");
        }
        playerMapCamera = gameManager.overheadCamera;
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && !onCooldown)
        {
            if (colorGhost == GhostType.RED)
            {
                gameManager.Blind(cooldown);
            }
            StartCoroutine(DrainLight(lightDrain));
        } else
        if (collider.gameObject.tag == "Projectile")
        {
            PlayerProjectile projectile = collider.gameObject.GetComponent<PlayerProjectile>();
            if (projectile != null)
            {
                DealDamage(projectile.damage);
                Destroy(projectile.gameObject);
                Ghost.GhostCountMinus(1);
            }
        }
    }

    /* TODO:
     * --------
     * Trigger Enter & Take Damage
     * Death effects & animation
     * Attack if player comes within range
     * Turn and face player
     * 
     */

    
    private IEnumerator DrainLight(int light)
    {
        onCooldown = true;
        Scoring.MinusLight(light);
        Debug.Log("Attacking player! " + Scoring.GetLight() + " light remaining");
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }

    private void DealDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }



    public static void GhostCountPlus(int add)
    {
        ghostCount += add;
    }

    public static void GhostCountMinus(int minus)
    {
        ghostCount -= minus;
        if (ghostCount < 0)
        {
            GhostCountReset();
        }
    }

    public static void GhostCountReset()
    {
        ghostCount = 0;
    }

    public static int GhostCountGet()
    {
        return ghostCount;
    }
}
