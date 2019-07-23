// <copyright file="button.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using UnityEngine;

#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable SA1300 // Element should begin with upper-case letter
/// <summary>
/// This class establishes behavior for a button press.
/// </summary>
public class button : MonoBehaviour
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore IDE1006 // Naming Styles
{
    private void OnMouseDown()
    {
        // load a new scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
