// <copyright file="CloudMovement.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using UnityEngine;

/// <summary>
/// This class controls movement of the clouds.
/// </summary>
public class CloudMovement : MonoBehaviour
{
    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private GameObject[] cloudArray;

    private void Start()
    {
        this.cloudArray = GameObject.FindGameObjectsWithTag("cloud");
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        int x;
        for (x = 0; x < this.cloudArray.Length; x++)
        {
            this.cloudArray[x].transform.position += new Vector3(-0.01f * (float)Random.Range(1, 5), 0, 0);
        }
    }
}
