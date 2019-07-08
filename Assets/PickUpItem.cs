using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickUpItem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject thisItem;
    public GameController game;
    public GameObject Player;
    public GameObject glider;
    public Camera cam;
    private int itemIndex;
    private int itemX;
    private int itemY;
    private int itemZ;
    private int centerScreenY = 65;

    void Start()
    {
        glider = GameObject.FindWithTag("Glider");
        Player = GameObject.FindWithTag("Player");
        game = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        itemIndex = game.AddItem(this);
        cam = Camera.current;
        itemZ = -5;
        itemX = (int)Player.transform.position.x + Random.Range(150, 160);  //creates a random x value for the spawn of the enemy
        itemY = Random.Range(centerScreenY - 35, centerScreenY + 15);  //creates a random y value for the spawn of the enemy
        Vector3 itemPosition = new Vector3(itemX, itemY, itemZ);
        thisItem.transform.position = itemPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(true) // Add else if's for different items
            {

                Player.GetComponent<Rigidbody2D>().velocity = Player.GetComponent<Rigidbody2D>().velocity +
                                                             (70 * Player.GetComponent<Rigidbody2D>().velocity.normalized);

                if (Upgrades.Glider == 1)
                {
                    glider tempGlider = GameObject.FindGameObjectWithTag("Glider").GetComponent<glider>();
                    if (tempGlider.GliderActive())
                    {
                        float tiltInAngles;
                        if (glider.transform.eulerAngles.z > 0 && glider.transform.eulerAngles.z < 60)
                        {

                            tiltInAngles = glider.transform.eulerAngles.z;
                        }
                        else
                        {
                            tiltInAngles = glider.transform.eulerAngles.z - 360;
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
            thisItem.SetActive(false);
            Destroy(thisItem);
        }
    }


}
