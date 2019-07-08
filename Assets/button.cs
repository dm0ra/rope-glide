using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{

    void OnMouseDown()
    {
        // load a new scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

}
