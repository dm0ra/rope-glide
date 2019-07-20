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
    /// <summary>
    /// Score text box.
    /// </summary>
    public Text Score;

    /// <summary>
    /// High score text box.
    /// </summary>
    public Text HighScore;

    /// <summary>
    /// Text box for available cash.
    /// </summary>
    public Text Cash;

    /// <summary>
    /// Maximum run height text box.
    /// </summary>
    public Text Height;

    /// <summary>
    /// Run distance.
    /// </summary>
    public Text Dist;

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
