using UnityEngine;
using UnityEditor;

/*
 * A class that holds information on upgrdes
 */
public static class Upgrades
{
    //Glider = 0 when the glider upgrade has not been purchesed
    private static int glider;
    private static int booster;

    //Get and set Glider
    public static int Glider
    {
        get
        {
            return glider;
        }
        set
        {
            glider = value;
        }
    }

    public static int Booster
    {
        get
        {
            return booster;
        }
        set
        {
            booster = value;
        }
    }


}
