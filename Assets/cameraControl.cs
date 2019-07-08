using System.Collections;
using System.Threading.Tasks;
//using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;

public class cameraControl : MonoBehaviour
{

    /*Todo: 
    * 
    */
    






    public Camera Cam;          //camera object
    public GameObject Player;   //player object
    static float startPos;      //position where player starts
    private int Count100s;      //helper variable to determine distance

    private int Count50s;       //second helper variable to determine distancec traveled
    public int groundWidth = 294;   //width of background
    public int cameraWidth = 228;   //width of camera view
    public int cameraHelper = 0;    //helper variable
    public int boostSpawnChance = 4;    // boostSpawnChance / 10 to spawn a boost every x distance
    public int startup = 1;     //helper variable
        
    public GameController game; //game object

    // public int blockSpawnDistance = 150;
    public int enemySpawnChance = 3;// enemySpawnChance / 10 to spawn an enemy every x distance


    public int blockSpawnDistance = 200;    //distance between blocks
    public int blockVar = 15;           

    private int camheight;      //height of camera
    private bool IsCoRutineRunning;
    private float bottomCamPos;//bottom of the campera position
    private float delayTime;

    //Cam Vars 
    public float dampTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;

    private float BackgroundStartPos;



    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        camheight = 133;
        IsCoRutineRunning = false;
        startPos = Player.transform.position.x;
        Count100s = 0;
        bottomCamPos = Cam.transform.position.y;
        delayTime = 0.000001f;
        BackgroundStartPos = 277f;
        
    }

    // Update is called once per frame
    void Update()
    {

        float xpos = Player.transform.position.x;
        game.setDistance(xpos);
        game.setHighestHeight(Player.transform.position.y);
        game.setTopSpeed(Player.GetComponent<Rigidbody2D>().velocity);

        //Rect view = Cam.pixelRect;


        float temp = 0f;
        target = Player.transform ;
        Vector3 point = Cam.WorldToViewportPoint(target.position);
        Vector3 delta = target.position - Cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
        Vector3 destination = transform.position + delta;
        if (destination.y < 105f)
        {
            destination = new Vector3(destination.x, 105f, destination.z);
        }
        if (destination.x < 107f)
        {
            destination = new Vector3(107f, destination.y, destination.z);
        }
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

        

        if (startup == 1)
        {
            startup = 0;
            //  Object prefab = AssetDatabase.LoadAssetAtPath("Assets/prefab/Block.prefab", typeof(GameObject));
           // GameObject block = Instantiate(Resources.Load("Block1", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            // Modify the clone to your heart's content
           // block.transform.position = new Vector3(Player.transform.position.x + Random.Range(blockSpawnDistance - blockVar, blockSpawnDistance + blockVar), Random.Range(187 - 5, 187 + 5), -5);

           //GameObject sky = Instantiate(Resources.Load("Sky_", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            // Modify the clone to your heart's content
            //sky.transform.position = new Vector3(startPos + 290, 108.9f, 0);



            if(Random.Range(0,10) < enemySpawnChance)
            {
                GameObject mace = Instantiate(Resources.Load("Mace", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            }


            //GameObject mace = Instantiate(Resources.Load("Mace", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;


            //Count100s = 0;
            //Object prefab2 = AssetDatabase.LoadAssetAtPath("Assets/prefab/Ground.prefab", typeof(GameObject));
           // GameObject ground = Instantiate(Resources.Load("Ground", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
           // ground.transform.position = new Vector3(startPos + 295, 55.0f, 0);
        }


        if (startPos + 100 < Player.transform.position.x)
        //if(cameraHelper + cameraWidth < Player.transform.position.x)
        {
            cameraHelper = (int)Player.transform.position.x;
            startPos = Player.transform.position.x;
            Count100s = Count100s + 1;

            if ((Count100s % 2) == 0)
            {
                GameObject block = Instantiate(Resources.Load("Block1", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
                block.transform.position = new Vector3(Player.transform.position.x + Random.Range(blockSpawnDistance - blockVar, blockSpawnDistance + blockVar), Random.Range(192 - 5, 192 + 5), -5);

            if ((Count100s % 3) == 0)
            {
                GameObject block2 = Instantiate(Resources.Load("Block1", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
                block2.transform.position = new Vector3(Player.transform.position.x + Random.Range(blockSpawnDistance - blockVar, blockSpawnDistance + blockVar), Random.Range(450 - 60, 292 + 10), -5);
            }
            if ((Count100s % 4) == 0)
            {
                GameObject block3 = Instantiate(Resources.Load("Block1", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
                block3.transform.position = new Vector3(Player.transform.position.x + Random.Range(blockSpawnDistance - blockVar, blockSpawnDistance + blockVar), Random.Range(680 - 80, 292 + 15), -5);

            }//Count100s = 0;
                //Object prefab1 = AssetDatabase.LoadAssetAtPath("Assets/prefab/Sky_.prefab", typeof(GameObject));
                GameObject background = Instantiate(Resources.Load("DayBackground", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
               background.transform.position = new Vector3(BackgroundStartPos + 809.95f, 133f, 50);
                BackgroundStartPos = BackgroundStartPos + 809.95f;

                if (Random.Range(0,10) < enemySpawnChance)//generates mace with enemySpawnChance/10 chance
                {
                    GameObject mace = Instantiate(Resources.Load("Mace", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
                }


             //   GameObject skyred = Instantiate(Resources.Load("Sky_Red", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
             //   skyred.transform.position = new Vector3(startPos + 290, 258.4f, 0);
                // GameObject mace = Instantiate(Resources.Load("Mace", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;

                //Count100s = 0;
                //Object prefab2 = AssetDatabase.LoadAssetAtPath("Assets/prefab/Ground.prefab", typeof(GameObject));
            //    GameObject ground = Instantiate(Resources.Load("Ground", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            //    ground.transform.position = new Vector3(startPos + 295, 55.0f, 0);

                if(Random.Range(0,10) > boostSpawnChance)
                {
                    GameObject PickUpItem = Instantiate(Resources.Load("Boost", typeof(GameObject))   ) as GameObject;
                    PickUpItem.transform.position = PickUpItem.transform.position + new Vector3(0, 0, -25);
                }
               

            }
            if (Count100s == 500)
            {
                // Object prefab3 = AssetDatabase.LoadAssetAtPath("Assets/prefab/Finish.prefab", typeof(GameObject));
                GameObject finishLine = Instantiate(Resources.Load("Finish", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
                finishLine.transform.position = new Vector3(startPos + 300, 82.1f, 0);

            }
        }
        
    }
    IEnumerator WaitAndMove(float delayTime, Vector3 from, Vector3 to)
    {
        IsCoRutineRunning = true;


        yield return new WaitForSeconds(delayTime); // start at time X

        float startTime = Time.time; // Time.time contains current frame time, so remember starting point
        while (Time.time - startTime <= .5)
        { // until one second passed


            Cam.transform.position = cameraControl.Lerp(from, to, Time.time - startTime, Player.transform.position.x); // lerp from A to B in one second
            yield return 1; // wait for next frame
        }
        IsCoRutineRunning = false;
    }

    public static Vector3 Lerp(Vector3 a, Vector3 b, float t, float x) 
    {

        t = Mathf.Clamp01(t);

        return new Vector3(

            x, 
            a.y + (b.y - a.y) * t,

            a.z + (b.z - a.z) * t

        ); 
    } 
}









   
