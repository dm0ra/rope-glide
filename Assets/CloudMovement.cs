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
    GameObject[] cloudArray;


    void Start()
    {
        cloudArray = GameObject.FindGameObjectsWithTag("cloud");
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        int x;
        for (x = 0; x < cloudArray.Length; x++)
        {
            cloudArray[x].transform.position += new Vector3(-0.01f * (float)Random.Range(1, 5), 0, 0);
        }
    }
}
