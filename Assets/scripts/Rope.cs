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
#pragma warning disable SA1401 // Fields should be private
#pragma warning disable CA1051 // Do not declare visible instance fields
    /// <summary>
    /// The joint that holds the player and the cloud at a constant distance.
    /// </summary>
    public HingeJoint2D CurrRope;
#pragma warning restore CA1051 // Do not declare visible instance fields
#pragma warning restore SA1401 // Fields should be private

#pragma warning disable SA1401 // Fields should be private
#pragma warning disable CA1051 // Do not declare visible instance fields
    /// <summary>
    /// The games player.
    /// </summary>
    public GameObject Player;
#pragma warning restore CA1051 // Do not declare visible instance fields
#pragma warning restore SA1401 // Fields should be private

#pragma warning disable SA1401 // Fields should be private
#pragma warning disable CA1051 // Do not declare visible instance fields
    /// <summary>
    /// Each instance of Rope belongs to a cloud (hinge).
    /// </summary>
    public GameObject Hinge;
#pragma warning restore CA1051 // Do not declare visible instance fields
#pragma warning restore SA1401 // Fields should be private

#pragma warning disable SA1307 // Accessible fields should begin with upper-case letter
#pragma warning disable SA1401 // Fields should be private
#pragma warning disable CA1051 // Do not declare visible instance fields
    /// <summary>
    /// Game controller instance. See GameController class.
    /// </summary>
    public GameController game;
#pragma warning restore CA1051 // Do not declare visible instance fields
#pragma warning restore SA1401 // Fields should be private
#pragma warning restore SA1307 // Accessible fields should begin with upper-case letter

    /// <summary>
    /// Mouse state 1 = clicked    0 = not clicke.
    /// </summary>
    private static int mouseState;

    /// <summary>
    /// Input class that shows where on the screen input is coming from.
    /// </summary>
    private InputClass gameInput;

    /// <summary>
    /// Index of the current hinge in the GameController class.
    /// </summary>
    private int index;

    /// <summary>
    /// The visual to the rope connection.
    /// </summary>
    private GameObject visualRope;

    /// <summary>
    /// Used to correct the coordinates of the cloud (hinge).
    /// </summary>
    private float xOffset;

    /// <summary>
    /// Used to correct the coordinates of the cloud (hinge).
    /// </summary>
    private float yOffset;

    /// <summary>
    /// Flag to ignore first click, because the first click tells the player to jump.
    /// </summary>
    private bool firstConnection;

    /// <summary>
    /// This method signals whether the first connection has occurred.
    /// </summary>
    /// <param name="b">bool b.</param>
    public void SetFirstConnection(bool b)
    {
        this.firstConnection = b;
    }

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        this.xOffset = 19.2f;
        this.yOffset = 67.9f;
        mouseState = -1;
        this.Player = GameObject.FindWithTag("Player");
        this.game = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        this.index = this.game.AddHinge(this);
        this.firstConnection = true;

        // bool a = CurrRope.isActiveAndEnabled;
        this.gameInput = GameObject.FindWithTag("MainCamera").GetComponent<InputClass>();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Update()
    {
        // Form the visable rope you see on screen if the mouse state is 1 (left click held down),
        // The index (the index of THIS connection point in the array of connection points)
        // of the cloud that is being swung from is the same as the one in the game controller
        // (game.getConnected()), and the game controller has a flag set with (game.GetConnectedFlag()).
        if (mouseState == 1 && this.index == this.game.GetConnected() && this.game.GetConnectedFlag())
        {
            // Transform.position gives the transform of the gameobject
            // This script is connected to in the unity editor.
            Vector3 temp = this.Player.transform.position + this.transform.position;
            Vector3 tempAng = this.Player.transform.position - this.transform.position;

            // temp now holds the coordinates at the half way point between the point of
            // connection and the player, using the midpoint formula
            temp *= 0.5f;
            float angle = Mathf.Atan2(tempAng.y, tempAng.x) * Mathf.Rad2Deg;

            // Create/Update the ropes scale, posiition and angle.
            this.visualRope.transform.position = new Vector3(temp.x, temp.y, -5);
            this.visualRope.transform.localScale = new Vector3(.25f, tempAng.magnitude / 10f, .25f);
            this.visualRope.transform.eulerAngles = new Vector3(0, 0, angle + 90);
        }

         // This if statement is run on first press and connects the player to the nearest connection point.
         // It relies on the following to be true:
         // game.GetJumpClick()  -
         // Input.GetMouseButtonDown(0) - Left mouse click down (also works for touch)
         // game.IsHingeClosest(index)  - Looks through an array of all possible connection points
         // !game.GetConnectedFlag()
        if (this.game.GetJumpClick() && Input.GetMouseButtonDown(0) && this.game.IsHingeClosest(this.index) && !this.game.GetConnectedFlag())
        {
            //Debug.Log("Glider: " + DB.Glider + "Booster: " + DB.Booster);
            // If the glider is active, connect only when the left half of the screen is pressed
            if (DB.Glider == 1 || DB.Booster == 1)
            {
#pragma warning disable SA1108 // Block statements should not contain embedded comments
                Debug.Log("input flag: " + this.gameInput.GetInputFlag());
                if (this.gameInput.GetInputFlag() == 0) // Only connect when left side of the screen is pressed
#pragma warning restore SA1108 // Block statements should not contain embedded comments
                {
                    this.Connect();
                }
            }

            // Connect on any input when no upgrades are active.
            if (DB.Glider == 0 && DB.Booster == 0)
            {
                this.Connect();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            this.game.SetConnectedFlag(false);
            this.CurrRope.connectedBody = null;
            mouseState = 0;
            Destroy(this.visualRope);
        }
    }

    private Vector3 CorrectedHingePosition()
    {
        return new Vector3(this.Hinge.transform.position.x - this.xOffset, this.Hinge.transform.position.y - this.yOffset, this.Hinge.transform.position.z);
    }

    private void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        myLine.GetComponent<LineRenderer>().sortingLayerName = "Default";
        myLine.GetComponent<LineRenderer>().sortingOrder = 1;
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Specular"));
#pragma warning disable CS0618 // Type or member is obsolete
        lr.SetColors(color, color);
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
        lr.SetWidth(0.5f, 0.5f);
#pragma warning restore CS0618 // Type or member is obsolete
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, .01f);
    }

    private void Connect()
    {
        if (mouseState != 1)
        {
            if (this.firstConnection)
            {
                this.firstConnection = false;
                this.game.incrementConnections();
            }

            this.visualRope = Instantiate(Resources.Load("VisualRope", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;

            // Modify the clone to your heart's content
            Vector3 temp = (this.Player.transform.position + this.CorrectedHingePosition()) * 0.5f;
            this.visualRope.transform.position = new Vector3(temp.x, temp.y, -5);
            this.game.SetIsConnected(this);
            Vector2 vel = this.Player.GetComponent<Rigidbody2D>().velocity + (12 * this.Player.GetComponent<Rigidbody2D>().velocity.normalized);
            this.CurrRope.connectedBody = this.Player.GetComponent<Rigidbody2D>();
        }

        mouseState = 1;
    }
}