using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    bool playLose = false;
    AudioSource src;
    public GameObject loseUi;
    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        src.Pause();
        loseUi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (MainCharScript.currHealth == 0) StopGame();
    }

    public void StopGame()
    {
        loseUi.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (PauseMenu.StopAudio == true && playLose == false)
        {
            src.Play();
            playLose = true;
        }
    }

    public void BacktoMain()
    {
        Debug.Log("Back to menu.");
        SceneManager.LoadScene(0);
        loseUi.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
