using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoKickButton : ButtonBase
{
    protected override void OnButtonClick()
    {
        if(GameManager.Instance == null) return;
        GameManager.Instance.AutoBallKickHandle();
    }
}
