using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{

    [SerializeField]private Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void UpdateHealthBar(float currValue, float maxValue)
    {
        slider.value = currValue / maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
