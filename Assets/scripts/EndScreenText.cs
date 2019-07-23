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
#pragma warning disable SA1401 // Fields should be private
#pragma warning disable CA1051 // Do not declare visible instance fields
    /// <summary>
    /// Score text box.
    /// </summary>
    public Text Score;
#pragma warning restore CA1051 // Do not declare visible instance fields
#pragma warning restore SA1401 // Fields should be private

#pragma warning disable SA1401 // Fields should be private
#pragma warning disable CA1051 // Do not declare visible instance fields
    /// <summary>
    /// High score text box.
    /// </summary>
    public Text HighScore;
#pragma warning restore CA1051 // Do not declare visible instance fields
#pragma warning restore SA1401 // Fields should be private

#pragma warning disable SA1401 // Fields should be private
#pragma warning disable CA1051 // Do not declare visible instance fields
    /// <summary>
    /// Cash text box.
    /// </summary>
    public Text Cash;
#pragma warning restore CA1051 // Do not declare visible instance fields
#pragma warning restore SA1401 // Fields should be private

#pragma warning disable SA1401 // Fields should be private
#pragma warning disable CA1051 // Do not declare visible instance fields
    /// <summary>
    /// Run maximum height text box.
    /// </summary>
    public Text Height;
#pragma warning restore CA1051 // Do not declare visible instance fields
#pragma warning restore SA1401 // Fields should be private

#pragma warning disable SA1401 // Fields should be private
#pragma warning disable CA1051 // Do not declare visible instance fields
    /// <summary>
    /// Run distance text box.
    /// </summary>
    public Text Dist;
#pragma warning restore CA1051 // Do not declare visible instance fields
#pragma warning restore SA1401 // Fields should be private

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
