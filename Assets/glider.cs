using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable SA1307 // Accessible fields should begin with upper-case letter
#pragma warning disable SA1300 // Element should begin with upper-case letter
/// <summary>
/// glider takes user input and changes the tilt of the glider
/// that changes the players motion.
/// </summary>
public class glider : MonoBehaviour
#pragma warning restore SA1300 // Element should begin with upper-case letter
{

#pragma warning disable SA1306 // Field names should begin with lower-case letter
#pragma warning disable SA1202 // Elements should be ordered by access
#pragma warning disable SA1401 // Fields should be private

    // The game controller class
#pragma warning disable SA1600 // Elements should be documented
    public GameController game;
#pragma warning restore SA1600 // Elements should be documented

    // Player from unity editor
#pragma warning disable SA1600 // Elements should be documented
    public GameObject Player;
#pragma warning restore SA1600 // Elements should be documented

    // Glider from unity editor
#pragma warning disable SA1600 // Elements should be documented
    public GameObject Glider;
#pragma warning restore SA1600 // Elements should be documented

    // Input class used to see where the user is touching the screen.
    private InputClass gameInput;

    // Player is tilting down
    private bool TiltDownPressed;

    // Player is tilting up
    private bool TiltUpPressed;

    // Player is gliding
    private bool GlidePressed;

    // Used in tap and drag calculation
    private float LastMousePosX;

    // A variable that decreases the effect of tap and drag to change
    // the inclanation of the glider.
    private float dragWeight;

    // Set the intitalposition of the tap */
    private bool firstClick;

    // Csv file for drag and lift coefficents given an angle.
    private CSVParsing CoefData;

    // The prive of the glider in the upgrades menu.
#pragma warning disable SA1600 // Elements should be documented
    public int gliderPrice;
#pragma warning restore SA1600 // Elements should be documented
#pragma warning restore SA1202 // Elements should be ordered by access
#pragma warning restore SA1401 // Fields should be private
#pragma warning restore SA1306 // Field names should begin with lower-case letter
#pragma warning restore SA1307 // Accessible fields should begin with upper-case letter

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        // Initilize varibales
        this.dragWeight = 0.3f;
        this.firstClick = true;
        this.CoefData = GameObject.FindWithTag("Glider").GetComponent<CSVParsing>();
        this.game = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        float startAngle = -90f;
        this.Player = GameObject.FindWithTag("Player");
        this.Glider.transform.position = this.Player.transform.position + new Vector3(0, 10, 0);
        this.Player.transform.eulerAngles = new Vector3(0, 0, startAngle);
        this.Glider.transform.eulerAngles = new Vector3(65, 0, 80 + startAngle);
        this.LastMousePosX = 0f;
        this.gameInput = GameObject.FindWithTag("MainCamera").GetComponent<InputClass>();
        this.gliderPrice = 100;
    }

#pragma warning disable SA1202 // Elements should be ordered by access
                              /// <summary>
                              /// Tells if the glider is currently being activated.
                              /// </summary>
                              /// <returns>Boolean if the glider is being used</returns>
    public bool GliderActive()
#pragma warning restore SA1202 // Elements should be ordered by access
    {

        return this.GlidePressed | this.TiltDownPressed | this.TiltUpPressed;
    }

    /// <summary>
    /// Runs once a frame. Handles glider input
    /// and changes the players speed and direction.
    /// </summary>
    private void Update()
    {
        float dragDistance;
        float newRot;
        float tiltInAngles = this.Glider.transform.eulerAngles.z;

        // If the right side of the screen is being touched
        if (this.gameInput.getInputFlag() == 1 || (this.gameInput.getInputFlag() == 2 && DB.Booster == 0))
        {
            // Save the position of the first click
            if (this.firstClick)
            {
                this.LastMousePosX = Input.mousePosition.x;
            }

            this.firstClick = false;

            // Every frame update the glider position to the player position plus <2,5,0>
            this.Glider.transform.position = this.Player.transform.position + new Vector3(2, 5, 0);

            // Drag left
            if (this.LastMousePosX > Input.mousePosition.x)
            {
                // This is to limit roatation, allow rotation between 60 to -45 degrees
                if ((tiltInAngles >= 319 && tiltInAngles <= 360) || (tiltInAngles <= 60 && tiltInAngles >= 0) || (tiltInAngles < 0 && tiltInAngles > -0.1f))
                {
                    // Get raw drag distance in click position
                    dragDistance = this.LastMousePosX - Input.mousePosition.x;

                    // Put a weight on drag distnace so roation is slower
                    dragDistance *= this.dragWeight;

                    // Create the new rotation
                    newRot = (this.Glider.transform.eulerAngles + new Vector3(0, 0, dragDistance)).z;

                    // This is to limit roatation, allow rotation between 60 to -45 degrees
                    if ((newRot >= 319 && newRot <= 365) || (newRot <= 60 && newRot >= -5) || (newRot < 0 && newRot > -0.5f))
                    {
                        // Set the new rotation of the glider and the player
                        this.Player.transform.eulerAngles = this.Player.transform.eulerAngles + new Vector3(0, 0, dragDistance);
                        this.Glider.transform.eulerAngles = this.Glider.transform.eulerAngles + new Vector3(0, 0, dragDistance);
                    }
                    else
                    {
                        // Set the glider to the maximum rotation allowed
                        this.Glider.transform.eulerAngles = new Vector3(this.Glider.transform.eulerAngles.x, this.Glider.transform.eulerAngles.y, 60);
                    }

                    newRot = (this.Glider.transform.eulerAngles + new Vector3(0, 0, dragDistance)).z;
                }

                // Changes the players movement based on glider
                this.SetNewPlayerVelocity();
            }

            // Drag right
            else if (this.LastMousePosX < Input.mousePosition.x)
            {
                // This is to limit roatation, allow rotation between 60 to -45 degrees
                if ((tiltInAngles >= 320 && tiltInAngles <= 360) || (tiltInAngles <= 61 && tiltInAngles >= 0) || (tiltInAngles < 0 && tiltInAngles > -0.1f))
                {
                    // Get raw drag distance in click position
                    dragDistance = this.LastMousePosX - Input.mousePosition.x;

                    // Put a weight on drag distnace so roation is slower
                    dragDistance *= this.dragWeight;

                    // Create the new rotation
                    newRot = (this.Glider.transform.eulerAngles + new Vector3(0, 0, dragDistance)).z;

                    // This is to limit roatation, allow rotation between 60 to -45 degrees
                    if ((newRot >= 320 && newRot <= 365) || (newRot <= 61 && newRot >= -5) || (newRot < 0 && newRot > -0.5f))
                    {
                        // Set the new rotation of the glider and the player
                        this.Player.transform.eulerAngles = this.Player.transform.eulerAngles + new Vector3(0, 0, dragDistance);
                        this.Glider.transform.eulerAngles = this.Glider.transform.eulerAngles + new Vector3(0, 0, dragDistance);
                    }
                    else
                    {
                        // Set the glider to the maximum rotation allowed
                        this.Glider.transform.eulerAngles = new Vector3(this.Glider.transform.eulerAngles.x, this.Glider.transform.eulerAngles.y, 320);
                    }

                    newRot = (this.Glider.transform.eulerAngles + new Vector3(0, 0, dragDistance)).z;
                }

                // Changes the players movement based on glider
                this.SetNewPlayerVelocity();
            }
            else
            {
                this.GlidePressed = true;
                this.SetNewPlayerVelocity();
            }
        }
        else
        {
            this.firstClick = true;
            this.GlidePressed = false;
            this.TiltDownPressed = false;
            this.TiltUpPressed = false;
            this.Glider.transform.position = this.Player.transform.position + new Vector3(0, 0, 100);
        }

        // Save the last touch position to see touch and drag distance.
        this.LastMousePosX = Input.mousePosition.x;
    }

    /// <summary>
    /// Gets the lift coefficent for the glider.
    /// </summary>
    /// <param name="tilt"> The angle of the glider </param>
    /// <returns>Lift coefficent</returns>
    private float GetLiftCoefficent(float tilt)
    {
        try
        {
            float coef = -99;
            int x;
            for (x = 0; x < 176; x++)
            {
                float[] angle_Coef = new float[2];
                angle_Coef = this.CoefData.getLiftCoef(x);

                if (tilt < angle_Coef[0])
                {
                    if (x == 0)
                    {
                        coef = angle_Coef[1];
                    }

                    coef = (angle_Coef[1] + this.CoefData.getLiftCoef(x - 1)[1]) / 2;
                    break;
                }

            }

            return coef;
        }
#pragma warning disable CS0168 // Variable is declared but never used
        catch (System.Exception x)
#pragma warning restore CS0168 // Variable is declared but never used
        {
            Debug.Log("Lift Coefficent Error");
        }

        return -1;
    }
    /// <summary>
    /// Gets the drag coefficent for the glider.
    /// </summary>
    /// <param name="tilt"> The angle of the glider. </param>
    /// <returns>Lift coefficent.</returns>
    private float GetDragCoefficent(float tilt)
    {
        try
        {
            float coef = -99;
            int x;
            for (x = 0; x < 229; x++)
            {
                float[] angle_Coef = new float[2];
                angle_Coef = this.CoefData.getDragCoef(x);

                if (tilt < angle_Coef[0])
                {
                    if (x == 0)
                    {
                        coef = angle_Coef[1];
                    }

                    coef = (angle_Coef[1] + this.CoefData.getDragCoef(x - 1)[1]) / 2;
                    break;
                }
            }

            return coef;
        }
        #pragma warning disable CS0168 // Variable is declared but never used
        catch (System.Exception x)
        #pragma warning restore CS0168 // Variable is declared but never used
        {
            Debug.Log("drag Coefficent Error");
        }

        return -1;
    }

    /// <summary>
    /// Sets the players new movment, is called once a frame when screen 
    /// is being touched.
    /// </summary>
    private void SetNewPlayerVelocity()
    {
        // Area of wing
        float area = 0.5f;
        float airDensity = 1.225f;

        // Current velocity is weighted down to help glider function
        float currVelo = this.Player.GetComponent<Rigidbody2D>().velocity.magnitude;
        currVelo = currVelo / 2.5f;

        // Angle of the glider
        float tiltInAngles;
        if ((this.Glider.transform.eulerAngles.z > 0 && this.Glider.transform.eulerAngles.z <= 61) || this.Glider.transform.eulerAngles.z == 0)
        {

            tiltInAngles = this.Glider.transform.eulerAngles.z;
        }
        else
        {
            tiltInAngles = this.Glider.transform.eulerAngles.z - 360;
        }

        float tiltInRads = 0.0174533f * tiltInAngles;

        // Get Coefficents
        float liftCoefficent;
        liftCoefficent = this.GetLiftCoefficent(tiltInAngles);// 2 * Mathf.PI * tiltInRads;
        float dragCoefficent = this.GetDragCoefficent(tiltInAngles);// 1.28f * Mathf.Sin(tiltInRads);//

        // Calculate lift and drag forces
        float lift = liftCoefficent * ((currVelo * currVelo * airDensity) / 2) * area;
        float drag = dragCoefficent * (currVelo * currVelo * airDensity / 2) * area;
        float weight = 9.8f * this.Player.GetComponent<Rigidbody2D>().mass;
        float stallSpeed = Mathf.Sqrt((2f * weight * 9.8f) / (airDensity * area * (2 * Mathf.PI * 0.785398f)));

        // Calculate component forces of lift and drag.
        float veritcalLift = lift * Mathf.Cos(tiltInRads); 
        float horizontalLift = lift * Mathf.Sin(tiltInRads);
        float verticalDrag = drag * Mathf.Sin(tiltInRads);
        float horizontalDrag = drag * Mathf.Cos(tiltInRads);

        float balenceVertical = veritcalLift + verticalDrag - weight;
        float balenceHorizontal = horizontalLift - horizontalDrag;

        float stallspeed = 30f;
        float maxspeed = 300f;

        // Normal glider function less than max speend and greater than stall speed
        if (this.Player.GetComponent<Rigidbody2D>().velocity.x > stallspeed && this.Player.GetComponent<Rigidbody2D>().velocity.magnitude < maxspeed)
        {
            Debug.Log("NORMALSPEED");

            // Tilted Down
            if (tiltInAngles < 0)
            {
                // Add the drag and lift forces to the player
                this.Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontalLift, veritcalLift));
                this.Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1f * horizontalDrag, verticalDrag));

                balenceVertical = veritcalLift + verticalDrag - weight;
                balenceHorizontal = horizontalLift - horizontalDrag;
            }

            // Tilted up
            else
            {
                // Add the drag and lift forces to the player
                this.Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(
                    (-1f * horizontalLift) - horizontalDrag,
                    veritcalLift - verticalDrag));

                balenceVertical = veritcalLift - verticalDrag - weight;
                balenceHorizontal = (-1f * horizontalLift) - horizontalDrag;
            }
        }

        // Low speed reduce the effectiveness of the glider
        else if (this.Player.GetComponent<Rigidbody2D>().velocity.x > 0f && this.Player.GetComponent<Rigidbody2D>().velocity.x < stallspeed &&
            this.Player.GetComponent<Rigidbody2D>().velocity.magnitude < maxspeed)
        {
            Debug.Log("LOW x SPEED");

            // Damp the lift and drag forces by (Player.Velo.X/30) Player.Velo.X will be less than 30
            horizontalLift = horizontalLift * (this.Player.GetComponent<Rigidbody2D>().velocity.x / stallspeed);
            veritcalLift = veritcalLift * (this.Player.GetComponent<Rigidbody2D>().velocity.x / stallspeed);
            horizontalDrag = horizontalDrag * (this.Player.GetComponent<Rigidbody2D>().velocity.x / stallspeed);
            verticalDrag = verticalDrag * (this.Player.GetComponent<Rigidbody2D>().velocity.x / stallspeed);

            // Tilted Down
            if (tiltInAngles < 0)
            {
                // Add the drag and lift forces to the player
                this.Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontalLift, veritcalLift));
                this.Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1f * horizontalDrag, verticalDrag));

                balenceVertical = veritcalLift + verticalDrag - weight;
                balenceHorizontal = horizontalLift - horizontalDrag;
            }

            // Tilted up
            else
            {
                // Add the drag and lift forces to the player
                this.Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(
                    (-1f * horizontalLift) - horizontalDrag,
                    veritcalLift - verticalDrag));

                balenceVertical = veritcalLift - verticalDrag - weight;
                balenceHorizontal = (-1f * horizontalLift) - horizontalDrag;
            }
        }

        // Highspeed reduce the gliders effectiveness to prevent from runaway glider
        else if (this.Player.GetComponent<Rigidbody2D>().velocity.magnitude > maxspeed && this.Player.GetComponent<Rigidbody2D>().velocity.x > 0f)
        {
            // Top speed disable all glider function
            if (this.Player.GetComponent<Rigidbody2D>().velocity.y > 350)
            {
                Debug.Log("Vertical speed limit reached  STALL");
            }
            else
            {
                Debug.Log("HIGHSPEED" + "  vertical speed " + this.Player.GetComponent<Rigidbody2D>().velocity.y);

                horizontalLift = horizontalLift * (maxspeed / this.Player.GetComponent<Rigidbody2D>().velocity.magnitude);
                veritcalLift = veritcalLift * (maxspeed / this.Player.GetComponent<Rigidbody2D>().velocity.magnitude);
                horizontalDrag = horizontalDrag * 1;
                verticalDrag = verticalDrag * 1;

                // Tilted Down
                if (tiltInAngles < 0)
                {
                    // Add the drag and lift forces to the player
                    this.Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontalLift, veritcalLift));
                    this.Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1f * horizontalDrag, verticalDrag));

                    balenceVertical = veritcalLift + verticalDrag - weight;
                    balenceHorizontal = horizontalLift - horizontalDrag;
                }

                // Tilted up
                else
                {
                    // Add the drag and lift forces to the player
                    this.Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(
                        (-1f * horizontalLift) - horizontalDrag,
                        veritcalLift - verticalDrag));

                    balenceVertical = veritcalLift - verticalDrag - weight;
                    balenceHorizontal = (-1f * horizontalLift) - horizontalDrag;
                }
            }
        }

        // Vertical speed to high stall glider
        else
        {
            Debug.Log("STALL" + "  vertical speed " + this.Player.GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}
