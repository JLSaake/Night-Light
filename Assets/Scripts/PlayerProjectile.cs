using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {


    public float offset;
    public int damage;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FireProjectile(UnityStandardAssets.Characters.FirstPerson.FirstPersonController player)
    {
        gameObject.transform.position = player.transform.position;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + player.transform.forward.x * offset,
                                                    gameObject.transform.position.y,
                                                    gameObject.transform.position.z + player.transform.forward.z * offset);
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(player.transform.forward.x * 4,
                                                                    (-player.GetComponentInChildren<Transform>().rotation.eulerAngles.x),
                                                                    player.transform.forward.z * 4);
        Scoring.MinusLight(1); // TODO: Set as variable
        Debug.Log("Firing: " + Scoring.GetLight() + " light remaining");
    }

    public void OnCollisionEnter(Collision collider)
    {
        Destroy(this.gameObject);
    }

    
}
