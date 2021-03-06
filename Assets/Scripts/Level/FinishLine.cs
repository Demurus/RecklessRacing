using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private LapController _lapController; 
    public void Init(LapController link)
    {
        _lapController = link;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.name + "passed");
            _lapController.OnLapFinished.Invoke();
        }
    }
}
