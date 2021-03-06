using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
   // private static UIController _instance;
    //public static UIController Instance => _instance;


    [SerializeField] private CountDownUI _countDownUI;

    public LapController LapController;
    public UnityAction OnCountDownFinished;
   // private void Awake()
    //{
    //    _instance = this;
    //}
    public void StartRacePrep()
    {
        OnCountDownFinished += FinalizeRacePrep;
        _countDownUI.Show();
        _countDownUI.StartCountDown(OnCountDownFinished);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void FinalizeRacePrep()
    {
        Debug.Log("FinalizeRacePrep");
        GameController.Instance.OnRaceIsReady?.Invoke();
        StartCoroutine(HideMenuTimer(1.0f, _countDownUI));
    }

    IEnumerator HideMenuTimer(float timer, CountDownUI target)
    {
        yield return new WaitForSeconds(timer);
        target.Hide();
        OnCountDownFinished -= FinalizeRacePrep;
       
    }
}
