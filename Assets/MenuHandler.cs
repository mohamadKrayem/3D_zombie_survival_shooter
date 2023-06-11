using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuHandler : MonoBehaviour
{
    public GameObject PauseCanvas;
    bool isGamePaused;
    public GameObject GameOverCanvas;
    bool isGameOver;
    public TextMeshProUGUI scoreText;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
         //hide the pause canvas and the game over canvas
        PauseCanvas.SetActive(false);
        GameOverCanvas.SetActive(false);
        isGameOver = false;
        isGamePaused = false;
    }


    // Update is called once per frame
    void Update()
    {
         // If the user clicks on the 'p' key and the game is not over, pause the game
        if (Input.GetKeyDown(KeyCode.P) && !isGameOver)
            PauseGame();
    }

   // This method is called when the user clicks on the 'p' key to pause the game
    public void PauseGame()
    {
        isGamePaused = !isGamePaused;

        //activate the pause canvas
        PauseCanvas.SetActive(isGamePaused);

        // If the game is paused, unlock the cursor and make it visible, 
        // otherwise lock the cursor and make it invisible
        if (isGamePaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

         // If the game is paused, stop the game, otherwise resume the game and make it runs normally.
        Time.timeScale = isGamePaused ? 0 : 1;
    }

   // This method is called when the user clicks on the 'Restart' button to restart the game
    public void RestartGame()
    {
        Time.timeScale = 1;
        string sceneName = SceneManager.GetActiveScene().name;

        // Reload the current scene
        SceneManager.LoadScene(sceneName);
    }

   // This method is called when the user clicks on the 'Quit' button to quit the game
    public void QuitGame()
    {
      Time.timeScale = 0;
        Application.Quit();
    }

   // This method is called when the player dies
    public void GameOver()
    {
        isGameOver = true;

        //show the game over canvas, which contains the score
        scoreText.text = "Score: " + score;
        GameOverCanvas.SetActive(isGameOver);

        //if the game is over, unlock the cursor and make it visible and stop the running of the game
        //otherwise lock the cursor and make it invisible and resume the running of the game
        if (isGameOver)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }
}
