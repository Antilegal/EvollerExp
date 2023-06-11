using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    [SerializeField] private Slider timescaleSlider;

    private void Start()
    {
        timescaleSlider.onValueChanged.AddListener(OnTimescaleChange);
        OnTimescaleChange(timescaleSlider.value);
    }

    private void OnTimescaleChange(float newValue)
    {
        Time.timeScale = newValue;
    }
}
