using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using System;

public class Unit : MonoBehaviour
{
    public GameManager.UnitType unitType;

    public BaseController baseController { get; set; }

    public int attack = 1;

    public float maxHealth = 100;
    public float armor = 0;
    public float attackSpeed = 1;
    public float speed = 1;
    public float attackRange = 1f;
    public int gold = 1;

    public HealthBar healthBar;
    public GameObject topSelector, downSelector;
    public Action onDead;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public void SetHealth(float heal)
    {
        if (currentHealth + heal <= 100)
        {
            currentHealth += heal;
            healthBar.UpdateHealth(currentHealth);
        }
    }

    public void OnDamage(float damage)
    {
        SetHealth(-(damage * (1 - armor)));
        if (currentHealth <= 0)
        {
            if (onDead != null)
            {
                onDead();
            }
        }
    }

    public void ResetUnit()
    {
        topSelector.SetActive(false);
        downSelector.SetActive(false);
        currentHealth = maxHealth;
        healthBar.UpdateHealth(maxHealth);
    }
}
