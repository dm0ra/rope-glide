// <copyright file="PickUpItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using UnityEngine;

/// <summary>
/// This class allows player to pick up a coin to increase speed.
/// Start() is called before the first frame update.
/// </summary>
public class PickUpItem : MonoBehaviour
{
#pragma warning disable SA1401 // Fields should be private
    /// <summary>
    /// ThisItem is a coin for now; may add more items later.
    /// </summary>
    public GameObject ThisItem;
#pragma warning restore SA1401 // Fields should be private

#pragma warning disable SA1401 // Fields should be private
    /// <summary>
    /// See GameController class.
    /// </summary>
    public GameController Game;
#pragma warning restore SA1401 // Fields should be private

#pragma warning disable SA1401 // Fields should be private
    /// <summary>
    /// The player in the Unity editor.
    /// </summary>
    public GameObject Player;
#pragma warning restore SA1401 // Fields should be private

#pragma warning disable SA1401 // Fields should be private
    /// <summary>
    /// The glider in the Unity editor.
    /// </summary>
    public GameObject Glider;
#pragma warning restore SA1401 // Fields should be private

#pragma warning disable SA1401 // Fields should be private
    /// <summary>
    /// The camera in the Unity editor.
    /// </summary>
    public Camera Cam;
#pragma warning restore SA1401 // Fields should be private
    private int itemIndex;  // will use for later items
    private int itemX;
    private int itemY;
    private int itemZ;
    private int centerScreenY = 65;

    private void Start()
    {
        this.Glider = GameObject.FindWithTag("Glider");
        this.Player = GameObject.FindWithTag("Player");
        this.Game = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        this.itemIndex = this.Game.AddItem(this);
        this.Cam = Camera.current;
        this.itemZ = -5;
        this.itemX = (int)this.Player.transform.position.x + Random.Range(150, 160);  // creates a random x value for the spawn of the enemy
        this.itemY = Random.Range(this.centerScreenY - 35, this.centerScreenY + 15);  // creates a random y value for the spawn of the enemy
        Vector3 itemPosition = new Vector3(this.itemX, this.itemY, this.itemZ);
        this.ThisItem.transform.position = itemPosition;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
#pragma warning disable SA1108 // Block statements should not contain embedded comments
            if (true) // Add else if's for different items
#pragma warning restore SA1108 // Block statements should not contain embedded comments
            {
                this.Player.GetComponent<Rigidbody2D>().velocity = this.Player.GetComponent<Rigidbody2D>().velocity +
                                                             (70 * this.Player.GetComponent<Rigidbody2D>().velocity.normalized);
                if (DB.Glider == 1)
                {
                    glider tempGlider = GameObject.FindGameObjectWithTag("Glider").GetComponent<glider>();
                    if (tempGlider.GliderActive())
                    {
                        float tiltInAngles;
                        if (this.Glider.transform.eulerAngles.z > 0 && this.Glider.transform.eulerAngles.z < 60)
                        {
                            tiltInAngles = this.Glider.transform.eulerAngles.z;
                        }
                        else
                        {
                            tiltInAngles = this.Glider.transform.eulerAngles.z - 360;
                        }

                        float tiltInRads = 0.0174533f * tiltInAngles;
                        float cos = Mathf.Cos(tiltInRads);
                        float sin = Mathf.Sin(tiltInRads);
                        Vector2 vec = new Vector2(cos, sin);
                        this.Player.GetComponent<Rigidbody2D>().velocity = this.Player.GetComponent<Rigidbody2D>().velocity +
                                                                     (120 * vec);
                    }
                    else
                    {
                        this.Player.GetComponent<Rigidbody2D>().velocity = this.Player.GetComponent<Rigidbody2D>().velocity +
                                                                 (120 * this.Player.GetComponent<Rigidbody2D>().velocity.normalized);
                    }
                }
                else
                {
                    this.Player.GetComponent<Rigidbody2D>().velocity = this.Player.GetComponent<Rigidbody2D>().velocity +
                                                                 (120 * this.Player.GetComponent<Rigidbody2D>().velocity.normalized);
                }
            }

            // else if()
            this.ThisItem.SetActive(false);
            Destroy(this.ThisItem);
        }
    }
}
