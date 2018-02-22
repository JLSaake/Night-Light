using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour {

    public float cooldown;
    public int lightGive;

    public Light light1;
    public Light light2;
    public Light light3;
    public ParticleSystem particles;

    private bool onCooldown;

	// Use this for initialization
	void Start () {
        onCooldown = false;
        light1.gameObject.SetActive(true);
        light2.gameObject.SetActive(true);
        light3.gameObject.SetActive(true);
        particles.gameObject.SetActive(true);
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
        light1.gameObject.SetActive(false);
        light2.gameObject.SetActive(false);
        light3.gameObject.SetActive(false);
        particles.gameObject.SetActive(false);
        Debug.Log("Warming! " + Scoring.GetLight() + " light remaining");
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
        light1.gameObject.SetActive(true);
        light2.gameObject.SetActive(true);
        light3.gameObject.SetActive(true);
        particles.gameObject.SetActive(true);
    }
}
