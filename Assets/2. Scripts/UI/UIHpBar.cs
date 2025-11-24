using UnityEngine;
using UnityEngine.UI;

public class UIHpBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public void Init()
    {
        UpdateHp(1.0f);
    }

    public void UpdateHp(float percentage)
    {
        _slider.value = percentage;
    }
}
