// <copyright file="Booster.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using UnityEngine;

/// <summary>
/// This class is used for booster logic when the upgrade is purchased.
/// </summary>
public class Booster : MonoBehaviour
{
    public GameController Game; // gamecontroller object
    public GameObject Player; // player game object
    public GameObject booster; // booster game object
    public int BoosterPrice; // booster price integer
    private static int fuelMax = 100; // maximum fuel for booster
    private static int fuelUseRate = 3; // usage rate of fuel
    private InputClass gameInput; // gameInput to read what buttons were pressed
    private float a = 3000f; // acceleration constant

    private int fuel; // amount of fuel left
    private Vector3 playerDelta; // this variable represents the distance between the player and the booster images

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
        this.booster = GameObject.FindGameObjectWithTag("Booster");
        this.fuel = fuelMax; // fuel starts at max
        this.Game = GameObject.FindWithTag("GameController").GetComponent<GameController>(); // links game object
        this.Player = GameObject.FindWithTag("Player"); // links player object
        this.gameInput = GameObject.FindWithTag("MainCamera").GetComponent<InputClass>(); // link inputs
        this.playerDelta = this.Player.transform.position - this.transform.position; // sets delta of player and booster
        this.playerDelta.z = this.Player.transform.position.z - 100; // sets z position
        this.BoosterPrice = 100;
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        // maps booster to player
        if (DB.Booster == 1)
        {
            this.playerDelta.z = this.Player.transform.position.z;
        }
        else
        {
            this.playerDelta.z = this.Player.transform.position.z - 100;
        }
        this.transform.position = (Player.transform.position - playerDelta); // sets the booster position close to the player position
        this.Refuel(); // refuels if needed

        // if click on the proper side of the screen and upgrade is selected
        if ((this.gameInput.GetInputFlag() == 2 && DB.Booster == 1 && DB.Glider == 1) || (this.gameInput.GetInputFlag() == 1 && DB.Glider == 0 && DB.Booster == 1))
        {
            this.AccelerateYVelocity(); // accelerates player
        }
    }

    /// <summary>
    /// This method is used to accelerate velocity in the y direction, aka boosting.
    /// </summary>
    private void AccelerateYVelocity()
    {
        Debug.Log("lift off");

        // checks if there is fuel left
        if (this.fuel >= 0)
        {
            this.Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, this.a)); // adds force to the player in the y direction only
            this.fuel -= fuelUseRate; // reduces fuel
        }
    }

    /// <summary>
    /// This method refuels the booster.
    /// </summary>
    private void Refuel()
    {
        if (this.fuel < fuelMax)
        {
            this.fuel++;
        }
    }
}
