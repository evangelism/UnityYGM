//-----------------------------------------
//   GameController.cs
//
//   Jason Walters
//   http://glitchbeam.com
//   @jasonrwalters
//
//   last edited on 3/23/2015
//-----------------------------------------

using UnityEngine;
using System.Collections;

public enum GameStates
{
    MainMenu, GamePlay, GameOver
}

public class GameController : MonoBehaviour 
{
    public static GameStates gameState;
    public static bool changeState = false;
    public static float gameScore;
    public static float gameHighScore;
    public static float audioVolume;
    public static int audioEnabled;
    public static float gameSpeed;

    public AudioClip sndMainMenu;
    public AudioClip sndPlayGame;
    public AudioClip sndReplayGame;
    public AudioClip sndGameOver;
    public float maxVolume = 0.75f;
    public float maxSpeed = 0.0f;

    private AudioSource audioSrc;
    private GameObject[] go0, go1, go2;
    private GameObject goBack;
    private int firstTimeLaunch;

	// Use this for initialization
	void Start ()
    {
        // cache components
        audioSrc = GetComponent<AudioSource>();

        // setup game settings
        SettingsDefault();

        // setup game objects
        FindGameObjects();

        // start our game states
        GameState();
	}
	
	// Update is called once per frame
	void Update () 
    {
        KeysPressed();
        GameScore();

        if (changeState) GameState();
	}

    //-----------------------------------------
    //   SETUP
    //-----------------------------------------
    void SettingsDefault()
    {
        // first time playing?
        firstTimeLaunch = PlayerPrefs.GetInt("FirstTime", firstTimeLaunch);
        if (firstTimeLaunch != 1)
        {
            // default high score values
            gameHighScore = 0.0f;

            // default audio settings
            audioEnabled = 1;
            audioVolume = maxVolume;

            // change this toggle to 1
            // this means the app has run it's initial launch
            firstTimeLaunch = 1;
            // save the value for future launches
            PlayerPrefs.SetInt("FirstTime", firstTimeLaunch);
            PlayerPrefs.Save();
        }
        else
        {
            // check player prefs for current highscore and update;
            gameHighScore = PlayerPrefs.GetFloat("HighScore");

            // default audio settings
            audioEnabled = PlayerPrefs.GetInt("AudioEnabled");
            audioVolume = PlayerPrefs.GetFloat("AudioVolume");
        }

        // sync our max game speed
        gameSpeed = maxSpeed;
    }

    void FindGameObjects()
    {
        // find all game objects that use specified tag
        go0 = GameObject.FindGameObjectsWithTag("MainMenu");
        go1 = GameObject.FindGameObjectsWithTag("GamePlay");
        go2 = GameObject.FindGameObjectsWithTag("GameOver");
        goBack = GameObject.FindGameObjectWithTag("UI_Back");
    }

    //-----------------------------------------
    //   GAME STATES
    //-----------------------------------------
    void GameState()
    {
        // game state switch
        switch (gameState)
        {
            case GameStates.MainMenu:
                AudioController(sndMainMenu);
                State("UI_Back", false);
                State("MainMenu", true);
                State("GamePlay", false);
                State("GameOver", false);
                break;

            case GameStates.GamePlay:
                AudioController(sndPlayGame);
                State("UI_Back", false);
                State("MainMenu", false);
                State("GamePlay", true);
                State("GameOver", false);
                break;

            case GameStates.GameOver:
                AudioController(sndGameOver);
                State("UI_Back", true);
                State("GamePlay", false);
                State("GameOver", true);
                break;
            
            default:
                break;
        }

        // stop updating this function
        changeState = false;
    }

    void State(string tag, bool active)
    {
        switch (tag)
        {
            case "MainMenu":
                for (int i = 0; i < go0.Length; i++)
                    go0[i].SetActive(active);
                break;

            case "GamePlay":
                for (int i = 0; i < go1.Length; i++)
                    go1[i].SetActive(active);
                break;

            case "GameOver":
                for (int i = 0; i < go2.Length; i++)
                    go2[i].SetActive(active);
                break;

            case "UI_Back":
                goBack.SetActive(active);
                break;

            default:
                break;
        }
    }


    //-----------------------------------------
    //   GAME SCORE
    //-----------------------------------------
    void GameScore()
    {
        // if changing from game over state
        if (changeState && gameState != GameStates.GameOver)
        {
            // clear the score
            gameScore = 0.0f;
        }

        // if game over...
        if (gameState == GameStates.GameOver)
        {
            // if game score is greater than high score
            if (gameScore > gameHighScore)
            {
                // save score to player prefs... these are unsecure
                // and can be hacked.  just saying...
                PlayerPrefs.SetFloat("HighScore", gameScore);
                PlayerPrefs.Save();

                // update the high score with the newly saved score
                gameHighScore = PlayerPrefs.GetFloat("HighScore");
            }
        }
    }


    //-----------------------------------------
    //   AUDIO
    //-----------------------------------------
    // volume control, max = 1.0f
    public void AudioVolume(float volume)
    {
        audioVolume = volume;
    }

    // audio toggle - on/off
    public void AudioEnabled(bool active)
    {
        if (active)
        {
            audioEnabled = 1;
        }
        else
        {
            audioEnabled = 0;
        }
    }

    // audio file controller
    void AudioController(AudioClip clipName)
    {
        // update our audio volume
        audioSrc.volume = audioVolume;

        // if audio is active...
        if (audioEnabled == 1)
        {
            // play specified clip
            audioSrc.clip = clipName;
            audioSrc.Play();
        }
    }

    //-----------------------------------------
    //   INPUT
    //-----------------------------------------
    public void ButtonControls(int state)
    {
        // map int to enum values for readability
        gameState = (GameStates)state;

        // change the game state
        changeState = true;
    }

    void KeysPressed()
    {
        // if enter key is pressed
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Jump"))
        {
            if (gameState == GameStates.MainMenu || gameState == GameStates.GameOver)
            {
                // switch to game play state
                gameState = GameStates.GamePlay;
                // update the game states
                changeState = true;
            }
        }

        // if escape key is pressed
        // ** this is also how we handle the `BACK` key for Windows Phone  :)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameState == GameStates.MainMenu)
            {
                // quit the app, and return to OS
                Application.Quit();
            }
            else if (gameState == GameStates.GamePlay)
            {
                // switch to game over state
                gameState = GameStates.GameOver;
                // update the game state
                changeState = true;
            }
            else
            {
                // switch to main menu state
                gameState = GameStates.MainMenu;
                // update the game states
                changeState = true;
            }
        }
    }
}