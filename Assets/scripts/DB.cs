using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DB
{
    private static int lvlIndex;
    private static float score, highScore;


    public static float HighScore
    {
        get
        {
            return highScore;
        }
        set
        {
            highScore = value;
        }
    }

    public static float Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }

    public static int LvlIndex
    {
        get
        {
            return lvlIndex;
        }
        set
        {
            lvlIndex = value;
        }
    }
}
