using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // These should eventually be the cutscene entrance scenes
    public string level1 = "CityEnterCutscene";
    public string l2 = "CloudEnterCutscene";
    public string level3 = "Level3(Space)";

    public void loadLevel1() 
    {
        SceneManager.LoadScene(sceneName: level1);
    }

    public void loadLevel2()
    {
        SceneManager.LoadScene(l2);
    }

    public void loadLevel3()
    {
        SceneManager.LoadScene(sceneName: level3);
    }

    // Play the into cutscene when playing from the start w/ play button
    public void playButton()
    {
        SceneManager.LoadScene("OpeningCutscene");
    }

    public void quit()
    {
        Application.Quit();
        // UnityEditor.EditorApplication.isPlaying = false;
    }

}
