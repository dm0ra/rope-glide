using UnityEngine.SceneManagement;
/*
 *  A class that controls scene switching 
 */
public class SceneSwitch
{
    public int sceneIndex;

    /*
     * switch scene to index. The index for scenes are set in build settings
     */
    public void SwitchScenes(int index)
    {
        sceneIndex = index;
        SceneManager.LoadScene(index);
    }

    /*
     * Get current scene index.
     */
    public int GetSceneIndex()
    {
        return sceneIndex;
    }
}
