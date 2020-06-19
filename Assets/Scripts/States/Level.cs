using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : State
{
    int stage;
    protected override void OnStateStart()
    {
        base.OnStateStart();
        //progress = 0;
        //stageProgress = GameC.Instance.MenuUI.StageProgress;
        GameController.Instance.OnStageIsOver += DoEndStage;
        GameController.Instance.InputController.Interactable = true;
        stage = 1;
        Debug.Log("Level Started");
    }

    private void DoEndStage()
    {
        GameController.Instance.InputController.Interactable = false;
        DoContinue();
    }

    private void DoContinue()
    {
        GameController.Instance.InputController.Interactable = true;
        GameController.Instance.OnStageIsOver -= DoEndStage;
        Debug.Log("Continue After Stage");
        OnStateEnd();
    }

    protected override void OnStateEnd()
    {
        Debug.Log("State Ended");
        GameController.Instance.InputController.Interactable = false;
        //GameController.Instance.InputController.OnHold -= DoHold;

        //State.Start<EndOfGame>();
    }
}
