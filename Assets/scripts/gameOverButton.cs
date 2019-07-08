using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameOverButton : MonoBehaviour
{
    public GameController game;
    public Text score;
    public Text highScore;
    // Start is called before the first frame update
    void Start()
    {


        if (SceneManager.GetActiveScene().buildIndex == 2)  //checks that the scene index is the game over screen
        { 

            //creates highscore text on screen
            Font arial;
            arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

            // Create Canvas GameObject.
            GameObject canvasGO = new GameObject();
            canvasGO.name = "Canvas";
            canvasGO.AddComponent<Canvas>();
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();

            // Get canvas from the GameObject.
            Canvas canvas;
            canvas = canvasGO.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            // Create the Text GameObject.
            GameObject textGO = new GameObject();
            textGO.transform.parent = canvasGO.transform;
            textGO.AddComponent<Text>();

            GameObject textGO1 = new GameObject();
            textGO1.transform.parent = canvasGO.transform;
            textGO1.AddComponent<Text>();

            // Set Text component properties.
            score = textGO.GetComponent<Text>();
            score.font = arial;
            score.fontSize = 24;
            score.alignment = TextAnchor.MiddleCenter;
            score.color = Color.black;

            // Provide Text position and size using RectTransform.
            score.transform.position = new Vector3(460, 410, -5);
            score.text = "Score: " + DB.Score;
            
            highScore = textGO1.GetComponent<Text>();
            highScore.font = arial;
            highScore.fontSize = 24;
            highScore.alignment = TextAnchor.MiddleCenter;
            highScore.color = Color.black;

            // Provide Text position and size using RectTransform.
            highScore.transform.position = new Vector3(460, 315, -5);
            highScore.text = "High Score: " + DB.HighScore;
            

        }

    }

    // Update is called once per frame
    void Update()
    {
        //checkForEnter();
    }

    public void restart()   //on game over scene, button press causes this method to run
    {
        DB.LvlIndex = 0;
        SceneManager.LoadScene(0);

    }

    public void upgrade()   //on game over scene, button press causes this method to run
    {
        DB.LvlIndex = 0;

        Upgrades.Glider = 1;//sets flag for glider
        Upgrades.Booster = 1;//sets flag for booster
        SceneManager.LoadScene(0);


    }
    //Load main menu button
    public void mainMenuPlay()  //main menu play button causes this method to run
    {
        SceneManager.LoadScene(0);
    }
    //Load Game over menu
    public void gameOverMainMenu()
    {
        SceneManager.LoadScene(3);
    }
}
