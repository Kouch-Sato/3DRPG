using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyUIManager : MonoBehaviour
{
    public Slider hpSlider;

    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    public void init(EnemyManager enemyManager)
    {
        hpSlider.maxValue = enemyManager.maxHp;
        hpSlider.value = enemyManager.maxHp;
    }

    public void updateHp(int hp)
    {
        hpSlider.DOValue(hp, 0.5f);
    }
}
