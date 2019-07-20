// <copyright file="EndScreenText.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class contains the data used to update the text boxes
/// on the game over screen.
/// </summary>
public class EndScreenText : MonoBehaviour
{
    public Text Score; // variable for run score text box
    public Text HighScore; // variable for high score text box
    public Text Cash; // variable for available cash text box
    public Text Height; // variable for run maximum height text box
    public Text Dist; // variable for run maximum distance text box

    // Start is called before the first frame update
    private void Start()
    {
        // update score text
        this.Score.text = "Score: " + DB.Score;

        // update high score text
        this.HighScore.text = "High Score: " + DB.HighScore;

        // update cash text
        this.Cash.text = "Cash: $" + DB.BankCash;

        // update height text
        this.Height.text = "Max Height: " + DB.MaxHeight;

        // update distance text
        this.Dist.text = "Distance: " + DB.RunDist;
    }
}
