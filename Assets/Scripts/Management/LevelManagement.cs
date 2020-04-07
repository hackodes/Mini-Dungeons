﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManagement : MonoBehaviour
{
    public static LevelManagement manager;

    public float delayTimer = 3f; // The wait 

    public string levelName;

    public bool isPaused;

    public int playerCoins;

    private void Awake()
    {
        manager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f; // Make the time back to normal when new game is loaded

        UserInterfaceController.UIcontroller.playerCoins.text = playerCoins.ToString(); // Set the coin text to the playerCoins
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Pause the game when key P is pressed
        {
            pauseLevel();
        }
    }

    public void pauseLevel()
    {
        if (!isPaused == true) // If the game isnt paused then pause it
        {
            UserInterfaceController.UIcontroller.pauseUI.SetActive(true);
            UserInterfaceController.UIcontroller.pauseButton.enabled = false;
            isPaused = true;

            pauseTime(0f);
            
        }
        else // Otherwise it is paused so unpause it
        {
            resumeLevel();

            isPaused = false;
            UserInterfaceController.UIcontroller.pauseButton.enabled = true;
            pauseTime(1f);
        }
    }

    public void pauseTime(float value) // Pauses the current time or 
    {
        Time.timeScale = value;
    }


    public void resumeLevel()
    {
        UserInterfaceController.UIcontroller.pauseUI.SetActive(false);
    }

    public IEnumerator exitDungeon() // Coroutine
    {
        // AudioManager.instance.PlayVictory();
        PlayerController.player.isActivated = false;

        UserInterfaceController.UIcontroller.startFadeIn(); // Fading background

        yield return new WaitForSeconds(delayTimer);  // Wait for some time

        SceneManager.LoadScene(levelName);


    }

    public void getCoins(int value) // Gives the player coins after an event
    {
        playerCoins = playerCoins + value;
        UserInterfaceController.UIcontroller.playerCoins.text = playerCoins.ToString();
    }

    public void useCoins(int value) // Using coins to buy things
    {
        playerCoins = playerCoins - value;

        if (playerCoins < 0) 
        {
            playerCoins = 0;
        }
        UserInterfaceController.UIcontroller.playerCoins.text = playerCoins.ToString();
    }
}
