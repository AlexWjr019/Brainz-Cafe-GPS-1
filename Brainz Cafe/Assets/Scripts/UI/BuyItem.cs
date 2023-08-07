using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BuyItem : MonoBehaviour
{
    [SerializeField] FoodSpawn FS;

    [SerializeField] PlayerMovement PM;

    [HideInInspector] Counter[] tables;

    [SerializeField] Button cookButton;
    [SerializeField] TMP_Text cookText;
    [SerializeField] int cookPrice;
    int upgradedCook;

    [SerializeField] Button barrierButton;
    [SerializeField] TMP_Text barrierText;
    [SerializeField] int barrierPrice;
    [SerializeField] int healthIncrease;
    [SerializeField] public Sprite[] upgrades;
    [HideInInspector] public int upgradedTable = -1;

    [SerializeField] Button repairButton;
    [SerializeField] TMP_Text repairText;
    [SerializeField] int repairPrice;

    [SerializeField] Button pillButton;
    [SerializeField] TMP_Text pillText; 
    [SerializeField] int pillPrice;
    [SerializeField] public int boostDuration;
    [HideInInspector] public bool boost;

    [SerializeField] Button poisonButton;
    [SerializeField] TMP_Text poisonText;
    [SerializeField] int poisonPrice;
    [SerializeField] public float poisonDuration;
    [HideInInspector] public bool poisoned;

    private void Start()
    {
        tables = FindObjectsOfType<Counter>();

        cookText.text = cookPrice.ToString();
        barrierText.text = barrierPrice.ToString();
        repairText.text = repairPrice.ToString();
        pillText.text = pillPrice.ToString();
        poisonText.text = poisonPrice.ToString();
    }

    private void Update()
    {
        if (CurrencyManager.Instance.currency >= barrierPrice && upgradedTable < upgrades.Length - 1)
        {
            barrierButton.interactable = true;
        }
        else
        {
            barrierButton.interactable = false;
        }

        if (CurrencyManager.Instance.currency >= repairPrice)
        {
            repairButton.interactable = true;
        }
        else
        {
            repairButton.interactable = false;
        }

        if (CurrencyManager.Instance.currency >= pillPrice && !boost)
        {
            pillButton.interactable = true;
        }
        else
        {
            pillButton.interactable = false;
        }

        if (CurrencyManager.Instance.currency >= cookPrice && upgradedCook < 5)
        {
            cookButton.interactable = true;
        }
        else
        {
            cookButton.interactable = false;
        }

        if (CurrencyManager.Instance.currency >= poisonPrice && !poisoned)
        {
            poisonButton.interactable = true;
        }
        else
        {
            poisonButton.interactable = false;
        }
    }

    public void UpgradeTable()
    {
        if (CurrencyManager.Instance.currency >= barrierPrice)
        {
            CurrencyManager.Instance.SpendMoney(barrierPrice);

            if (upgradedTable < upgrades.Length)
            {
                Debug.Log(upgradedTable);

                for (int i = 0; i < tables.Length; i++)
                {
                    tables[i].maxHealth += healthIncrease;
                    tables[i].currentHealth = tables[i].maxHealth;
                }
                upgradedTable++;
            }
        }
    }

    public void Repair()
    {
        if (CurrencyManager.Instance.currency >= repairPrice)
        {
            CurrencyManager.Instance.SpendMoney(repairPrice);

            for (int i = 0; i < tables.Length; i++)
            {
                tables[i].Repair();
            }
        }
    }

    public void SuperPill()
    {
        if (CurrencyManager.Instance.currency >= pillPrice)
        {
            CurrencyManager.Instance.SpendMoney(pillPrice);
            if (!boost)
            {
                PM.walkSpeed *= 1.40f;
                boost = true;
            }
        }
    }

    public void UpgradeCook()
    {
        if (CurrencyManager.Instance.currency >= cookPrice)
        {
            CurrencyManager.Instance.SpendMoney(cookPrice);
            if (upgradedCook < 5)
            {
                FS.spawnDelay -= 0.2f;
                cookPrice += 10;
                upgradedCook += 1;
            }
        }
    }

    public void PoisonGas()
    {
        if (CurrencyManager.Instance.currency >= poisonPrice)
        {
            CurrencyManager.Instance.SpendMoney(poisonPrice);
            if (!poisoned)
            {
                poisoned = true;
            }
        }
    }
}