// <copyright file="LevelControl.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class controls the level that is loaded.
/// </summary>
public class LevelControl : MonoBehaviour
{
#pragma warning disable SA1401 // Fields should be private
#pragma warning disable CA1051 // Do not declare visible instance fields
    /// <summary>
    /// The currently loaded level.
    /// </summary>
    public int LvlIndex;
#pragma warning restore CA1051 // Do not declare visible instance fields
#pragma warning restore SA1401 // Fields should be private

#pragma warning disable SA1401 // Fields should be private
#pragma warning disable CA1051 // Do not declare visible instance fields
    /// <summary>
    /// Request for level to be changed.
    /// </summary>
    public int RequestLvlChange;
#pragma warning restore CA1051 // Do not declare visible instance fields
#pragma warning restore SA1401 // Fields should be private

    // private void Update()
    // {
    // Debug.Log("sugma");
    //   if(requestLvlChange == 1)
    //    {
    //         SceneManager.LoadScene(2);
    //         requestLvlChange = 0;
    //     }
    // }

#pragma warning disable CA1822 // Mark members as static
    /// <summary>
    /// Restart the game.
    /// </summary>
    public void Restart()
#pragma warning restore CA1822 // Mark members as static
    {
        GameObject game = GameObject.FindWithTag("Game Controller");
        SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(this.LvlIndex);
        }
    }
}
