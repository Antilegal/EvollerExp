using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UIGraph : UIValue
{
    public Image image;

    public Gradient colorGradient = new Gradient();

    private float maxvalue = 200f;
    private RectTransform rectTransform;

    protected override void SetValue()
    {
        base.SetValue();

        if (!rectTransform) rectTransform = GetComponent<RectTransform>();

        GameObject obj = Instantiate(image.gameObject, transform);

        obj.SetActive(true);

        Image newImage = obj.GetComponent<Image>();

        //Rect rect = newImage.rectTransform.rect;
        //rect.height = _value;
        Vector2 sizeDelta = newImage.rectTransform.sizeDelta;

        float ratio = Mathf.Clamp01(_value / maxvalue);

        newImage.CrossFadeColor(colorGradient.Evaluate(ratio), 1f, true, false);

        sizeDelta.y = ratio * rectTransform.rect.height;
        newImage.rectTransform.sizeDelta = sizeDelta;
    }
}
