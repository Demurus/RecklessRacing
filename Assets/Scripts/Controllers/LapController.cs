using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LapController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentLapTimeText;
    [SerializeField] private TextMeshProUGUI _bestLapTimeText;
    [SerializeField] private TextMeshProUGUI _currentLapText;
    [SerializeField] private TextMeshProUGUI _maxLapText;

    private FinishLine _finishLine;
    private TimeSpan _timeOfRace;
    private TimeSpan _pastLapTime;
    private TimeSpan _bestLapTime;
    private float _elapsedTime;
    private bool _timerIsGoing;
    private int _maxLapsNumber;
    private int _currentLapNumber;

    public UnityAction OnLapFinished;

    public void Init(int maxLaps)
    {
        _currentLapNumber = 0;
        _currentLapText.text = _currentLapNumber.ToString();
        _maxLapsNumber = maxLaps;
        _maxLapText.text = _maxLapsNumber.ToString();
        _currentLapTimeText.text = "Current Lap: 00:00:00";
        _bestLapTimeText.text = "Best Lap: 00:00:00";
        _timerIsGoing = false;

        _finishLine = FindObjectOfType<FinishLine>();
        if (_finishLine == null)
        {
            Debug.LogError("No Finish Line Found");
        }
        else
        {
            _finishLine.Init(this);
        }
        OnLapFinished += CalculateLap;
    }

    private void CalculateLap()
    {
        _pastLapTime = _timeOfRace;
        _elapsedTime = 0f;
        _currentLapNumber++;
        _currentLapText.text = _currentLapNumber.ToString();
        if (_currentLapNumber < _maxLapsNumber)
        {
           
           
            int timeIndex = TimeSpan.Compare(_pastLapTime, _bestLapTime);
            //Debug.Log("timeindex" + timeIndex);
            if (timeIndex < 0 && _currentLapNumber>1)
            {
                _bestLapTime = _pastLapTime;
                _bestLapTimeText.text= "Best Lap:" + _bestLapTime.ToString("mm':'ss':'ff");
            }
        }
        else
        {
            Debug.Log("RaceIsFinished");
            EndTimer();
        }
    }
    public void BeginTimer()
    {
        _timerIsGoing = true;
        _elapsedTime = 0f;
        StartCoroutine(UpdateTimer());
    }
    private IEnumerator UpdateTimer()
    {
        while (_timerIsGoing)
        {
            _elapsedTime += Time.deltaTime;
            _timeOfRace = TimeSpan.FromSeconds(_elapsedTime);
            _currentLapTimeText.text = "Current Lap:" + _timeOfRace.ToString("mm':'ss':'ff");
            yield return null;
        }
    }
    public void EndTimer()
    {
        _timerIsGoing = false;
        OnLapFinished -= CalculateLap;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
