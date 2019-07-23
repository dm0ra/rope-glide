// <copyright file="SceneSwitch.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using UnityEngine.SceneManagement;

/// <summary>
/// A class that controls scene switching.
/// </summary>
public class SceneSwitch
{
#pragma warning disable SA1307 // Accessible fields should begin with upper-case letter
#pragma warning disable SA1401 // Fields should be private
    /// <summary>
    /// This holds the index for the next scene.
    /// </summary>
    public int sceneIndex;
#pragma warning restore SA1401 // Fields should be private
#pragma warning restore SA1307 // Accessible fields should begin with upper-case letter

    /// <summary>
    /// switch scene to index. The index for scenes are set in build settings.
    /// </summary>
    /// <param name="index">int index.</param>
    public void SwitchScenes(int index)
    {
        this.sceneIndex = index;
        SceneManager.LoadScene(index);
    }

    /// <summary>
    /// Get current scene index.
    /// </summary>
    /// <returns>int sceneIndex.</returns>
    public int GetSceneIndex()
    {
        return this.sceneIndex;
    }
}
