using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DogUIManager : MonoBehaviour
{
    public Slider hpSlider;

    public void init(DogManager dogManager)
    {
        hpSlider.maxValue = dogManager.maxHp;
        hpSlider.value = dogManager.maxHp;
    }

    public void UpdateHP(int hp)
    {
        hpSlider.DOValue(hp, 0.5f);
    }
}
