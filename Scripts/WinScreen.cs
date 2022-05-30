using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    bool playWin = false;
    AudioSource src;
    public GameObject winUi;
    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        src.Pause();
        winUi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (MainCharScript.winGame == true) StopGame();
    }

    public void StopGame()
    {
        winUi.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (PauseMenu.StopAudio == true && playWin == false)
        {
            src.Play();
            playWin = true;
        }
    }

    public void BacktoMain()
    {
        Debug.Log("Back to menu.");
        SceneManager.LoadScene(0);
        winUi.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
