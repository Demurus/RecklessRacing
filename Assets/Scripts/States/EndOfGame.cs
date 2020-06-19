using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfGame : State
{
    protected override void OnStateStart()
    {
        base.OnStateStart();
        // GameC.Instance.SaveLoadSystem.Data.Level++;
        // GameC.Instance.SaveLoadSystem.Save();
      //  GameC.Instance.MenuUI.StageProgress.Hide();
        GameController.Instance.UnloadLevel();
        //TODO: удаление уровня и других созданных объектов
        GameController.Instance.DelayInvoke(0.2f, () => { State.Start<InitializeNewGame>(); });
    }
}
