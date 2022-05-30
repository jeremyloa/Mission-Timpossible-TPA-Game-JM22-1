using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject pauseUI;
    AudioSource[] allAudio;
    public static bool StopAudio = false;
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if (MainCharScript.currHealth <= 0 || MainCharScript.winGame == true)
        {
            Time.timeScale = 0;
            GamePaused = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (StopAudio == false)
            {
                allAudio = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
                foreach (AudioSource audios in allAudio) audios.Stop();
                StopAudio = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused == true && MainCharScript.currHealth > 0) ResumeGame();
            else PauseGame();
        }
    }

    public void ResumeGame ()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        GamePaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PauseGame ()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0;
        GamePaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void quitGame()
    {
        Debug.Log("Back to menu.");
        SceneManager.LoadScene(0);
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        GamePaused = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
