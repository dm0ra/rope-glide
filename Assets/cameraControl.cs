using System.Collections;
using UnityEngine;

#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable SA1300 // Element should begin with upper-case letter
/// <summary>
/// cameraControl Handles the spawning in of gameobjects
/// based on how far the player has traveled.
/// It also handles the camera following the player.
/// </summary>
public class cameraControl : MonoBehaviour
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore IDE1006 // Naming Styles
{
#pragma warning disable SA1306 // Field names should begin with lower-case letter
#pragma warning disable SA1202 // Elements should be ordered by access
#pragma warning disable SA1401 // Fields should be private
    /// <summary>
    /// The main camera in the unity editor.
    /// </summary>
    public Camera Cam;

    /// <summary>
    /// Player gameobject from the unity editor.
    /// </summary>
    public GameObject Player;

    // position where player starts
    private static float startPos;

    // helper variable to determine distance
    private int Count100s;

    // second helper variable to determine distancec traveled
    private int Count50s;

    // width of background
    private int groundWidth = 294;

    // width of camera view
    private int cameraWidth = 228;

    // helper variable
    private int cameraHelper = 0;

    // boostSpawnChance / 10 to spawn a boost every x distance
    private int boostSpawnChance = 4;

    // helper variable
    private int startup = 1;

    // Current instance of GameController object
    private GameController game;

    // enemySpawnChance / 10 to spawn an enemy every x distance
    private int enemySpawnChance = 3;

    // distance between clouds
    private int blockSpawnDistance = 200;

    // varience in where the block spawns
    private int blockVar = 15;

    // height of camera
    private int camheight;

    // bottom of the campera position
    private float bottomCamPos;

    // Cam Vars

    // Delay variable for the camera following the player.
    private float dampTime ;

    // Velocity for the camera following the the player
    private Vector3 velocity = Vector3.zero;

    // The destination of the camera next position
    private Transform target;

    // Starting position of the backgroud
    private float BackgroundStartPos;

#pragma warning restore SA1306 // Field names should begin with lower-case letter
#pragma warning restore SA1202 // Elements should be ordered by access
#pragma warning restore SA1401 // Fields should be private
    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        this.game = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        this.camheight = 133;
        startPos = this.Player.transform.position.x;
        this.Count100s = 0;
        this.bottomCamPos = this.Cam.transform.position.y;
        this.dampTime = 0.025f;
        this.BackgroundStartPos = 277f;
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        float xpos = this.Player.transform.position.x;
        this.game.setDistance(xpos);
        this.game.setHighestHeight(this.Player.transform.position.y);
        this.game.setTopSpeed(this.Player.GetComponent<Rigidbody2D>().velocity);
        this.target = this.Player.transform;
        Vector3 point = this.Cam.WorldToViewportPoint(this.target.position);
        Vector3 delta = this.target.position - this.Cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); // (new Vector3(0.5, 0.5, point.z));
        Vector3 destination = this.transform.position + delta;

        // Set the vertical limit the player can go.
        if (destination.y < 105f)
        {
            destination = new Vector3(destination.x, 105f, destination.z);
        }

        // Set the horizontal limit the player can go.
        if (destination.x < 107f)
        {
            destination = new Vector3(107f, destination.y, destination.z);
        }

        // Smoothly have the camera follow the player.
        this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref this.velocity, this.dampTime);

        // Flag for first time update is called
        if (this.startup == 1)
        {
            this.startup = 0;
            if (Random.Range(0, 10) < this.enemySpawnChance)
            {
                GameObject mace = Instantiate(Resources.Load("Mace", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            }
        }

        // Every 100 position units the player travels
        if (startPos + 100 < this.Player.transform.position.x)
        {
            this.cameraHelper = (int)this.Player.transform.position.x;
            startPos = this.Player.transform.position.x;
            this.Count100s = this.Count100s + 1;

            // Every 200 position units the player travels
            if ((this.Count100s % 2) == 0)
            {
                // Spawn in cloud
                GameObject block = Instantiate(Resources.Load("Block1", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
                block.transform.position = new Vector3(this.Player.transform.position.x + Random.Range(this.blockSpawnDistance - this.blockVar, this.blockSpawnDistance + this.blockVar), Random.Range(192 - 5, 192 + 5), -5);
            }

            // Every 300 position units the player travels
            if ((this.Count100s % 3) == 0)
            {
                // Spawn in cloud
                GameObject block2 = Instantiate(Resources.Load("Block1", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
                block2.transform.position = new Vector3(this.Player.transform.position.x + Random.Range(this.blockSpawnDistance - this.blockVar, this.blockSpawnDistance + this.blockVar), Random.Range(450 - 50, 450 + 50), -5);
            }

            // Every 400 position units the player travels
            if ((this.Count100s % 4) == 0)
            {
                // Spawn in cloud
                GameObject block3 = Instantiate(Resources.Load("Block1", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
                block3.transform.position = new Vector3(this.Player.transform.position.x + Random.Range(this.blockSpawnDistance - this.blockVar, this.blockSpawnDistance + this.blockVar), Random.Range(680 - 50, 680 + 50), -5);
            }

            // Object prefab1 = AssetDatabase.LoadAssetAtPath("Assets/prefab/Sky_.prefab", typeof(GameObject));
            GameObject background = Instantiate(Resources.Load("Background", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            background.transform.position = new Vector3(this.BackgroundStartPos + 808f, 133f, 0);
            this.BackgroundStartPos = this.BackgroundStartPos + 808f;

            // generates mace with enemySpawnChance/10 chance
            if (Random.Range(0, 10) < this.enemySpawnChance)
                {
                    GameObject mace = Instantiate(Resources.Load("Mace", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
                }

            // generates boost with enemySpawnChance/10 chance
            if (Random.Range(0, 10) > this.boostSpawnChance)
                {
                    GameObject pickUpItem = Instantiate(Resources.Load("Boost", typeof(GameObject))) as GameObject;
                    pickUpItem.transform.position = pickUpItem.transform.position + new Vector3(0, 0, -25);
                }
            }

        // End game
        if (this.Count100s == 500)
        {
            // Object prefab3 = AssetDatabase.LoadAssetAtPath("Assets/prefab/Finish.prefab", typeof(GameObject));
            GameObject finishLine = Instantiate(Resources.Load("Finish", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            finishLine.transform.position = new Vector3(startPos + 300, 82.1f, 0);
        }
    }
}