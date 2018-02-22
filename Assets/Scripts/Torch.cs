using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour {

    public float cooldown;
    public ParticleSystem particles;
    public Light light1;
    public Light light2;

    private bool canTake;
    private bool isActive;
    private bool playerIn;

	// Use this for initialization
	void Start () {
        canTake = false;
        isActive = true;
        playerIn = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1") && canTake && playerIn)
        {
            StartCoroutine(TakeTorch());
        }
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (isActive)
            {
                canTake = true;
            }
            playerIn = true;
            Debug.Log("CanTake");
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            canTake = false;
            playerIn = false;
            Debug.Log("CanNolongerTake");
        }
    }

    private IEnumerator TakeTorch()
    {
        Debug.Log("TakingTorch");
        Scoring.AddLight(1); // TODO: make constant
        Debug.Log("Taking: " + Scoring.GetLight() + " light remaining");
        isActive = false;
        canTake = false;
        particles.gameObject.SetActive(false);
        light1.gameObject.SetActive(false);
        light2.gameObject.SetActive(false);
        yield return new WaitForSeconds(cooldown);
        particles.gameObject.SetActive(true);
        light1.gameObject.SetActive(true);
        light2.gameObject.SetActive(true);
        isActive = true;
        if (playerIn)
        {
            canTake = true;
        }
        Debug.Log("TorchActive");
    }
}
