// <copyright file="DB.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// This class is used as an in memory database like class.
/// When scenes change, the variables inside the scripts are reset, so this class acts as a placeholder to store data for scene switches.
/// </summary>
public static class DB
{
    private static int lvlIndex;
    private static int pi;
    private static int glider;
    private static int booster;
    private static int runConNum;
    private static int cash;
    private static float score;
    private static float highScore;
    private static float maxSpeed;
    private static float maxHeight;
    private static float runDist;

    /// <summary>
    /// Gets or sets the variable highScore.
    /// </summary>
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

    /// <summary>
    /// Gets or sets the variable score.
    /// </summary>
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

    /// <summary>
    /// Gets or sets the variable lvlIndex.
    /// </summary>
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

    /// <summary>
    /// Gets or sets the variable previewIndex.
    /// </summary>
    public static int PreviewIndex
    {
        get
        {
            return pi;
        }

        set
        {
            pi = value;
        }
    }

    /// <summary>
    /// Gets or sets the variable glider.
    /// </summary>
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

    /// <summary>
    /// Gets or sets the variable booster.
    /// </summary>
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

    /// <summary>
    /// Gets or sets the variable maxSpeed.
    /// </summary>
    public static float MaxSpeed
    {
        get
        {
            return maxSpeed;
        }

        set
        {
            maxSpeed = value;
        }
    }

    /// <summary>
    /// Gets or sets the variable maxHeight.
    /// </summary>
    public static float MaxHeight
    {
        get
        {
            return maxHeight;
        }

        set
        {
            maxHeight = value;
        }
    }

    /// <summary>
    /// Gets or sets the variable runDist.
    /// </summary>
    public static float RunDist
    {
        get
        {
            return runDist;
        }

        set
        {
            runDist = value;
        }
    }

    /// <summary>
    /// Gets or sets the variable runConNum.
    /// </summary>
    public static int RunConNum
    {
        get
        {
            return runConNum;
        }

        set
        {
            runConNum = value;
        }
    }

    /// <summary>
    /// Gets or sets the variable cash.
    /// </summary>
    public static int BankCash
    {
        get
        {
            return cash;
        }

        set
        {
            cash = value;
        }
    }
}
