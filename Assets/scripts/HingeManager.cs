// <copyright file="HingeManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
using System.Collections;
using UnityEngine;

/// <summary>
/// This class contains the list of hinges.
/// </summary>
public class HingeManager : MonoBehaviour
{
    /// <summary>
    /// List of hinge objects.
    /// </summary>
    public ArrayList Hinges;

    /// <summary>
    /// This function will add a hinge to the list of hinges.
    /// </summary>
    /// <param name="h">
    /// The hinge object being added.
    /// </param>
    public void AddHinge(Rope h)
    {
        this.Hinges.Add(h);
    }

    // Start is called before the first frame update
    private void Start()
    {
        this.Hinges = new ArrayList();
    }
}
