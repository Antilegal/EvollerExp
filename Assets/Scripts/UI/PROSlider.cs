using UnityEngine;

using UnityEngine.UI;

using TMPro;

public class PROSlider : MonoBehaviour
{
    private Slider _slider;
    private TMP_Text _text;

    private void Start()
    {
        _slider = GetComponent<Slider>();

        _text = GetComponentInChildren<TMP_Text>();

        _slider.onValueChanged.AddListener(OnChangeValue);

        OnChangeValue(_slider.value);
    }

    private string PrepareValue(float value)
    {
        return value.ToString("F2");
    }

    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(OnChangeValue);
    }

    private void OnChangeValue(float newValue)
    {
        _text.text = PrepareValue(newValue);
    }


}
