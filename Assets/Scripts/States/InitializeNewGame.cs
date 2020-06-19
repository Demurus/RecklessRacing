using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeNewGame : State
{
    protected override void OnStateStart()
    {
        base.OnStateStart();

        GameController.Instance.LoadLevel();

        //TODO: Создание уровня, загрузка префабов

        GameController.Instance.InputController.OnHold += DoContinue;
        GameController.Instance.InputController.Interactable = true;
        //GameController.Instance.MenuUI.StartForm.Show();
        // GameC.Instance.MenuUI.Tutorial.TutorialForm = TutorialStages.Start;

    }

    private void DoContinue(bool condition)
    {
        GameController.Instance.InputController.Interactable = false;
        State.Start<Level>();
    }

    protected override void OnStateEnd()
    {
        GameController.Instance.InputController.OnHold -= DoContinue;
        //GameC.Instance.MenuUI.StartForm.Hide();
    }
    protected override void OnStateRestart()
    {
        GameController.Instance.InputController.OnHold -= DoContinue;
        //GameController.Instance.MenuUI.StartForm.Hide();
    }


}
