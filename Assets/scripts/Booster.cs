using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public GameController game;
    public GameObject Player;
    public GameObject booster;
    private InputClass gameInput;
    private float LastMousePosX;
    private bool TiltDownPressed;
    private bool TiltUpPressed;
    private float a = 3000f;//acceleration constant
    private static int fuelMax = 100;//maximum fuel for booster
    private int fuel;//amount of fuel left
    private static int fuelUseRate = 2;//usage rate of fuel
    private float playerXDelta;
    private float playerYDelta;
    private Vector3 playerDelta;
    // Start is called before the first frame update
    void Start()
    {

        fuel = fuelMax; //fuel starts at max
        game = GameObject.FindWithTag("GameController").GetComponent<GameController>();//links game object
        Player = GameObject.FindWithTag("Player");//links player object
        gameInput = GameObject.FindWithTag("MainCamera").GetComponent<InputClass>();//link inputs
        //booster.transform.position = Player.transform.position + new Vector3(0, 10, 0);
        playerDelta = Player.transform.position - this.transform.position;//sets delta of player and booster
        playerDelta.z = Player.transform.position.z - 100;//sets z position
    }

    // Update is called once per frame
    void Update()
    {
        //maps booster to player
        //Debug.Log(Player.transform.position.x);
        if(Upgrades.Booster == 1)
        {
            playerDelta.z = Player.transform.position.z;
        }
        else
        {
            playerDelta.z = Player.transform.position.z - 100;
        }
        this.transform.position = (Player.transform.position - playerDelta);
        //Debug.Log("Yeett");
        //LastMousePosX = Input.mousePosition.x;
        refuel();//refuels if needed
        if (gameInput.getInputFlag() == 2 && Upgrades.Booster == 1)//if click and upgrade is selected
        {
            accelerateYVelocity();//accelerates player
        }
    }

    int accelerateYVelocity()//boosts player up
    {
        Debug.Log("Fuel: " + fuel);

        if(fuel >= 0)//checks if there is fuel left
        {
            //Player.GetComponent<Rigidbody2D>().velocity.y += a;
            //Debug.Log(Player.GetComponent<Rigidbody2D>().velocity.y);
            Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, a));
            fuel-= fuelUseRate;//reduces fuel
        }

        return 1;
    }

    void refuel()//refuels booster
    {
        if (fuel < fuelMax)
        {
            fuel++;
        }
    }
}
