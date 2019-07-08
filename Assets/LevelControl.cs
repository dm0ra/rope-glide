using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    public int LvlIndex;
    public int requestLvlChange;
    // Start is called before the first frame update

    //private void Update()
   // {
        //Debug.Log("sugma");
     //   if(requestLvlChange == 1)
    //    {
   //         SceneManager.LoadScene(2);
   //         requestLvlChange = 0;
   //     }
   // }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(LvlIndex);
        }
    }

    public void restart()
    {
        GameObject game = GameObject.FindWithTag("Game Controller"); 
        SceneManager.LoadScene(0);
    }
}
