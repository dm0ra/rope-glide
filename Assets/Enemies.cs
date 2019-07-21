// <copyright file="Enemies.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using UnityEngine;

/// <summary>
/// This clas is used to control the enemies behavior in the game, when to create, and how to move.
/// </summary>
public class Enemies : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject thisEnemy; // references the current enemy
    public GameController Game; // game controller object
    public GameObject Player; // player object
    private static int enemyZ;
    private int enemyIndex;
    private int enemySpeed;
    private int enemyX;
    private int enemyY;
    private int updateCount;
    private int moveTime;
    private float timeCounter;
    private int centerScreenY = 65;

    void Start()
    {
        this.Player = GameObject.FindWithTag("Player");
        this.Game = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        this.enemyIndex = this.Game.AddEnemy(this);

        this.enemyX = (int)this.Player.transform.position.x + 200; // creates a random x value for the spawn of the enemy
        this.enemyY = Random.Range(this.centerScreenY - 30, this.centerScreenY + 20); // creates a random y value for the spawn of the enemy

        this.enemySpeed = 1;
        enemyZ = -5;
        this.updateCount = 0; // this variable is used to see how many times the update function has been called
        this.moveTime = 80;
        this.timeCounter = 0;

        Vector3 enemyPosition = new Vector3(this.enemyX, this.enemyY, enemyZ);
        this.thisEnemy.transform.position = enemyPosition;
    }

    // Update is called once per frame
    void Update()
    {
        this.updateCount++;

        // move time seconds of left movement
        if (this.updateCount < this.moveTime && (this.updateCount % 3 == 0))
        {
            this.enemyY = this.enemyY - this.enemySpeed;
        }

        // move time seconds of left movement
        else if ((this.updateCount < this.moveTime * 2) && (this.updateCount % 3 == 0))
        {
            this.enemyY = this.enemyY + this.enemySpeed;
        }

        // move time seconds of left movement
        else if ((this.updateCount < this.moveTime * 3) && (this.updateCount % 3 == 0))
        {
            this.enemyY = this.enemyY - this.enemySpeed;
        }

        // move time seconds of left movement
        else if ((this.updateCount < this.moveTime * 4) && (this.updateCount % 3 == 0))
        {
            this.enemyY = this.enemyY + this.enemySpeed;
        }

        if (this.updateCount >= this.moveTime * 4)
        {
            this.updateCount = 0;
        }

        // multiply all this with some speed variable (* speed);
        Vector3 enemyPosition = new Vector3(this.enemyX, this.enemyY, enemyZ);
        this.thisEnemy.transform.position = enemyPosition;
    }

    // called during collision with enemies
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (true)
            {
                this.Player.GetComponent<Rigidbody2D>().velocity = this.Player.GetComponent<Rigidbody2D>().velocity +
                                                             (80 * this.Player.GetComponent<Rigidbody2D>().velocity.normalized);
                this.Game.RespawnPlayer();
            }
        }
    }
}
