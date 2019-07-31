// <copyright file="EndScreenText.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

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
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://ropeglide.firebaseio.com/");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
       
        
        // update score text
        this.Score.text = "Score: " + DB.Score;

        // update high score text
        float hiscore = DB.HighScore;
        this.HighScore.text = "High Score: " + hiscore;

        //if hiscore > leaderboard hiscores
        string displayName = "testName";
        if ((Application.platform == RuntimePlatform.IPhonePlayer) || SystemInfo.operatingSystem.Contains("Mac"))
        {
            reference.Child("Leaderboard").Child(displayName).SetPriorityAsync(hiscore);
        }
            

        // update cash text
        this.Cash.text = "Cash: $" + DB.BankCash;

        // update height text
        this.Height.text = "Max Height: " + DB.MaxHeight;

        // update distance text
        this.Dist.text = "Distance: " + DB.RunDist;
    }
}
