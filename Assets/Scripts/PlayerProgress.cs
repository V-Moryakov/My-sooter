using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    public List<PlayerProgressLevel> levels;

    public GameObject gamePlayUI;
    public GameObject winGameScreen;
    public int counterCollectebleItem = 0;
    public TextMeshProUGUI itemValueTMP;

    private int _levelValue = 1;
    private float _experienceCurrentValue = 0;
    private float _experienceTargetValue = 100;

    public RectTransform experienceValueRectTransform;
    public TextMeshProUGUI levelValueTMP;


    void Start()
    {
        SetLevel(_levelValue);
        DrawUI();
        DrawItem();
    }

    public void AddExperience(float value)
    {
        _experienceCurrentValue += value;
        if(_experienceCurrentValue >= _experienceTargetValue)
        {
            SetLevel(_levelValue + 1);
            _experienceCurrentValue = 0;
        }
        DrawUI();
    }

    private void SetLevel(int value)
    {
        _levelValue = value;

        var currentLevel = levels[_levelValue - 1];
        _experienceTargetValue = currentLevel.experienceForTheNextLevel;
        GetComponent<FireballCaster>().damage = currentLevel.fireballDamage;

        var grenadeCaster = GetComponent<GrenadeCaster>();
        grenadeCaster.damage = currentLevel.grenadeDamage;

        if (currentLevel.grenadeDamage < 0)
            grenadeCaster.enabled = false;
        else
            grenadeCaster.enabled = true;
    }

    private void DrawUI()
    {
        experienceValueRectTransform.anchorMax = new Vector2(_experienceCurrentValue / _experienceTargetValue, 1);
        levelValueTMP.text = _levelValue.ToString();
    }

    public void AddCount(int item)
    {
        counterCollectebleItem += item;
        DrawItem();
        if (counterCollectebleItem < 20) return;

        gamePlayUI.SetActive(false);
        winGameScreen.SetActive(true);
        GetComponent<PlayerController>().enabled = false;
        GetComponent<FireballCaster>().enabled = false;
        GetComponent<CameraRotation>().enabled = false;
        GetComponent<EnemyAI>().enabled = false;


    }

    private void DrawItem()
    {
        itemValueTMP.text = counterCollectebleItem.ToString();
    }
}
