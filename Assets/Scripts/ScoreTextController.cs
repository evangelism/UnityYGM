//-----------------------------------------
//   ScoreTextController.cs
//
//   Jason Walters
//   http://glitchbeam.com
//   @jasonrwalters
//
//   last edited on 1/23/2015
//-----------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreTextController : MonoBehaviour 
{

    void OnDisable ()
    {
        // clear the text
        GetComponent<Text>().text = string.Empty;
    }

    void OnEnable ()
    {
        if (gameObject.name == "MainMenu_Text_HighScore")
        {
            float highScore = GameController.gameHighScore;
            GetComponent<Text>().text = "Highscore " + highScore;
        }

        if (gameObject.name == "GameOver_Text_Score_Time")
        {
            float score = GameController.gameScore;
            GetComponent<Text>().text = "You scored " + score + " points!";
        }
    }

    void Start ()
    {
        if (gameObject.name == "MainMenu_Text_HighScore")
        {
            float highScore = GameController.gameHighScore;
            GetComponent<Text>().text = "Highscore " + highScore;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        // update the score based on following object names
        if (gameObject.name == "GamePlay_Text_Score_Time")
        {
            float score = GameController.gameScore;
            GetComponent<Text>().text = "Score " + score;
        }
	}
}
