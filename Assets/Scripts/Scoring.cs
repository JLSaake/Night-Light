using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Scoring {

    private static int playerLight;
    private static int lightMax;

    public static void AddLight(int light)
    {
        playerLight += light;
        if (playerLight > lightMax)
        {
            playerLight = lightMax;
        }
    }

    public static void MinusLight(int light)
    {
        playerLight -= light;
        if (playerLight < 0)
        {
            playerLight = 0;
        }
    }

    public static int GetLight()
    {
        return playerLight;
    }

    public static void SetLight(int light)
    {
        playerLight = light;
        if (playerLight > lightMax)
        {
            playerLight = light;
        }
    }

    public static void SetLightMax(int light)
    {
        lightMax = light;
    }

    public static int GetLightMax()
    {
        return lightMax;
    }
}
