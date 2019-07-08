using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputClass : MonoBehaviour
{
    private float cameraStartPos;
    private float currCameraPos;
    private float cameraWidth;
    private int ClickFlag; // represents what side of the screen is clicked

    private float LastMousePosX;
    public GameObject Camera;//camera object


    private float waitTime = 0.05f;
    private float timer = 0.0f;
    private float touchOffest;
    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.FindWithTag("MainCamera");//links camera object
        cameraStartPos = transform.position.x;
        cameraWidth = 1000;
        ClickFlag = -1;
        if (  (Application.platform == RuntimePlatform.IPhonePlayer) || SystemInfo.operatingSystem.Contains("Mac"))
        {
            touchOffest = 710f;
            Debug.Log("Running on iphone or mac");
        }
        else
        {
            touchOffest = 530f;
            Debug.Log("Running on pc");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Working");
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Screen Height: " + Screen.height);
            //Debug.Log("Screen Input y pos: " + Input.mousePosition.y);

            currCameraPos = transform.position.x;
             
           float touchPositionX = (Input.mousePosition.x) - touchOffest + transform.position.x;//x position for touch
           float touchPositiony = (Input.mousePosition.y) - touchOffest + transform.position.y;//y position for y
           float MiddleVerticalBoundry =  transform.position.x;//sets vertical boundary to divide touches
           float MiddleHorizontalBoundry = Camera.transform.position.y/2;//sets horizontal boundary to divide touches
           if (touchPositionX > MiddleVerticalBoundry)
            {
                if(Input.mousePosition.y > Screen.height/2)
                {
                    ClickFlag = 2; // Top Right
                    //Debug.Log("Woah");
                }
                else
                {
                    ClickFlag = 1;//Bottom Right
                }

            }
            else
            {
                ClickFlag = 0; // Left
             }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ClickFlag = -1; // No Click
        } 
    }

    public int getInputFlag()
    {
        return ClickFlag;
    }
   
}
