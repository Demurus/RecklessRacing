using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : State
{
    protected override void OnStateStart()
    {
        base.OnStateStart();
        // GameC.Instance.MenuUI.LooseForm.OnClose += DoClose;
        // GameC.Instance.MenuUI.LooseForm.Show();
        Debug.Log("Lose State Started");
        DoClose();
    }

    private void DoClose()
    {
        Debug.Log("Lose State Ended. Player lose");
        //GameC.Instance.MenuUI.LooseForm.OnClose -= DoClose;
        State.Start<EndOfGame>();
    }
}
