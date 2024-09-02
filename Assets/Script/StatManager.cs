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

    private int affection = 10;
    private int trust = 50;

    private int trustLevel;
    private int affectionLevel;

    void Start()
    {
        affectionSlider.maxValue = 100;
        trustSlider.maxValue = 100;
        UpdateSliders();
        UpdateAffectionLevel();
        UpdateTrustLevel();
    }

    public void ModifyAffectionStats(int affectionAdd)
    {
        affection += affectionAdd;
        UpdateSliders();
        UpdateAffectionLevel();
    }

    public void ModifyTrustStats(int trustAdd)
    {
        trust += trustAdd;
        UpdateSliders();
        UpdateTrustLevel();
    }
    void UpdateSliders()
    {
        affectionSlider.value = affection;
        trustSlider.value = trust;
    }
    void UpdateAffectionLevel()
    {
        affectionLevel = CalculateLevels(affection);
        affectionLevelText.text = "호감도 Lv " + affectionLevel;
    }

    void UpdateTrustLevel()
    {
        trustLevel = CalculateLevels(trust);
        trustLevelText.text = "신뢰도 Lv " + trustLevel;
    }

    public int getTrustLevel()
    {
        return trustLevel;
    }

    public int getAffectionLevel()
    {
        return affectionLevel;
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
