public static class DB
{
    private static int lvlIndex, pi, glider, booster;
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
}
