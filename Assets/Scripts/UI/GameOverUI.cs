using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : UI
{
    private Canvas _canvas;

    public override void Initialize()
    {
        _canvas = gameObject.GetComponent<Canvas>();
    }

    public void Show()
    {
        if (!_canvas.isActiveAndEnabled)
        {
            _canvas.enabled = true;
        }
    }
    
    public void Hide()
    {
        if (_canvas.isActiveAndEnabled)
        {
            _canvas.enabled = false;
        }
    }
    
}
