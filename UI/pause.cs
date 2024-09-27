using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    private playerMovements movementScript;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        movementScript = player.GetComponent<playerMovements>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
            movementScript.CanMove = true;
            AudioListener.volume = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
    }

    void Pause()
    {
            movementScript.CanMove = false;
            AudioListener.volume = 0;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
    }
}
