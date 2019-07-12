public static class DB
{
    private static int lvlIndex, pi, glider, booster, runConNum, cash;
    private static float score, highScore, maxSpeed, maxHeight, runDist;


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

    //setter and getter for booster value
    //booster value represents if you have purchased the booster or not
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
