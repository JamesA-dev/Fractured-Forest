using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScene2 : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {   
        SceneManager.LoadScene(sceneName);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
