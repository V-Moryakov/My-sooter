using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float value = 100;
    public RectTransform valueRectTransform;

    public GameObject gamePlayUI;
    public GameObject gameOverScreen;

    public Animator animator;

    private float _maxValue;

    private void Start()
    {
        _maxValue = value;
        DrawHealthBar();
    }

    public bool IsAlive()
    {
        return value > 0;
    }
    public void DealDamage(float damage)
    {
        if()
        value -= damage;
        if (value <= 0)
        {
            PlayerIsDead();
        }

        DrawHealthBar();
    }

    private void DrawHealthBar()
    {
        valueRectTransform.anchorMax = new Vector2(value/_maxValue, 1);
    }

    private void PlayerIsDead()
    {
        gamePlayUI.SetActive(false);
        gameOverScreen.SetActive(true);
        gameOverScreen.GetComponent<Animator>().SetTrigger("show");

        GetComponent<PlayerController>().enabled = false;
        GetComponent<FireballCaster>().enabled = false;
        GetComponent<CameraRotation>().enabled = false;
        animator.SetTrigger("death");
    }

    public void AddHealth(float amount)
    {
        value += amount;
        value = Mathf.Clamp(value, 0, _maxValue);
        DrawHealthBar();
    }
}
