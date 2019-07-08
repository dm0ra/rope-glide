using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 *  A class that controls scene switching 
 */
public class SceneSwitch
{
    public int sceneIndex;

    /*
     * switch scene to index. The index for scences are set in build settings
     */
    public void switchScenes(int index)
    {
        sceneIndex = index;
        SceneManager.LoadScene(index);
    }

    /*
     * Get Current scene index.
     */
    public int getSceneIndex()
    {
        return sceneIndex;
    }
}
