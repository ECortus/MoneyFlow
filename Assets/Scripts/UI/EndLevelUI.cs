using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelUI : ShowHideUI
{
    public void Open()
    {
        if(isShown) return;
        StartCoroutine(ShowProcess());
    }

    public void Close()
    {
        if(!isShown) return;
        StartCoroutine(HideProcess());
    }
}
