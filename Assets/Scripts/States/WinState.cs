using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : State
{
    protected override void OnStateStart()
    {
        base.OnStateStart();
        // GameC.Instance.Level++;
        Debug.Log("Win State Started");
        DoClose();
        //GameController.Instance.MenuUI.WinForm.OnClose += DoClose;
       // GameController.Instance.MenuUI.WinForm.Show();
    }

    private void DoClose()
    {
        Debug.Log("Win State Ended. Player Won");
        State.Start<EndOfGame>();
    }
}
