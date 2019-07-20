// <copyright file="InputClass.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
using UnityEngine;

/// <summary>
/// Class that contains data related to user input.
/// </summary>
public class InputClass : MonoBehaviour
{
    /// <summary>
    /// Camera object.
    /// </summary>
    public GameObject Camera;
    private float cameraStartPos; // starting position of camera
    private float currCameraPos; // current camera position
    private float cameraWidth; // width of camera
    private int clickFlag; // represents what side of the screen is clicked
    private float lastMousePosX;
    private float waitTime = 0.05f;
    private float timer = 0.0f;
    private float touchOffest;

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        // Debug.Log("Working");
        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log("Screen Height: " + Screen.height);

            // Debug.Log("Screen Input y pos: " + Input.mousePosition.y);

            this.currCameraPos = this.transform.position.x;

            float touchPositionX = Input.mousePosition.x - this.touchOffest + this.transform.position.x; // x position for touch
            float touchPositiony = Input.mousePosition.y - this.touchOffest + this.transform.position.y; // y position for y

            float middleVerticalBoundry = this.transform.position.x; // sets vertical boundary to divide touches
            float middleHorizontalBoundry = this.Camera.transform.position.y / 2; // sets horizontal boundary to divide touches

            if (touchPositionX > middleVerticalBoundry)
            {
                if (Input.mousePosition.y > Screen.height / 2)
                {
                    this.clickFlag = 2; // Top Right

                    // Debug.Log("Woah");
                }
                else
                {
                    this.clickFlag = 1; // Bottom Right
                }
            }
            else
            {
                this.clickFlag = 0; // Left
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            this.clickFlag = -1; // No Click
        }
    }

    /// <summary>
    /// Get the flag indicating input status.
    /// </summary>
    /// <returns>
    /// Return the flag indicating input status.
    /// </returns>
    public int GetInputFlag()
    {
        Debug.Log("Click Flag: " + this.clickFlag);
        return this.clickFlag;
    }

    // Start is called before the first frame update
    private void Start()
    {
        this.Camera = GameObject.FindWithTag("MainCamera"); // links camera object
        this.cameraStartPos = this.transform.position.x;
        this.cameraWidth = 1000;
        this.clickFlag = -1;
        if ((Application.platform == RuntimePlatform.IPhonePlayer) || SystemInfo.operatingSystem.Contains("Mac"))
        {
            this.touchOffest = 710f;
            Debug.Log("Running on iphone or mac");
        }
        else
        {
            this.touchOffest = 530f;
            Debug.Log("Running on pc");
        }
    }
}