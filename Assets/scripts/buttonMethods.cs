using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonMethods : MonoBehaviour
//public class buttonMethods
{

    public GameController game;
    public Text score;
    public Text highScore;
    public int previewIndex;//used to know which preview item was clicked
    //1 = glider, 2 = booster

    private GameObject purchaseButton, upgradeDescription, upgradePrice, boosterImage, gliderImage, upgradeMenuCanvas, gameController, userCash;//these are gameobjects of the text boxes in the upgrade menu screen as well as preview images
    private int bPrice = 100;//sets price for booster
    private int gPrice = 100;//sets price for glider
    private string boosterPrice;
    private string gliderPrice;
    private string unpurchasedText = "Buy";
    private string purchasedText = "Purchased";
    private string cashString = "Cash: ";
    private string boosterDescriptionText = "Description:   This booster allows you to jetpack in the sky for a limited time, " +
        "fuel replenishes over time. Useful for dodging enemies or reaching the stars";
    private string gliderDescriptionText = "Description:    Glider allows you glide upwards and downwards to bring out your inner fortnite";

    private bool firstRun = true;

    // Start is called before the first frame update
    void Start()
    {
        //these three lines of code link game objects to tagged objects in the scene manager
        purchaseButton = GameObject.FindWithTag("upgradePurchaseButtonText");
        upgradeDescription = GameObject.FindWithTag("upgradeDescriptionText");
        upgradePrice = GameObject.FindWithTag("upgradePriceText");
        //these two gameobjects are used to interact with the preview images in the upgrade menu
        gliderImage = GameObject.FindWithTag("GliderImage");
        boosterImage = GameObject.FindWithTag("BoosterImage");
        userCash = GameObject.FindWithTag("UserCash");//text box to display a users cash
        boosterPrice = "Price:   " + bPrice + " Cash";
        gliderPrice = "Price:    " + gPrice + " Cash";

        //gliderImage.SetActive(false);


        //boosterImage = game.boosterImage;
        //gliderImage = game.gliderImage;

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
            //GameObject textGO = new GameObject();
            //textGO.transform.parent = canvasGO.transform;
            //textGO.AddComponent<Text>();

            //GameObject textGO1 = new GameObject();
            //textGO1.transform.parent = canvasGO.transform;
            //textGO1.AddComponent<Text>();

            // Set Text component properties.
            //score = textGO.GetComponent<Text>();
            //score.font = arial;
            //score.fontSize = 24;
            //score.alignment = TextAnchor.MiddleCenter;
            //score.color = Color.black;

            // Provide Text position and size using RectTransform.
            //score.transform.position = new Vector3(460, 410, -5);
            //score.text = "Score: " + DB.Score;

            //highScore = textGO1.GetComponent<Text>();
            //highScore.font = arial;
            //highScore.fontSize = 24;
            //highScore.alignment = TextAnchor.MiddleCenter;
            //highScore.color = Color.black;

            // Provide Text position and size using RectTransform.
            //highScore.transform.position = new Vector3(460, 315, -5);
            //highScore.text = "High Score: " + DB.HighScore;




        }

    }

    // Update is called once per frame
    void Update()
    {
        //checkForEnter();
        if (SceneManager.GetActiveScene().buildIndex == 4 && firstRun)
        {
            setComponentText(userCash, cashString + DB.BankCash);//updates the cash string
            gliderImage.SetActive(false);
            boosterImage.SetActive(false);
            firstRun = false;

        }
    }

    //used to tag booster images to the booster game object from other scripts
    public void initBoosterImages(GameObject booster)
    {
        boosterImage = booster;
    }

    //function used to change the text of a text component in a scene
    //requirements to use this function is that the game object has a component of text
    void setComponentText(GameObject go, string text)
    {
        go.GetComponent<Text>().text = text;//changes button text
    }

    public void restart()   //on game over scene, button press causes this method to run
    {
        DB.LvlIndex = 0;
        SceneManager.LoadScene(0);

    }

    public void upgrade()   //on game over scene, button press causes this method to run
    {
        DB.LvlIndex = 0;

        DB.Glider = 1;//sets flag for glider
        DB.Booster = 1;//sets flag for booster
        SceneManager.LoadScene(0);
    }

    //used to purchase a booster in the 
    public void previewBooster()
    {
        DB.PreviewIndex = 2;
        //previewIndex = 2;//sets preview index to booster

        //setting purchased button text
        if (DB.Booster == 1)//checks if booster is purchased
        {
            setComponentText(purchaseButton, purchasedText); //sets button to display "purchased"
        }
        else//if booster is not purchased
        {
            setComponentText(purchaseButton, unpurchasedText); //sets button to display "buy"
        }

        boosterImage.SetActive(true);//displays booster
        gliderImage.SetActive(false);//removes glider

        setComponentText(upgradeDescription, boosterDescriptionText);//sets the description to display in the upgrade menu
        setComponentText(upgradePrice, boosterPrice);//sets the price to display in upgrade menu
    }


    //used to buy a glider in the upgrade menu
    public void previewGlider()
    {
        DB.PreviewIndex = 1;

        //setting purchase button text
        if (DB.Glider == 1)//checks if glider is purchased
        {
            setComponentText(purchaseButton, purchasedText); //sets button to display "purchased"
        }
        else//if glider is not purchased
        {
            setComponentText(purchaseButton, unpurchasedText); //sets button to display "buy"
        }

        setComponentText(upgradeDescription, gliderDescriptionText);//sets the description to display in the upgrade menu
        setComponentText(upgradePrice, gliderPrice);//sets the price to display in upgrade menu


        boosterImage.SetActive(false);//removes booster
        gliderImage.SetActive(true);//display glider
        //previewIndex = 1;//sets preview index to glider
    }

    //called when a user clicks the buy button in the upgrades menu
    //used to purchase upgrade items after previewing them.
    public void purchase()
    {
        //Debug.Log(previewIndex);
        if (DB.PreviewIndex == 1 && DB.BankCash > gPrice && DB.Glider != 1)//checks which item is being previewed and that the user has enough cash, and checks that it is already not purchased
        {
            DB.Glider = 1;//sets glider purchased flag
            DB.BankCash -= gPrice;//decreases cash
            Debug.Log("Glider Purchased");
            setComponentText(purchaseButton, purchasedText);//sets the text of the button to a purchased message

            //saves highscore and cash to csv file
            var csv = new System.Text.StringBuilder();
            var highScoreString = DB.HighScore.ToString();
            var newLine = string.Format(highScoreString);
            var cashString = DB.BankCash.ToString();
            var glideString = DB.Glider.ToString();
            var boostString = DB.Booster.ToString();
            csv.AppendLine(newLine);
            csv.AppendLine(cashString);
            csv.AppendLine(glideString);
            csv.AppendLine(boostString);
            DataSaver.saveData<string>(csv.ToString(), "HighScore");
        }

        if (DB.PreviewIndex == 2 && DB.BankCash > bPrice && DB.Booster != 1)//checks which item is being previewed and that the user has enough cash, and checks that it is already not purchased
        {
            DB.Booster = 1;//sets booster purchased flag
            DB.BankCash -= bPrice;//decreases cash 
            Debug.Log("Booster Purchased");
            setComponentText(purchaseButton, purchasedText);//sets the text of the button to a purchased message
            //saves highscore and cash to csv file
            var csv = new System.Text.StringBuilder();
            var highScoreString = DB.HighScore.ToString();
            var newLine = string.Format(highScoreString);
            var cashString = DB.BankCash.ToString();
            var glideString = DB.Glider.ToString();
            var boostString = DB.Booster.ToString();
            csv.AppendLine(newLine);
            csv.AppendLine(cashString);
            csv.AppendLine(glideString);
            csv.AppendLine(boostString);
            DataSaver.saveData<string>(csv.ToString(), "HighScore");
        }

        setComponentText(userCash, cashString + DB.BankCash);//updates new cash variable
    }

    public void gotoMainMenu()
    {
        //previewIndex = 0;
        SceneManager.LoadScene(3);//switches scene to main menu
    }

    public void gotoUpgrades()  //main menu play button causes this method to run
    {
        SceneManager.LoadScene(4);

    }

    //Load main menu button
    public void gotoPlay()  //main menu play button causes this method to run
    {
        SceneManager.LoadScene(0);
    }

    //Load Game over menu
    public void gameOverMainMenu()
    {
        SceneManager.LoadScene(3);
    }
}
