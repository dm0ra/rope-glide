// <copyright file="ButtonMethods.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// The button methods class is used to handle all button presses in the game as well as some logic for gliders, boosters, and purchasing there are helper functions.
/// </summary>
public class ButtonMethods : MonoBehaviour
{
    private Text score; // text object for score
    private Text highScore; // text object for highschore
    private int previewIndex; // used to know which preview item was clicked. 1 = glider, 2 = booster

    // these are gameobjects of the GUI, mostly in the upgrade menu. Such as button text, descriptions, price, cash, preview images
    private GameObject purchaseButton;
    private GameObject upgradeDescription;
    private GameObject upgradePrice;
    private GameObject boosterImage;
    private GameObject gliderImage;
    private GameObject upgradeMenuCanvas;
    private GameObject gameController;
    private GameObject userCash;

    private int bPrice = 100; // sets price for booster
    private int gPrice = 100; // sets price for glider
    private string boosterPrice; // string representing booster price in gui
    private string gliderPrice; // string representing glider price in gui
    private string unpurchasedText = "Buy"; // gui text
    private string purchasedText = "Purchased"; // gui text
    private string cashString = "Cash: "; // gui text
    private string boosterDescriptionText = "Description:   This booster allows you to jetpack in the sky for a limited time, " +
        "fuel replenishes over time. Useful for dodging enemies or reaching the stars"; // gui text

    private string gliderDescriptionText = "Description:    Glider allows you glide upwards and downwards to bring out your inner fortnite"; // gui text

    private bool firstRun = true; // helper flag to run some code only on the first run

    /// <summary>
    /// used to purchase a booster in the upgrade menu.
    /// </summary>
    public void PreviewBooster()
    {
        DB.PreviewIndex = 2; // sets preview index to booster

        // setting purchased button text
        // checks if booster is purchased
        if (DB.Booster == 1)
        {
            this.SetComponentText(this.purchaseButton, this.purchasedText); // sets button to display "purchased"
        }

        // if booster is not purchased
        else
        {
            this.SetComponentText(this.purchaseButton, this.unpurchasedText); // sets button to display "buy"
        }

        this.boosterImage.SetActive(true); // displays booster
        this.gliderImage.SetActive(false); // removes glider

        this.SetComponentText(this.upgradeDescription, this.boosterDescriptionText); // sets the description to display in the upgrade menu
        this.SetComponentText(this.upgradePrice, this.boosterPrice); // sets the price to display in upgrade menu
    }

    /// <summary>
    /// used to buy a glider in the upgrade menu.
    /// </summary>
    public void PreviewGlider()
    {
        DB.PreviewIndex = 1;

        // setting purchase button text
        // checks if glider is purchased
        if (DB.Glider == 1)
        {
            this.SetComponentText(this.purchaseButton, this.purchasedText); // sets button to display "purchased"
        }

        // if glider is not purchased
        else
        {
            this.SetComponentText(this.purchaseButton, this.unpurchasedText); // sets button to display "buy"
        }

        this.SetComponentText(this.upgradeDescription, this.gliderDescriptionText); // sets the description to display in the upgrade menu
        this.SetComponentText(this.upgradePrice, this.gliderPrice); // sets the price to display in upgrade menu
        this.boosterImage.SetActive(false); // removes booster
        this.gliderImage.SetActive(true); // display glider
    }

    /// <summary>
    /// called when a user clicks the buy button in the upgrades menu used to purchase upgrade items after previewing them.
    /// </summary>
    public void Purchase()
    {
        // checks which item is being previewed and that the user has enough cash, and checks that it is already not purchased
        if (DB.PreviewIndex == 1 && DB.BankCash > this.gPrice && DB.Glider != 1)
        {
            DB.Glider = 1; // sets glider purchased flag
            DB.BankCash -= this.gPrice; // decreases cash
            Debug.Log("Glider Purchased");
            this.SetComponentText(this.purchaseButton, this.purchasedText); // sets the text of the button to a purchased message

            // saves highscore and cash to csv file
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
            DataSaver.SaveData<string>(csv.ToString(), "HighScore");
        }

        // checks which item is being previewed and that the user has enough cash, and checks that it is already not purchased
        if (DB.PreviewIndex == 2 && DB.BankCash > this.bPrice && DB.Booster != 1)
        {
            DB.Booster = 1; // sets booster purchased flag
            DB.BankCash -= this.bPrice; // decreases cash
            Debug.Log("Booster Purchased");
            this.SetComponentText(this.purchaseButton, this.purchasedText); // sets the text of the button to a purchased message

            // saves highscore and cash to csv file
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
            DataSaver.SaveData<string>(csv.ToString(), "HighScore");
        }

        this.SetComponentText(this.userCash, this.cashString + DB.BankCash); // updates new cash variable
    }

    /// <summary>
    /// This method is used to go to the main menu.
    /// Called when main menu buttons are pressed.
    /// </summary>
    public void GotoMainMenu()
    {
        SceneManager.LoadScene(3); // switches scene to main menu
    }

    /// <summary>
    /// This method is used to go to the upgrades screen.
    /// </summary>
    public void GotoUpgrades() // main menu play button causes this method to run
    {
        SceneManager.LoadScene(4);
    }

    /// <summary>
    /// Load swing game with main menu button.
    /// </summary>
    public void GotoPlay() // main menu play button causes this method to run
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// changes scene to the game over menu using main menu button.
    /// </summary>
    public void GameOverMainMenu()
    {
        SceneManager.LoadScene(3);
    }

    // Start is called before the first frame update
    private void Start()
    {
        // these three lines of code link game objects to tagged objects in the scene manager
        this.purchaseButton = GameObject.FindWithTag("upgradePurchaseButtonText");
        this.upgradeDescription = GameObject.FindWithTag("upgradeDescriptionText");
        this.upgradePrice = GameObject.FindWithTag("upgradePriceText");

        // these two gameobjects are used to interact with the preview images in the upgrade menu
        this.gliderImage = GameObject.FindWithTag("GliderImage");
        this.boosterImage = GameObject.FindWithTag("BoosterImage");
        this.userCash = GameObject.FindWithTag("UserCash"); // text box to display a users cash
        this.boosterPrice = "Price:   " + this.bPrice + " Cash";
        this.gliderPrice = "Price:    " + this.gPrice + " Cash";

        // checks that the scene index is the game over screen
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            // creates highscore text on screen
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
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4 && this.firstRun)
        {
            this.SetComponentText(this.userCash, this.cashString + DB.BankCash); // updates the cash string
            this.gliderImage.SetActive(false); // removes the glider image in the upgrade menu
            this.boosterImage.SetActive(false); // removes the booster image in the upgrade menu
            this.firstRun = false; // sets the first run variable false
        }
    }

    // used to tag booster images to the booster game object from other scripts
    private void InitBoosterImages(GameObject booster)
    {
        this.boosterImage = booster;
    }

    // function used to change the text of a text component in a scene
    // requirements to use this function is that the game object has a component of text
    private void SetComponentText(GameObject go, string text)
    {
        go.GetComponent<Text>().text = text; // changes button text
    }

    /// <summary>
    /// This function is used to restart the player to the swinging game.
    /// </summary>
    private void Restart() // on game over scene, button press causes this method to run
    {
        DB.LvlIndex = 0;
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// This function is used to go to the game over screen.
    /// </summary>
    private void Upgrade() // on game over scene, button press causes this method to run
    {
        DB.LvlIndex = 0;
        DB.Glider = 1; // sets flag for glider
        DB.Booster = 1; // sets flag for booster
        SceneManager.LoadScene(0);
    }
}
