using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TriggerCutscene : MonoBehaviour
{
    [SerializeField]
    private string sceneName;
  
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
