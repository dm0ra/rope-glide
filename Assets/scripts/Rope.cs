// <copyright file="Rope.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using UnityEngine;

/// <summary>
/// This class creates ropes. The code decides which cloud
/// is closest to the player and connects a rope to the cloud on click/tap
/// and hold.The rope holds the player, who uses the rope to progress through
/// the game by swinging until the player swings too close to the ground, hits
/// by picking up coins by swinging through them.
/// </summary>
public class Rope : MonoBehaviour
{
    /// <summary>
    /// The joint that holds the player and the cloud at a constant distance.
    /// </summary>
    public HingeJoint2D CurrRope;

    /// <summary>
    /// The games player.
    /// </summary>
    public GameObject Player;

    /// <summary>
    /// Each instance of Rope belongs to a cloud (hinge).
    /// </summary>
    public GameObject Hinge;

    /// <summary>
    /// Mouse state 1 = clicked    0 = not clicke
    /// </summary>
    private static int mouseState;

    /// <summary>
    /// Input class that shows where on the screen input is coming from.
    /// </summary>
    private InputClass gameInput;

    /// <summary>
    /// Game controller instance. See GameController class.
    /// </summary>
    public GameController game;

    /// <summary>
    /// Index of the current hinge in the GameController class.
    /// </summary>
    private int index;

    /// <summary>
    /// The visual to the rope connection
    /// </summary>
    private GameObject VisualRope;

    /// <summary>
    /// Used to correct the coordinates of the cloud (hinge)
    /// </summary>
    private float xOffset;

    /// <summary>
    /// Used to correct the coordinates of the cloud (hinge)
    /// </summary>
    private float yOffset;

    /// <summary>
    /// Flag to ignore first click, because the first click tells the player to jump.
    /// </summary>
    private bool firstConnection;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        xOffset = 19.2f;
        yOffset = 67.9f;
        mouseState = -1;
        Player = GameObject.FindWithTag("Player");
        game = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        index = game.AddHinge(this);
        firstConnection = true;
        //bool a = CurrRope.isActiveAndEnabled;
        gameInput = GameObject.FindWithTag("MainCamera").GetComponent<InputClass>();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        Debug.Log("DB Glider: " + DB.Glider);

        // Form the visable rope you see on screen if the mouse state is 1 (left click held down),
        // The index (the index of THIS connection point in the array of connection points)
        // of the cloud that is being swung from is the same as the one in the game controller
        // (game.getConnected()), and the game controller has a flag set with (game.GetConnectedFlag()).
        if (mouseState == 1 && index == game.getConnected() && game.GetConnectedFlag())
        {
            // Transform.position gives the transform of the gameobject
            // This script is connected to in the unity editor.
            Vector3 temp = (Player.transform.position + transform.position);
            Vector3 tempAng = (Player.transform.position - transform.position);

            // temp now holds the coordinates at the half way point between the point of
            // connection and the player, using the midpoint formula
            temp *= 0.5f;


            float angle = Mathf.Atan2(tempAng.y, tempAng.x) * Mathf.Rad2Deg;

            //Create/Update the ropes scale, posiition and angle.
            VisualRope.transform.position = new Vector3(temp.x, temp.y, -5);
            VisualRope.transform.localScale = new Vector3(.25f, tempAng.magnitude / 10f, .25f);
            VisualRope.transform.eulerAngles = new Vector3(0, 0, angle + 90);
        }

        /*
         * This if statement is run on first press and connects the player to the nearest connection point.
         * It relies on the following to be true:
         * game.GetJumpClick()  -  
         * Input.GetMouseButtonDown(0) - Left mouse click down (also works for touch)
         * game.IsHingeClosest(index)  - Looks through an array of all possible connection points
         * !game.GetConnectedFlag()
         */
        if (game.GetJumpClick() && Input.GetMouseButtonDown(0) && game.IsHingeClosest(index) && !game.GetConnectedFlag())
        {
            // If the glider is active, connect only when the left half of the screen is pressed

            if (DB.Glider == 1)
            {
                Debug.Log(gameInput.getInputFlag());
                if (gameInput.getInputFlag() == 0) // Only connect when left side of the screen is pressed
                {
                    Connect();
                }
            }
            // Connect on any input when no upgrades are active.
            if (DB.Glider == 0)
            {
                Connect();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            game.SetConnectedFlag(false);
            CurrRope.connectedBody = null;
            mouseState = 0;
            Destroy(VisualRope);

        }
    }

    Vector3 CorrectedHingePosition()
    {
        return new Vector3(Hinge.transform.position.x - xOffset, Hinge.transform.position.y - yOffset,
            Hinge.transform.position.z);

    }

    void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        myLine.GetComponent<LineRenderer>().sortingLayerName = "Default";
        myLine.GetComponent<LineRenderer>().sortingOrder = 1;
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Specular"));
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = 0.5f;
        lr.endWidth = 0.5f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, .01f);
    }

    public void SetFirstConnection(bool b)
    {
        firstConnection = b;
    }

    private void Connect()
    {
        if (mouseState != 1)
        {
            if (firstConnection)
            {
                firstConnection = false;
                game.incrementConnections();
            }
            VisualRope = Instantiate(Resources.Load("VisualRope", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            // Modify the clone to your heart's content
            Vector3 temp = (Player.transform.position + CorrectedHingePosition()) * 0.5f;
            VisualRope.transform.position = new Vector3(temp.x, temp.y, -5);
            game.setIsConnected(this);
            //Vector2 vel = Player.GetComponent<Rigidbody2D>().velocity + (12 * Player.GetComponent<Rigidbody2D>().velocity.normalized);
            CurrRope.connectedBody = Player.GetComponent<Rigidbody2D>();
        }
        mouseState = 1;
    }
}