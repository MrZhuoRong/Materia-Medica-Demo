using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public Slider HPSlider;
    public Gradient gradient;

    public Image fill;
    public Text HPtext;    

    public void SetMaxHealth(int maxhealth)
    {
        HPSlider.maxValue = maxhealth;
        HPSlider.value = maxhealth;

        fill.color=gradient.Evaluate(1f);
    }
    public void SetHealth(int health, int maxhealth)
    {
        HPSlider.maxValue = maxhealth;
        HPSlider.value = health; 

        fill.color = gradient.Evaluate(HPSlider.normalizedValue);
    }

    public void SetHPtext(int health,int maxhealth)
    {
        HPtext.text=health.ToString()+" / "+maxhealth.ToString();
    }
}
