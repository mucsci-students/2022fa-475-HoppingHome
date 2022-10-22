using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

  public string level1 = "Level1(City)";

  public void loadLevel1() 
  {
    SceneManager.LoadScene(sceneName: level1);
  }

}
