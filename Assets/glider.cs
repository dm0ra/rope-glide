using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glider : MonoBehaviour
{
    /* The game controller class */
    public GameController game;
    /* Player from unity editor */
    public GameObject Player;
    /*  Glider from unity editor */
    public GameObject Glider;    
    /* Input class used to see where the user is touching the screen  */
    private InputClass gameInput;   
    /*  */
    private bool TiltDownPressed;
    /*  */
    private bool TiltUpPressed;
    /*  */
    private bool GlidePressed;
    /* Used in tap and drag calculation */
    private float LastMousePosX;   
    /* A variable that decreases the effect of tap and drag to change
       the inclanation of the glider */
    private float dragWeight;
    /* Set the intitalposition of the tap */
    private bool firstClick;
    /* Csv file for drag and lift coefficents given an angle */
    CSVParsing CoefData;

    // Start is called before the first frame update
    void Start()
    {
        //Initilize varibales
        dragWeight = 0.3f;
        firstClick = true;
        CoefData = GameObject.FindWithTag("Glider").GetComponent<CSVParsing>();
        game = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        float startAngle = -90f;
        Player = GameObject.FindWithTag("Player");
        //Player.transform.rotation = new Quaternion(0, 0, -90, 0);
        Glider.transform.position = Player.transform.position + new Vector3(0, 10, 0);
        Player.transform.eulerAngles = new Vector3(0, 0, startAngle);
        Glider.transform.eulerAngles = new Vector3(65, 0, 80 + startAngle);
        LastMousePosX = 0f;
        gameInput = GameObject.FindWithTag("MainCamera").GetComponent<InputClass>(); 
    }
    public bool GliderActive()
    {
        return GlidePressed | TiltDownPressed | TiltUpPressed;
    }
    // Update is called once per frame
    void Update()
    {
 
        float tilt = 1f;
        float drag = 0.1f;
        float dragDistance;
        float newRot;
        float tiltInAngles = Glider.transform.eulerAngles.z;

        //If the right side of the screen is being touched
        if (gameInput.getInputFlag() == 1 || (gameInput.getInputFlag() == 2 && DB.Booster == 0))
        {
            // Save the position of the first click
            if (firstClick)
            {
                LastMousePosX = Input.mousePosition.x;
            }

         
      
        

            firstClick = false;
            //Every frame update the glider position to the player position plus <2,5,0>
            Glider.transform.position = Player.transform.position + new Vector3(2, 5, 0);

            //Drag left
            if (LastMousePosX > Input.mousePosition.x)
            {
                //This is to limit roatation, allow rotation between 60 to -45 degrees
                if (tiltInAngles >= 319 && tiltInAngles <= 360 || tiltInAngles <= 60 && tiltInAngles >= 0 || tiltInAngles < 0 && tiltInAngles > -0.1f)
                { 
                    //Get raw drag distance in click position
                    dragDistance = LastMousePosX - Input.mousePosition.x;
                    //Put a weight on drag distnace so roation is slower
                    dragDistance *= dragWeight;
                    //Create the new rotation
                    newRot = (Glider.transform.eulerAngles + new Vector3(0, 0, dragDistance)).z;
                    //This is to limit roatation, allow rotation between 60 to -45 degrees 
                    if (newRot >= 319 && newRot <= 365 || newRot <= 60 && newRot >= -5 || newRot < 0 && newRot > -0.5f)
                    {
                        //Set the new rotation of the glider and the player
                        Player.transform.eulerAngles = Player.transform.eulerAngles + new Vector3(0, 0, dragDistance);
                        Glider.transform.eulerAngles = Glider.transform.eulerAngles + new Vector3(0, 0, dragDistance);
                    }
                    else
                    {
                        //Set the glider to the maximum rotation allowed
                        Glider.transform.eulerAngles = new Vector3(Glider.transform.eulerAngles.x, Glider.transform.eulerAngles.y, 60);
                    }

                    newRot = (Glider.transform.eulerAngles + new Vector3(0, 0, dragDistance)).z;
                   // Debug.Log("New Rotation: " + newRot);
                } 
                setNewPlayerVelocity();
            }
            //Drag right
            else if (LastMousePosX < Input.mousePosition.x)
            {
                //This is to limit roatation, allow rotation between 60 to -45 degrees
                if (tiltInAngles >= 320 && tiltInAngles <= 360 || tiltInAngles <= 61 && tiltInAngles >= 0 || tiltInAngles < 0 && tiltInAngles > -0.1f)
                {
                    //Get raw drag distance in click position
                    dragDistance = LastMousePosX - Input.mousePosition.x;
                    //Put a weight on drag distnace so roation is slower
                    dragDistance *= dragWeight;
                    //Create the new rotation
                    newRot = (Glider.transform.eulerAngles + new Vector3(0, 0, dragDistance)).z;
                    //This is to limit roatation, allow rotation between 60 to -45 degrees
                    if (newRot >= 320 && newRot <= 365 || newRot <= 61 && newRot >= -5 || newRot < 0 && newRot > -0.5f)
                    {
                        //Set the new rotation of the glider and the player
                        Player.transform.eulerAngles = Player.transform.eulerAngles + new Vector3(0, 0, dragDistance);
                        Glider.transform.eulerAngles = Glider.transform.eulerAngles + new Vector3(0, 0, dragDistance);
                    }
                    else
                    {
                        //Set the glider to the maximum rotation allowed
                        Glider.transform.eulerAngles = new Vector3(Glider.transform.eulerAngles.x, Glider.transform.eulerAngles.y, 320);
                    }

                    newRot = (Glider.transform.eulerAngles + new Vector3(0, 0, dragDistance)).z;
                   // Debug.Log("New Rotation: " + newRot);
                } 
                setNewPlayerVelocity();
            }
            else
            {
                GlidePressed = true;
                setNewPlayerVelocity();
            }
        }
        else
        {
            firstClick = true;
            GlidePressed = false;
            TiltDownPressed = false;
            TiltUpPressed = false;
            Glider.transform.position = Player.transform.position + new Vector3(0, 0, 100);
        }
        LastMousePosX = Input.mousePosition.x;
    }
    private float getLiftCoefficent(float tilt)
    {
        try
        {
            float coef = -99;
            int x;
            for (x = 0; x < 176; x++)
            {
                float[] Angle_Coef = new float[2];
                Angle_Coef = CoefData.getLiftCoef(x);

                if (tilt < Angle_Coef[0])
                {
                    if (x == 0)
                    {
                        coef = Angle_Coef[1];
                    }
                    coef = (Angle_Coef[1] + CoefData.getLiftCoef(x - 1)[1]) / 2;
                    break;
                }

            }
            return coef;
        }
        catch (System.Exception x)
        {
            Debug.Log("Lift Coefficent Error");
        }
        return -99;
    }
    private float getDragCoefficent(float tilt)
    {
        try
        {
            float coef = -99;
            int x;
            for (x = 0; x < 229; x++)
            {
                float[] Angle_Coef = new float[2];
                Angle_Coef = CoefData.getDragCoef(x);

                if (tilt < Angle_Coef[0])
                {
                    if (x == 0)
                    {
                        coef = Angle_Coef[1];
                    }
                    coef = (Angle_Coef[1] + CoefData.getDragCoef(x - 1)[1]) / 2;
                    break;
                }

            }

            return coef;
        }
        catch (System.Exception x)
        {
            Debug.Log("drag Coefficent Error");
        }
        return -99;
    }
 
    void setNewPlayerVelocity()
    {
        float Area = 0.5f;//0.055
        float airDensity = 1.225f;//1.225f; //kg/m^3
        float CurrVelo = Player.GetComponent<Rigidbody2D>().velocity.magnitude; 
        CurrVelo = CurrVelo / 2.5f; 
        float tiltInAngles;
        if (Glider.transform.eulerAngles.z > 0 && Glider.transform.eulerAngles.z <= 61 || Glider.transform.eulerAngles.z == 0)
        {

            tiltInAngles = Glider.transform.eulerAngles.z;
        }
        else
        {
            tiltInAngles = Glider.transform.eulerAngles.z - 360;
        }
        //tiltInAngles = tiltInAngles - 90f;
        float tiltInRads = 0.0174533f * tiltInAngles;
        float liftCoefficent;

        /*if(tiltInAngles < 0)
            liftCoefficent = 2 * Mathf.PI * (-1f*tiltInRads);
        else
            liftCoefficent = 2 * Mathf.PI * tiltInRads; //Decending glider Should get this*/
        liftCoefficent = getLiftCoefficent(tiltInAngles);//2 * Mathf.PI * tiltInRads;
        float dragCoefficent = getDragCoefficent(tiltInAngles);//1.28f * Mathf.Sin(tiltInRads);// 

        float Lift = liftCoefficent * ((CurrVelo * CurrVelo * airDensity) / 2) * Area;
        float Drag = (dragCoefficent * ((CurrVelo * CurrVelo * airDensity) / 2) * Area);
        float weight = 9.8f * Player.GetComponent<Rigidbody2D>().mass;
        float stallSpeed = Mathf.Sqrt((2f * weight * 9.8f) / (airDensity * Area * (2 * Mathf.PI * 0.785398f)));

        float VeritcalLift = Lift * Mathf.Cos(tiltInRads);


        float HorizontalLift = Lift * Mathf.Sin(tiltInRads);
        float VerticalDrag = Drag * Mathf.Sin(tiltInRads);
        float HorizontalDrag = Drag * Mathf.Cos(tiltInRads);
        if (VeritcalLift > 3000f)
        {
            //VeritcalLift = 2000f;
          //  Debug.Log("Current Velo: " + CurrVelo + "   Vertical Lift: " + VeritcalLift + "   Vertical Drag: " + VerticalDrag);

        }
        float balenceVertical = VeritcalLift + VerticalDrag - weight;
        float balenceHorizontal = HorizontalLift - HorizontalDrag;
        float stallspeed = 30f;
        float maxspeed = 300f;

        /*if(VeritcalLift + VerticalDrag > weight)
        {
            VeritcalLift = (VeritcalLift + VerticalDrag) - weight;
        }*/
        if(Player.GetComponent<Rigidbody2D>().velocity.x > maxspeed)
        {
          //  Debug.Log("highspeeeeed");
        }
        if (Player.GetComponent<Rigidbody2D>().velocity.x > stallspeed && Player.GetComponent<Rigidbody2D>().velocity.magnitude < maxspeed)
        {
            Debug.Log("NORMALSPEED");
            if (tiltInAngles < 0)
            {
                //layer.GetComponent<Rigidbody2D>().AddForce(new Vector2(HorizontalLift - HorizontalDrag
                //, VeritcalLift + VerticalDrag));
                Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(HorizontalLift, VeritcalLift));
                Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1f * HorizontalDrag, VerticalDrag));

                balenceVertical = VeritcalLift + VerticalDrag - weight;
                balenceHorizontal = HorizontalLift - HorizontalDrag;
            }
            else
            {
                Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1f * HorizontalLift - HorizontalDrag
                , VeritcalLift - VerticalDrag));

                balenceVertical = VeritcalLift - VerticalDrag - weight;
                balenceHorizontal = -1f * HorizontalLift - HorizontalDrag;
            }
        }
        else if(Player.GetComponent<Rigidbody2D>().velocity.x > 0f && Player.GetComponent<Rigidbody2D>().velocity.x < stallspeed &&
            Player.GetComponent<Rigidbody2D>().velocity.magnitude < maxspeed)
        {
            Debug.Log("LOW x SPEED");
            HorizontalLift = HorizontalLift * (Player.GetComponent<Rigidbody2D>().velocity.x / stallspeed);
            VeritcalLift = VeritcalLift * (Player.GetComponent<Rigidbody2D>().velocity.x / stallspeed);
           HorizontalDrag = HorizontalDrag * (Player.GetComponent<Rigidbody2D>().velocity.x / stallspeed);
            VerticalDrag = VerticalDrag * (Player.GetComponent<Rigidbody2D>().velocity.x / stallspeed);
            if (tiltInAngles < 0)
            {
                //layer.GetComponent<Rigidbody2D>().AddForce(new Vector2(HorizontalLift - HorizontalDrag
                //, VeritcalLift + VerticalDrag));

                Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(HorizontalLift, VeritcalLift));
                Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1f * HorizontalDrag, VerticalDrag));

                balenceVertical = VeritcalLift + VerticalDrag - weight;
                balenceHorizontal = HorizontalLift - HorizontalDrag;
            }
            else
            {
                Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1f * HorizontalLift - HorizontalDrag
                , VeritcalLift - VerticalDrag));

                balenceVertical = VeritcalLift - VerticalDrag - weight;
                balenceHorizontal = -1f * HorizontalLift - HorizontalDrag;
            }
        }
        else if(Player.GetComponent<Rigidbody2D>().velocity.magnitude > maxspeed && Player.GetComponent<Rigidbody2D>().velocity.x > 0f)
        {
            if (Player.GetComponent<Rigidbody2D>().velocity.y > 350)
            {
                Debug.Log("Vertical speed limit reached  STALL");
            }
            else
            {
                Debug.Log("HIGHSPEED" + "  vertical speed " + Player.GetComponent<Rigidbody2D>().velocity.y);

                HorizontalLift = HorizontalLift * (maxspeed / Player.GetComponent<Rigidbody2D>().velocity.magnitude);
                VeritcalLift = VeritcalLift * (maxspeed / Player.GetComponent<Rigidbody2D>().velocity.magnitude);
                HorizontalDrag = HorizontalDrag; //* (maxspeed / Player.GetComponent<Rigidbody2D>().velocity.x);
                VerticalDrag = VerticalDrag;//* (maxspeed / Player.GetComponent<Rigidbody2D>().velocity.x);
                if (tiltInAngles < 0)
                {
                    //layer.GetComponent<Rigidbody2D>().AddForce(new Vector2(HorizontalLift - HorizontalDrag
                    //, VeritcalLift + VerticalDrag));

                    Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(HorizontalLift, VeritcalLift));
                    Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1f * HorizontalDrag, VerticalDrag));

                    balenceVertical = VeritcalLift + VerticalDrag - weight;
                    balenceHorizontal = HorizontalLift - HorizontalDrag;
                }
                else
                {
                    Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1f * HorizontalLift - HorizontalDrag
                    , VeritcalLift - VerticalDrag));

                    balenceVertical = VeritcalLift - VerticalDrag - weight;
                    balenceHorizontal = -1f * HorizontalLift - HorizontalDrag;
                }
            }
        }
        else
        {
            Debug.Log("STALL" + "  vertical speed " + Player.GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}
