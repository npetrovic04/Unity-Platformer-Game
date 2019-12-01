using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public void LoadScene(string sceneName)
    {    
        SceneManager.LoadScene(sceneName);

    }

    /// <summary>
    /// It loads the current scene
    /// </summary>
    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        GameCtrl.instance.data.keyFound[0] = false;
        GameCtrl.instance.data.keyFound[1] = false;
        GameCtrl.instance.data.keyFound[2] = false;

        GameCtrl.instance.DeleteCheckPoints();
    }
}
