using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour {

    public float cooldown;
    public int lightGive;

    private bool onCooldown;

	// Use this for initialization
	void Start () {
        onCooldown = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && !onCooldown)
        {
            StartCoroutine(GiveLight(lightGive));
        }
    }

    private IEnumerator GiveLight(int light)
    {
        onCooldown = true;
        Scoring.AddLight(light);
        Debug.Log("Warming! " + Scoring.GetLight() + " light remaining");
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }
}
