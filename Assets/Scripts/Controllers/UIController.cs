using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
   [SerializeField] private CountDownUI _countDownUI;

   public UnityAction OnCountDownFinished;

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

    IEnumerator HideMenuTimer(float timer, UIController target)
    {
        yield return new WaitForSeconds(timer);
        target.Hide();
        OnCountDownFinished -= FinalizeRacePrep;
       
    }
}
