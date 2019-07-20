// <copyright file="LevelControl.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class controls the loaded level.
/// </summary>
public class LevelControl : MonoBehaviour
{
    /// <summary>
    /// The currently loaded level.
    /// </summary>
    public int LvlIndex;

    /// <summary>
    /// Request for level to be changed.
    /// </summary>
    public int RequestLvlChange;

    // private void Update()
    // {
    // Debug.Log("sugma");
    //   if(requestLvlChange == 1)
    //    {
    //         SceneManager.LoadScene(2);
    //         requestLvlChange = 0;
    //     }
    // }

    /// <summary>
    /// Restart the game.
    /// </summary>
    public void Restart()
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
