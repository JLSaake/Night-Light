using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour {

    public float cooldown;

    private bool canTake;
    private bool isActive;

	// Use this for initialization
	void Start () {
        canTake = false;
        isActive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1") && canTake && isActive)
        {
            StartCoroutine(TakeTorch());
        }
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && isActive)
        {
            canTake = true;
            Debug.Log("CanTake");
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            canTake = false;
            Debug.Log("CanNolongerTake");
        }
    }

    private IEnumerator TakeTorch()
    {
        Debug.Log("TakingTorch");
        //TODO: add light to player
        isActive = false;
        canTake = false;
        yield return new WaitForSeconds(cooldown);
        isActive = true;
        Debug.Log("TorchActive");
    }

}
