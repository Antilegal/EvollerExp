using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : UIValue
{
    public string caption = "";

    private Text text;

    protected override void SetValue()
    {
        base.SetValue();

        if (!text) text = GetComponent<Text>();

        if(text)
        {
            text.text = caption + _value.ToString();
        }
    }
}
