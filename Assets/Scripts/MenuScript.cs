using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public string level1 = "Level1(City)";
    public string level2 = "Level2(Clouds)";
    public string level3 = "Level3(Space)";

    public void loadLevel1() 
    {
        SceneManager.LoadScene(sceneName: level1);
    }

    public void loadLevel2()
    {
        SceneManager.LoadScene(sceneName: level2);
    }

    public void loadLevel3()
    {
        SceneManager.LoadScene(sceneName: level3);
    }

    public void quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
