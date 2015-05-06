//-----------------------------------------
//   GameControllerSimple.cs
//
//   Jason Walters
//   http://glitchbeam.com
//   @jasonrwalters
//
//   Modified from GameController.cs by
//   Dmitry Soshnikov
//   @shwars
//-----------------------------------------

using UnityEngine;
using System.Collections;

public class GameControllerSimple : MonoBehaviour 
{
	public static float gameScore;
	public static float gameHighScore;
	public static float audioVolume;
	public static int audioEnabled;
	public static float gameSpeed;
	
	public AudioClip sndPlayGame;
	public float maxVolume = 0.75f;
	public float maxSpeed = 0.0f;
	
	private AudioSource audioSrc;
	private int firstTimeLaunch;
	
	// Use this for initialization
	void Start ()
	{
		// cache components
		audioSrc = GetComponent<AudioSource>();
		
		// setup game settings
		SettingsDefault();
	}
	
	
	
	// Update is called once per frame
	void Update () 
	{
		
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
	
	
	
	
	//-----------------------------------------
	//   GAME SCORE
	//-----------------------------------------
	public static void UpdateScore()
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
			gameScore = 0;
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
	
}