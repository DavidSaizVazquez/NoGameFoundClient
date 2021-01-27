using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button_click : MonoBehaviour
{
    private Button _button;
    // Start is called before the first frame update
    void Start()
    {
        
        
        _button = GameObject.FindObjectOfType<Button>();
        _button.onClick.AddListener(buttonClick);
    }

    private void buttonClick()
    {
        Screen.SetResolution(960,640, !Screen.fullScreen);
        
        
    }
}
