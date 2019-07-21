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
    /// <summary>
    /// ThisItem is a coin for now; may add more items later.
    /// </summary>
    public GameObject ThisItem;
    public GameController Game;
    public GameObject Player;
    public GameObject Glider;
    public Camera Cam;
    private int itemIndex;  // will use for later items
    private int itemX;
    private int itemY;
    private int itemZ;
    private int centerScreenY = 65;

    void Start()
    {
        Glider = GameObject.FindWithTag("Glider");
        Player = GameObject.FindWithTag("Player");
        Game = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        itemIndex = Game.AddItem(this);
        Cam = Camera.current;
        itemZ = -5;
        itemX = (int)Player.transform.position.x + Random.Range(150, 160);  //creates a random x value for the spawn of the enemy
        itemY = Random.Range(centerScreenY - 35, centerScreenY + 15);  //creates a random y value for the spawn of the enemy
        Vector3 itemPosition = new Vector3(itemX, itemY, itemZ);
        ThisItem.transform.position = itemPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (true) // Add else if's for different items
            {
                Player.GetComponent<Rigidbody2D>().velocity = Player.GetComponent<Rigidbody2D>().velocity +
                                                             (70 * Player.GetComponent<Rigidbody2D>().velocity.normalized);
                if (DB.Glider == 1)
                {
                    glider tempGlider = GameObject.FindGameObjectWithTag("Glider").GetComponent<glider>();
                    if (tempGlider.GliderActive())
                    {
                        float tiltInAngles;
                        if (Glider.transform.eulerAngles.z > 0 && Glider.transform.eulerAngles.z < 60)
                        {
                            tiltInAngles = Glider.transform.eulerAngles.z;
                        }
                        else
                        {
                            tiltInAngles = Glider.transform.eulerAngles.z - 360;
                        }
                        float tiltInRads = 0.0174533f * tiltInAngles;
                        float cos = Mathf.Cos(tiltInRads);
                        float sin = Mathf.Sin(tiltInRads);
                        Vector2 vec = new Vector2(cos, sin);
                        Player.GetComponent<Rigidbody2D>().velocity = Player.GetComponent<Rigidbody2D>().velocity +
                                                                     (120 * vec);
                    }
                    else
                    {
                        Player.GetComponent<Rigidbody2D>().velocity = Player.GetComponent<Rigidbody2D>().velocity +
                                                                 (120 * Player.GetComponent<Rigidbody2D>().velocity.normalized);
                    }
                }
                else
                {
                    Player.GetComponent<Rigidbody2D>().velocity = Player.GetComponent<Rigidbody2D>().velocity +
                                                                 (120 * Player.GetComponent<Rigidbody2D>().velocity.normalized);
                }
            }
            //else if()
            ThisItem.SetActive(false);
            Destroy(ThisItem);
        }
    }
}
