using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickButton : ButtonBase
{
    protected override void OnButtonClick()
    {
        if(GameManager.Instance == null) return;
        GameManager.Instance.BallKickHandle();
    }
}
