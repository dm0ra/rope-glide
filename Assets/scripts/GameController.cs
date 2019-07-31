// <copyright file="GameController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.VisualBasic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// GameController is being updated when the game is being played
/// it handles death of the player and scene change. it also...
/// </summary>
public class GameController : MonoBehaviour
{
#pragma warning disable SA1306 // Field names should begin with lower-case letter
#pragma warning disable SA1202 // Elements should be ordered by access
#pragma warning disable SA1401 // Fields should be private

#pragma warning disable CA1051 // Do not declare visible instance fields
    /// <summary>
    /// A list of all the clouds (hinges) that the player can connect to.
    /// </summary>
    public List<Rope> Hinges;
#pragma warning restore CA1051 // Do not declare visible instance fields

#pragma warning disable CA1051 // Do not declare visible instance fields
    /// <summary>
    /// List of pick of items (Boost,...).
    /// </summary>
    public List<PickUpItem> Items;
#pragma warning restore CA1051 // Do not declare visible instance fields

#pragma warning disable CA1051 // Do not declare visible instance fields
    /// <summary>
    /// List of Enemies.
    /// </summary>
    public List<Enemies> EnemyList;
#pragma warning restore CA1051 // Do not declare visible instance fields

#pragma warning disable SA1307 // Accessible fields should begin with upper-case letter
#pragma warning disable CA1051 // Do not declare visible instance fields
    /// <summary>
    /// Holds the index of the connected hinge.
    /// </summary>
    public int isConnected;
#pragma warning restore CA1051 // Do not declare visible instance fields
#pragma warning restore SA1307 // Accessible fields should begin with upper-case letter

#pragma warning disable CA1051 // Do not declare visible instance fields
    /// <summary>
    /// The games camera (See in unity editor).
    /// </summary>
    public GameObject Camera;
#pragma warning restore CA1051 // Do not declare visible instance fields

#pragma warning disable CA1051 // Do not declare visible instance fields
    /// <summary>
    /// The games player (See in unity editor).
    /// </summary>
    public GameObject Player;
#pragma warning restore CA1051 // Do not declare visible instance fields

#pragma warning disable CA1051 // Do not declare visible instance fields
    /// <summary>
    /// Object to hold a booster when upgraded.
    /// </summary>
    public GameObject Booster;
#pragma warning restore CA1051 // Do not declare visible instance fields

    // Flag that is set when the player is connected to a cloud
    private bool Connected;

    // SceneSwitch controlls the switching of menus
    private SceneSwitch sceneSwitcher;

    // Used to correct the coordinates of the cloud (hinge)
    private float xOffset;

    // Used to correct the coordinates of the cloud (hinge)
    private float yOffset;

    /// <summary>
    /// On screen score.
    /// </summary>
    private Text score;

    // Holds the current score
    private float scoreCount;

    // tallys the number of connections
    private int connectionCount;

    // Holds the highest hight recorded by player
    private float highestHeight;

    // Holds the greatest speed recorded by player
    private float topSpeed;

    // Holds the greatest distance recorded by player
    private float distance;

    // Flag that is set when the player falls off
    private bool fellOffFlag;

    // Starting position of main camera in unity editor
    private Vector3 StartingCameraPos;

#pragma warning disable CA1051 // Do not declare visible instance fields
    /// <summary>
    /// Tells whether the initial click occurred.
    /// </summary>
    public bool JumpClick;
#pragma warning restore CA1051 // Do not declare visible instance fields

    /// <summary>
    /// Debug method for scene swithching.
    /// </summary>
    public void CheckForEnter()
    {
        if (Input.GetKeyDown("space"))
        {
            if (this.sceneSwitcher.GetSceneIndex() == 2)
            {
                this.sceneSwitcher.SwitchScenes(0);
            }
        }

        this.StartingCameraPos = this.Camera.transform.position;
    }

    /// <summary>
    /// Has the player clicked/tapped the screen for the first time.
    /// </summary>
    /// <returns>Jump click flag.</returns>
    public bool GetJumpClick()
    {
        return this.JumpClick;
    }

    /// <summary>
    /// Set the initial jump click flag.
    /// </summary>
    /// <param name="b">b is boolean if the player has clicked for the
    /// first time.</param>
    public void SetJumpClick(bool b)
    {
        this.JumpClick = b;
    }

    /// <summary>
    ///  Add rope to the list of hinges.
    /// </summary>
    /// <param name="h">The rope to add to the list of hinges.</param>
    /// <returns>the index of the. </returns>
    public int AddHinge(Rope h)
    {
        this.Hinges.Add(h);
        return this.Hinges.Count - 1;
    }

#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable SA1300 // Element should begin with upper-case letter
    /// <summary>
    ///  used to calculate score.
    /// </summary>
    public void incrementScore()
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore IDE1006 // Naming Styles
    {
        this.scoreCount++;
    }

#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable SA1300 // Element should begin with upper-case letter
    /// <summary>
    /// used to calculate score.
    /// </summary>
    public void incrementConnections()
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore IDE1006 // Naming Styles
    {
        this.connectionCount++;
    }

#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable SA1300 // Element should begin with upper-case letter
    /// <summary>
    /// used to calculate score.
    /// </summary>
    /// <param name="ts">Current vector for speed.</param>
    public void setTopSpeed(Vector2 ts)
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore IDE1006 // Naming Styles
    {
        if (ts.magnitude > this.topSpeed)
        {
            this.topSpeed = ts.magnitude;
        }
    }

#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable SA1300 // Element should begin with upper-case letter
    /// <summary>
    /// Set the players highest height.
    /// </summary>
    /// <param name="h">height h.</param>
    public void setHighestHeight(float h) // used to calculate score
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore IDE1006 // Naming Styles
    {
        if (h > this.highestHeight)
        {
            this.highestHeight = h;
        }
    }

#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable SA1300 // Element should begin with upper-case letter
    /// <summary>
    /// Set new distance highscore.
    /// </summary>
    /// <param name="d">distance d.</param>
    public void setDistance(float d)
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore IDE1006 // Naming Styles
    {
        if (d > this.distance)
        {
            this.distance = d;
        }
    }

    /// <summary>
    ///  calculate overall score.
    /// </summary>
    /// <returns>The current score of the player.</returns>
    public float CalculateScore()
    {
        return (float)this.connectionCount + (this.topSpeed / 10) + this.highestHeight + (this.distance / 100);
    }

    /// <summary>
    ///  Has the person fallen off.
    /// </summary>
    /// <returns>has the player fallen off.</returns>
    public bool GetfellOffFlag()
    {
        return this.fellOffFlag;
    }

#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable SA1300 // Element should begin with upper-case letter
    /// <summary>
    /// set if the person has fell off.
    /// </summary>
    /// <param name="flag">flag for fallen off.</param>
    public void setfellOffFlag(bool flag)
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore IDE1006 // Naming Styles
    {
        this.fellOffFlag = flag;
    }

    /// <summary>
    ///  InitScore sets up the on screen score.
    /// </summary>
    public void InitScore()
    {
        Font arial;
        arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

        // Create Canvas GameObject.
        GameObject canvasGO = new GameObject
        {
            name = "Canvas"
        };
        canvasGO.AddComponent<Canvas>();
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        // Get canvas from the GameObject.
        Canvas canvas;
        canvas = canvasGO.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // Create the Text GameObject.
        GameObject textGO = new GameObject();
        textGO.transform.parent = canvasGO.transform;
        textGO.AddComponent<Text>();

        // Set Text component properties.
        this.score = textGO.GetComponent<Text>();
        this.score.font = arial;
        this.score.fontSize = 24;
        this.score.alignment = TextAnchor.MiddleCenter;
        this.score.color = Color.black;

        // Provide Text position and size using RectTransform.
        this.score.transform.position = new Vector3(850, 520, -5);

        // On Screen score for iphone
        // score.transform.position = new Vector3(1350, 820, -5);
    }

    /// <summary>
    ///  Constantly updates the score on screen.
    /// </summary>
    public void UpdateScore()
    {
        this.score.text = "score: " + (int)this.scoreCount;

        // Debug.Log(score.text + scoreCount)
    }

    /// <summary>
    /// adds pickup item to list and returns index.
    /// </summary>
    /// <param name="i">The item to be added.</param>
    /// <returns>The index of the item.</returns>
    public int AddItem(PickUpItem i)
    {
        this.Items.Add(i);

        return this.Hinges.Count - 1;
    }

    /// <summary>
    /// adds enemy item to list and returns index.
    /// </summary>
    /// <param name="e">enemy to be added.</param>
    /// <returns>the indexof the item added.</returns>
    public int AddEnemy(Enemies e)
    {
        this.EnemyList.Add(e);
        return this.Hinges.Count - 1;
    }

    /// <summary>
    /// Sets connected flag and sets the index of the cloud that is connected.
    /// </summary>
    /// <param name="rope">The rope that is connected.</param>
    public void SetIsConnected(Rope rope)
    {
        this.Connected = true;
        this.isConnected = this.Hinges.IndexOf(rope);
    }

    /// <summary>
    /// Sets connected flag.
    /// </summary>
    /// <param name="s">connected flag.</param>
    public void SetConnectedFlag(bool s)
    {
        this.Connected = s;
    }

    /// <summary>
    ///  gets connected index.
    /// </summary>
    /// <returns>true if the player is connected.</returns>
    public bool GetConnectedFlag()
    {
        return this.Connected;
    }

    /// <summary>
    /// gets connected flag.
    /// </summary>
    /// <returns>Gets the connected flag.</returns>
    public int GetConnected()
    {
        return this.isConnected;
    }

    /// <summary>
    /// This function is called in Rope.
    /// </summary>
    /// <param name="index">index to check if this cloud is the closest.</param>
    /// <returns>If this index is the closest.</returns>
    public bool IsHingeClosest(int index)
    {
        bool ret = true;
        float currDistance; // current distance
        float distanceInQuestion; // distance of hinge in question
        Vector2 nextVector; // next hinge to look at
        Vector2 playerVector = new Vector2(this.Player.transform.position.x, this.Player.transform.position.y); // gets player 2d vector
        Vector2 hingeVector = new Vector2(this.Hinges[index].transform.position.x, this.Hinges[index].transform.position.y); // gets the current hinge 2d vector

        distanceInQuestion = Vector2.Distance(playerVector, hingeVector); // finds the distance between the current hinge and the play

        // loops through all the hinges
        for (int x = 0; x < this.Hinges.Count; x++)
        {
            // makes sure not to check the current hinge in question
            if (x != index)
            {
                nextVector = new Vector2(this.Hinges[x].transform.position.x, this.Hinges[x].transform.position.y);
                currDistance = Vector2.Distance(playerVector, nextVector);

                if (distanceInQuestion < currDistance)
                {
                    continue;
                }
                else
                {
                    ret = false;
                    break;
                }
            }
        }

        return ret;
    }

    /// <summary>
    /// Respawn player sets the highscore if needed.
    /// </summary>
    public void RespawnPlayer()
    {
        // Player.transform.position = new Vector3(-25, 127, -5);
        // Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        DB.Score = this.CalculateScore();
        DB.MaxSpeed = this.topSpeed;
        DB.RunDist = this.distance;
        DB.RunConNum = this.connectionCount;
        DB.MaxHeight = this.highestHeight;
        DB.BankCash = DB.BankCash + ((int)DB.Score / 10);
        if (DB.Score > DB.HighScore)
        {
            // saves highscore and cash to csv file
            DB.HighScore = this.scoreCount;
            this.WriteData();
        }
        else
        {
            this.WriteData();
        }

        this.scoreCount = 0;
        this.fellOffFlag = true;
        for (int i = 0; i < this.Hinges.Count; i++)
        {
            this.Hinges[i].SetFirstConnection(true);
        }

        DB.LvlIndex = 2;
        this.sceneSwitcher.SwitchScenes(2);
    }

#pragma warning restore SA1306 // Field names should begin with lower-case letter
#pragma warning restore SA1202 // Elements should be ordered by access
#pragma warning restore SA1401 // Fields should be private
    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Awake()
    {
        
        // Load the highscore and set highscore upon each life
        string fileData = DataSaver.LoadData<string>("HighScore");
        string[] fileLines = fileData.Split('\n');
#pragma warning disable CA1305 // Specify IFormatProvider
        DB.HighScore = float.Parse(fileLines[0]);
#pragma warning restore CA1305 // Specify IFormatProvider
#pragma warning disable CA1305 // Specify IFormatProvider
        DB.BankCash = int.Parse(fileLines[1]);
#pragma warning restore CA1305 // Specify IFormatProvider
#pragma warning disable CA1305 // Specify IFormatProvider
        DB.Glider = int.Parse(fileLines[2]);
#pragma warning restore CA1305 // Specify IFormatProvider
#pragma warning disable CA1305 // Specify IFormatProvider
        DB.Booster = int.Parse(fileLines[3]);
#pragma warning restore CA1305 // Specify IFormatProvider

        // Initilize instance variables
        this.xOffset = 19.2f;
        this.yOffset = 67.9f;
        this.sceneSwitcher = new SceneSwitch();
        this.Hinges = new List<Rope>();
        this.Items = new List<PickUpItem>();
        this.EnemyList = new List<Enemies>();
        this.Player = GameObject.FindWithTag("Player");
        this.Camera = GameObject.FindWithTag("MainCamera");
        this.Connected = false;
        this.JumpClick = false;
        this.scoreCount = 0;
        this.connectionCount = 0;
        this.distance = 0;
        this.highestHeight = 0;
        this.topSpeed = 0;
        this.fellOffFlag = false;
        this.InitScore();

        // Freeze the player upon each life. Taping or clicking makes the player start moving
        this.Player.GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionX;
        this.Player.GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionY;

        // links preview images for booster and glider then deactivates them
        // gliderImage = GameObject.FindWithTag("GliderImage");
        // boosterImage = GameObject.FindWithTag("BoosterImage");
        // gliderImage.SetActive(false);
        // boosterImage.SetActive(false);
    }

    // Runs once a frame
    private void Update()
    {
        this.CheckForEnter();

        this.UpdateScore();

        this.scoreCount = this.CalculateScore();

        // If the player falls off the map respawn player
        if (this.StartingCameraPos.y - 92 > this.Player.transform.position.y)
        {
            this.RespawnPlayer();
        }

        // Wait for first click to move the player
        if (Input.GetMouseButtonDown(0))
        {
            if (!this.JumpClick)
            {
                this.JumpClick = true;
                this.Player.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;
                this.Player.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            }
        }
    }

    /// <summary>
    /// Is the corrected position of hinges.
    /// </summary>
    /// <param name="index">index to cloud to be corrected.</param>
    /// <returns>The corrected position of the cloud.</returns>
    private Vector3 CorrectedHingePosition(int index)
    {
        return new Vector3(this.Hinges[index].Hinge.transform.position.x - this.xOffset, this.Hinges[index].Hinge.transform.position.y - this.yOffset, this.Hinges[index].Hinge.transform.position.z);
    }

#pragma warning disable CA1822 // Mark members as static
    /// <summary>
    /// writes to file game information.
    /// </summary>
    private void WriteData()
#pragma warning restore CA1822 // Mark members as static
    {
        // saves highscore and cash to csv file
        var csv = new System.Text.StringBuilder();
#pragma warning disable CA1305 // Specify IFormatProvider
        var highScoreString = DB.HighScore.ToString();
#pragma warning restore CA1305 // Specify IFormatProvider
#pragma warning disable CA1305 // Specify IFormatProvider
        var newLine = string.Format(highScoreString);
#pragma warning restore CA1305 // Specify IFormatProvider
#pragma warning disable CA1305 // Specify IFormatProvider
        var cashString = DB.BankCash.ToString();
#pragma warning restore CA1305 // Specify IFormatProvider
#pragma warning disable CA1305 // Specify IFormatProvider
        var glideString = DB.Glider.ToString();
#pragma warning restore CA1305 // Specify IFormatProvider
#pragma warning disable CA1305 // Specify IFormatProvider
        var boostString = DB.Booster.ToString();
#pragma warning restore CA1305 // Specify IFormatProvider
        csv.AppendLine(newLine);
        csv.AppendLine(cashString);
        csv.AppendLine(glideString);
        csv.AppendLine(boostString);
        DataSaver.SaveData<string>(csv.ToString(), "HighScore");
    }

    /// <summary>
    /// Check for upgrades each time the game is started.
    /// </summary>
    /// <param name="level">current level.</param>
    private void OnLevelWasLoaded(int level)
    {
        this.UpgradeMenuActions();
    }

#pragma warning disable CA1822 // Mark members as static
    /// <summary>
    /// See which upgrades have been puchesed and spawn them in.
    /// </summary>
    private void UpgradeMenuActions()
#pragma warning restore CA1822 // Mark members as static
    {
        if (DB.Glider == 1)
        {
            // Spawn in glider
            GameObject gldr = Instantiate(Resources.Load("Glider", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
        }

        if (DB.Booster == 1)
        {
            // Spawn in booster
            GameObject booster = Instantiate(Resources.Load("Booster", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
        }
    }
}
