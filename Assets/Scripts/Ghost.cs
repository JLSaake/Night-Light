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
    private Quaternion startRotation;
    private Transform playerTransform;
    private Animator animator;
    private bool isDead;

	// Use this for initialization
	void Start () {
        onCooldown = false;
        isDead = false;
        startRotation = this.gameObject.transform.rotation;
        animator = this.gameObject.GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("No GameManager found");
        }
	}
	
	// Update is called once per frame
	void Update () {
        playerTransform = gameManager.GetPlayerTransform();
        if (playerTransform != null)
        {
            this.gameObject.transform.rotation = startRotation;
            this.gameObject.transform.Rotate(new Vector3(this.gameObject.transform.eulerAngles.x,
                                                         playerTransform.eulerAngles.y + 180,
                                                         this.gameObject.transform.eulerAngles.z));
        }
	}

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && !onCooldown && !isDead)
        {
            animator.Play("BiteAttack");
            if (colorGhost == GhostType.RED)
            {
                gameManager.Blind(cooldown);
            } else
            if (colorGhost == GhostType.BLUE)
            {
                gameManager.TeleportPlayer();
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
            isDead = true;
            animator.Play("Die");
            Invoke("Die", 1.28f);
        } else
        {
            animator.Play("TakeDamage");
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
        Ghost.GhostCountMinus(1);
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
