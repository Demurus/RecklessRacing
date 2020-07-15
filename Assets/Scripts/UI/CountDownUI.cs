using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CountDownUI : UIController
{
    [SerializeField] private string[] _countDownTexts;
    [SerializeField] private TextMeshProUGUI _countDownText;
    private string _startText="START";
    private int _countDownIterations = 2;

    public void StartCountDown(UnityAction callback)
    {
        StartCoroutine(DoStartCountDown(callback));
    }

    private IEnumerator DoStartCountDown(UnityAction callback)
    {
        for (int i = 0; i <= _countDownIterations; i++)
        {
            _countDownText.text = _countDownTexts[i];
            yield return new WaitForSeconds(1.0f);
        }
        _countDownText.text = _startText;
        callback?.Invoke();
    }
}
