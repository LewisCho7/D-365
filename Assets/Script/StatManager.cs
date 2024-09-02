using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatManager : MonoBehaviour
{
    public Slider affectionSlider;
    public Slider trustSlider;

    public TextMeshProUGUI affectionLevelText;
    public TextMeshProUGUI trustLevelText;

    private int affection = 0;
    private int trust = 0;

    void Start()
    {
        affectionSlider.maxValue = 100;
        trustSlider.maxValue = 100;
        UpdateSliders();
        UpdateAffectionLevel();
        UpdateTrustLevel();
    }

    public void ModifyStats(int affectionAdd, int trustAdd)
    {
        affection += affectionAdd;
        trust += trustAdd;
        UpdateSliders();
        UpdateAffectionLevel();
    }
    void UpdateSliders()
    {
        affectionSlider.value = affection;
        trustSlider.value = trust;
    }
    void UpdateAffectionLevel()
    {
        int level = CalculateLevels(affection);
        affectionLevelText.text = "호감도 Lv " + level;
    }

    void UpdateTrustLevel()
    {
        int level = CalculateLevels(trust);
        trustLevelText.text = "신뢰도 Lv " + level;
    }

    int CalculateLevels(int stat)
    {
        if (stat < 10)
            return 1;
        else if(stat < 30)
            return 2;
        else if(stat < 60)
            return 3;
        else if(stat < 100)
            return 4;
        else
            return 5;
    }
}
