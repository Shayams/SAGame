using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UniRx;

public class TimerController : MonoBehaviour {

    private TimeSpan LevelTimer { get; set; }
    private Text _timerText;
    public int LevelTime = 6;


	void Start () {
        LevelTimer = TimeSpan.FromMinutes(LevelTime);
        _timerText = GetComponent<Text>();

        GetLevelTimerObservable()
            .Select(newTimer => newTimer.ToString())
            .Do(newTimer => _timerText.text = newTimer)
            .Subscribe();
    }

    public IObservable<TimeSpan> GetLevelTimerObservable()
    {
        return Observable.EveryUpdate()
            .Select(_ => LevelTimer.Subtract(TimeSpan.FromSeconds(Time.deltaTime)))
            .Do(newTimer => LevelTimer = newTimer)
            .Share();
    }
}
