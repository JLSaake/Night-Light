using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {


    public float offset;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FireProjectile(GameObject player)
    {
        gameObject.transform.position = player.transform.position;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + player.transform.forward.x * offset,
                                                    gameObject.transform.position.y,
                                                    gameObject.transform.position.z + player.transform.forward.z * offset);
        //rb.velocity = player.transform.forward * 3;
        gameObject.GetComponent<Rigidbody>().velocity = player.transform.forward * 3;
    }

    
}
