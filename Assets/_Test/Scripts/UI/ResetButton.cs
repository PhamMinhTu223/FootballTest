using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : ButtonBase
{
    protected override void OnButtonClick()
    {
        SceneManager.LoadScene("Location soccer field");
    }
}
