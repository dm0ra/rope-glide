using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemies : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject thisEnemy;
    public GameController game;
    public GameObject Player;
    public Camera cam;
    private int enemyIndex;
    private int enemySpeed;
    private int enemyX;
    private int enemyY;
    private static int enemyZ;
    private int updateCount;
    private int moveTime;
    float timeCounter;
    private int centerScreenY = 65;


    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        game = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        cam = Camera.current;
        enemyIndex = game.AddEnemy(this);
        //enemyIndex = game.AddItem(this);
        //Rect view = cam.rect;       //creates a rect object to use for the randomization of the x and y coordinates of the enemy
        
        enemyX = (int)Player.transform.position.x + 200;  //creates a random x value for the spawn of the enemy
        enemyY = Random.Range(centerScreenY - 30, centerScreenY + 20);  //creates a random y value for the spawn of the enemy
        //Debug.Log(cam.transform.position.y);

        //enemyX = 26;
        //enemyY = 75;

        enemySpeed = 1;
        enemyZ = -5;
        updateCount = 0;   //this variable is used to see how many times the update function has been called
        moveTime = 80;
        timeCounter = 0;

        Vector3 enemyPosition = new Vector3(enemyX, enemyY, enemyZ);
        thisEnemy.transform.position = enemyPosition;
}

// Update is called once per frame
void Update()
{
   //Debug.Log("x: " + enemyX + " y: " + enemyY + " updateCount: " + updateCount + " moveTime: " + moveTime);
        
        updateCount++;
        if(updateCount < moveTime && (updateCount % 3 == 0)) //move time seconds of left movement
        {
            //enemyX = enemyX - enemySpeed;   //decreases the enemy x and y position by the enemy speed once per frame for moveTime seconds
            enemyY = enemyY - enemySpeed;
        }

        else if ((updateCount < moveTime*2) && (updateCount % 3 == 0)) //move time seconds of left movement
        {
            //enemyX = enemyX + enemySpeed;   //increases the enemy x and y position by the enemy speed once per frame for moveTime seconds
            enemyY = enemyY + enemySpeed;
        }

        else if ((updateCount < moveTime * 3) && (updateCount % 3 == 0)) //move time seconds of left movement
        {
            //enemyX = enemyX + enemySpeed;   //increases the enemy x and y position by the enemy speed once per frame for moveTime seconds
            enemyY = enemyY - enemySpeed;
        }

        else if ((updateCount < moveTime * 4) && (updateCount % 3 == 0)) //move time seconds of left movement
        {
            //enemyX = enemyX - enemySpeed;   //increases the enemy x and y position by the enemy speed once per frame for moveTime seconds
            enemyY = enemyY + enemySpeed;
        }
        if (updateCount >= moveTime*4)
        {
            updateCount = 0;
        }


       

         // multiply all this with some speed variable (* speed);
        Vector3 enemyPosition = new Vector3(enemyX, enemyY, enemyZ);
        thisEnemy.transform.position = enemyPosition;
    }



    //called during collision with enemies
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (true) // Add else if's for different items
            {
                Player.GetComponent<Rigidbody2D>().velocity = Player.GetComponent<Rigidbody2D>().velocity +
                                                             (80 * Player.GetComponent<Rigidbody2D>().velocity.normalized);
                game.respawnPlayer();

            }
            //else if()
            //thisItem.SetActive(false);
            //Destroy(thisItem);
        }
    }

   /* public int garbageMan(bool destroy)
    {
        if (destroy)
        {

        }
    }
    */
}
