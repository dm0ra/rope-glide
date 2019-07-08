using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] cloudArray; 
    void Start()


    {
        cloudArray = GameObject.FindGameObjectsWithTag("cloud"); 
    }

    // Update is called once per frame
    void Update()
    {

         
    int x = 0;
        for (x = 0; x < cloudArray.Length; x++)
        {

            cloudArray[x].transform.position += new Vector3(-0.01f * (float)Random.Range(1, 5), 0, 0);
        }
    }
}
