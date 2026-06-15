using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonBase : MonoBehaviour
{
    private Button button;
    void Awake()
    {
        button = GetComponent<Button>();
    }
    void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    protected abstract void OnButtonClick();
}
