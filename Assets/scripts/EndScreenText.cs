using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenText : MonoBehaviour
{
    public Text score;
    public Text HighScore;
    public Text Cash;
    public Text Height;
    public Text Dist;

    // Start is called before the first frame update
    void Start()
    {
        //update score text
        score.text = "Score: " + DB.Score;

        //update high score text
        HighScore.text = "High Score: " + DB.HighScore;

        //update cash text
        Cash.text = "Cash: $" + DB.BankCash;

        //update height text
        Height.text = "Max Height: " + DB.MaxHeight;

        //update distance text
        Dist.text = "Distance: " + DB.RunDist;

    }
}
