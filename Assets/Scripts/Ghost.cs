using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

    public static int ghostCount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /* TODO:
     * --------
     * Trigger Enter & Take Damage
     * Death effects & animation
     * Attack if player comes within range
     * Turn and face player
     * 
     */

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
