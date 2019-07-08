using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using System;
using UnityEngine.UI;

using UnityEngine.Experimental.UIElements;

using UnityEngine.SceneManagement;
using System.IO;

using Microsoft.VisualBasic;
using System.Globalization;



public class GameController : MonoBehaviour
{
    /* A list of all the clouds (hinges) that the player can connect to */
    public List<Rope> Hinges;
    /* List of pick of items (Boost,...) */
    public List<PickUpItem> Items;
    /* List of Enemies */
    public List<Enemies> EnemyList;
    /* Holds the index of the connected hinge */
    public int isConnected;
    /* The games camera (See in unity editor) */
    public GameObject Camera;
    /* The games player (See in unity editor) */
    public GameObject Player;
    /* Object to hold a booster when upgraded */
    public GameObject Booster;
    /* Flag that is set when the player is connected to a cloud */
    public bool Connected;
    /* SceneSwitch controlls the switching of menus */
    public SceneSwitch sceneSwitcher;
    /* Used to correct the coordinates of the cloud (hinge) */
    private float xOffset;
    /* Used to correct the coordinates of the cloud (hinge) */
    private float yOffset;
    /* On screen score  */
    public Text score;
    /* Holds the current score */
    public float scoreCount;
    /* tallys the number of connections */
    public int connectionCount;
    /*  Holds the highest hight recorded by player */
    public float highestHeight;
    /* Holds the greatest speed recorded by player */
    public float topSpeed;
    /*  Holds the greatest distance recorded by player */
    public float distance;
    /* Flag that is set when the player falls off */
    private bool fellOffFlag; 

    string highScoreFilePath = " Data\\HighScore.csv";
       
    Vector3 StartingCameraPos;
     
    public bool JumpClick;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Succsessfully read High score Data ");

        //Load the highscore and set highscore upon each life
        string tempHiScore =  DataSaver.loadData<string>("HighScore");
        DB.HighScore = float.Parse(tempHiScore);
             
        //Initilize instance variables

        xOffset = 19.2f;
        yOffset = 67.9f;
        sceneSwitcher = new SceneSwitch();
        Hinges = new List<Rope>();
        Items = new List<PickUpItem>();
        EnemyList = new List<Enemies>();
        Player = GameObject.FindWithTag("Player");
        Camera = GameObject.FindWithTag("MainCamera");
        Connected = false;
        JumpClick = false;
        scoreCount = 0;
        connectionCount = 0;
        distance = 0;
        highestHeight = 0;
        topSpeed = 0; 
        fellOffFlag = false;
        initScore();

        //Freeze the player upon each life. Taping or clicking makes the player start moving
        Player.GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionX;
        Player.GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionY;
    }

    //Runs once a frame
    void Update()
    {
        //
        checkForEnter();
        //
        updateScore();
    
        scoreCount = calculateScore();

        //If the player falls off the map respawn player
        if (StartingCameraPos.y - 92 > Player.transform.position.y)
        { 
            respawnPlayer(); 
        }
        
         
        //Wait for first click to move the player
        if(Input.GetMouseButtonDown(0))
        {
            if (!JumpClick)
            {
                this.JumpClick = true;
                Player.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;
                Player.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            }
        }   
    }


    public void checkForEnter()
    {
        if (Input.GetKeyDown("space"))
        {
            if(sceneSwitcher.getSceneIndex() == 2)
            {
                sceneSwitcher.switchScenes(0);
            } 
        } 
        StartingCameraPos = Camera.transform.position; 
    }

    //Has the player clicked/tapped the screen for the first time
    public bool GetJumpClick()
    {
        return this.JumpClick;
    }
    public void SetJumpClick(bool b)
    {
        this.JumpClick = b;

    }

    //Returns index
    public int AddHinge(Rope H)
    {
        Hinges.Add(H);
        return Hinges.Count - 1;
    }

    public void incrementScore()    //used to calculate score
    {
        scoreCount++;
    }

    public void incrementConnections()  //used to calculate score
    {
        connectionCount++;
    }

    public void setTopSpeed(Vector2 ts)   //used to calculate score
    {
        if(ts.magnitude > topSpeed)
        {
            topSpeed = ts.magnitude;
        }
        
    }

    public void setHighestHeight(float h)   //used to calculate score
    {
        if(h > highestHeight)
        {
            highestHeight = h;
        }

    }
    //Set new distance highscore
    public void setDistance(float d)
    {
        if(d > distance)
        {
            distance = d;
        }
    }
    //calculate overall score
    public float calculateScore()
    {
        return (float)connectionCount + topSpeed/10 + highestHeight + distance/100;
    }
    //Has the person fallen off
    public bool getfellOffFlag()   
    {
        return fellOffFlag;
    }
    //set if the person has fell off
    public void setfellOffFlag(bool flag)
    {
        fellOffFlag = flag;
    }

    //InitScore sets up the on screen score
    public void initScore()
    {
        Font arial;
        arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

        // Create Canvas GameObject.
        GameObject canvasGO = new GameObject();
        canvasGO.name = "Canvas";
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
        score = textGO.GetComponent<Text>();
        score.font = arial;
        score.fontSize = 24;
        score.alignment = TextAnchor.MiddleCenter;
        score.color = Color.black;

        // Provide Text position and size using RectTransform.

        score.transform.position = new Vector3(850, 520, -5);

        //On Screen score for iphone
        //score.transform.position = new Vector3(1350, 820, -5);
    }
    // Constantly updates the score on screen
    public void updateScore()
    {
        score.text = "score: " + (int)scoreCount;
        //Debug.Log(score.text + scoreCount)
    }

    //adds pickup item to list and returns index
    public int AddItem(PickUpItem I)
    {
        Items.Add(I);

        return Hinges.Count - 1;
    }
    //adds enemy item to list and returns index
    public int AddEnemy(Enemies E)
    {
        EnemyList.Add(E);
        return Hinges.Count - 1;
    }
    //Sets connected flag and sets the index of the cloud that is connected
    public void setIsConnected(Rope rope)
    {
        Connected = true;
        this.isConnected = Hinges.IndexOf(rope);
    }
    //Sets connected flag
    public void SetConnectedFlag(bool s)
    {
        this.Connected = s;
    }
    //gets connected index
    public bool GetConnectedFlag()
    {
        return this.Connected;
    }
    //gets connected flag
    public int getConnected()
    {
        return isConnected;
    }
    //This function is called in Rope.
    public bool IsHingeClosest(int index)
    {
        bool ret = true;
        float currDistance;
        float distanceInQuestion;
        Vector2 NextVector;
        Vector2 PlayerVector = new Vector2(Player.transform.position.x, Player.transform.position.y);
        Vector2 HingeVector = new Vector2(Hinges[index].transform.position.x, Hinges[index].transform.position.y);

        distanceInQuestion = Vector2.Distance(PlayerVector, HingeVector);

        for (int x = 0; x < Hinges.Count; x++)
        {
            if (x != index)
            {
                NextVector = new Vector2(Hinges[x].transform.position.x, Hinges[x].transform.position.y);

                currDistance = Vector2.Distance(PlayerVector, NextVector);

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

    Vector3 CorrectedHingePosition(int index)
    {
        return new Vector3(Hinges[index].Hinge.transform.position.x - xOffset, Hinges[index].Hinge.transform.position.y - yOffset,
            Hinges[index].Hinge.transform.position.z);

    }

    

    public void respawnPlayer()
    {

        //Player.transform.position = new Vector3(-25, 127, -5);
        //Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        DB.Score = calculateScore();
        if(DB.Score > DB.HighScore)
        {
            //saves highscore to csv file
            DB.HighScore = scoreCount;
            var csv = new System.Text.StringBuilder();
            var highScoreString = DB.HighScore.ToString();
            var newLine = string.Format(highScoreString);
             csv.AppendLine(newLine);

            DataSaver.saveData<string>(csv.ToString(), "HighScore");
             //File.WriteAllText("Data\\HighScore.csv", csv.ToString());

        }
        scoreCount = 0;
        //Destroy(Player);
        fellOffFlag = true;
        for(int i = 0; i < Hinges.Count; i++)
        {
            Hinges[i].setFirstConnection(true);
        }
        DB.LvlIndex = 2;
        sceneSwitcher.switchScenes(2);
        //SceneManager.LoadScene(2);
    }

    private void garbageMan()
    {
        if (EnemyList.Count > 10)
        {
            for (var i = 0; i < EnemyList.Count - 10; i++)
            {
                Destroy(EnemyList[i]);
            }
        }

    }
    private void OnLevelWasLoaded(int level)
    {
        upgradeMenuActions();
    }

    public void upgradeMenuActions()
    {
        if (Upgrades.Glider == 1)
        {
            GameObject gldr = Instantiate(Resources.Load("Glider", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject; 
                
        }
        if(Upgrades.Booster == 1)
        {
            GameObject Booster = Instantiate(Resources.Load("Booster", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
        }
    }
}

