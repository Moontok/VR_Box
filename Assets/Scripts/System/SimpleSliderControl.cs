using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using static UnityEngine.Rendering.GPUSort;

[RequireComponent(typeof(Slider))]
public class SimpleSliderControl : MonoBehaviour
{
    public UnityEvent OnSliderActive;

    private Slider slider = null;

    [SerializeField] private float minValue = 0f;
    [SerializeField] private float maxValue = 20.0f;
    [SerializeField] private Light buildingLight = null;
    [SerializeField] private Material bulb = null;

    private void OnEnable()
    {
        slider = GetComponent<Slider>();
        slider.minValue = minValue;
        slider.maxValue = maxValue;
        slider.value = minValue;
        slider.onValueChanged.AddListener(OnValueChanged);
        buildingLight.intensity = 0;
    }

    private void OnValueChanged(float arg0)
    {
        if (arg0 >= maxValue)
        {
            OnSliderActive?.Invoke();
        }

        buildingLight.intensity = arg0;

        if (arg0 >= 0.01f)
            bulb.EnableKeyword("_EMISSION");
        else
            bulb.DisableKeyword("_EMISSION");
    }
}
