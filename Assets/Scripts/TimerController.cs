using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TimerController : MonoBehaviour {

    public TimeSpan LevelTimer { get; set; }
    private Text _timerText;


	// Use this for initialization
	void Start () {
        LevelTimer = TimeSpan.FromMinutes(6);
        _timerText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        LevelTimer = LevelTimer.Subtract(TimeSpan.FromSeconds(Time.deltaTime));
        _timerText.text = LevelTimer.ToString();
	}
}
