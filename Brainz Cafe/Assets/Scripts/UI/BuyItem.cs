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
    [SerializeField] int cookPriceIncease;
    [SerializeField] float cookTimeDecrease;
    int upgradedCook;

    [SerializeField] Button barrierButton;
    [SerializeField] TMP_Text barrierText;
    [SerializeField] int barrierPrice;
    [SerializeField] int healthIncrease;
    [SerializeField] float minHealthPercentage;
    [SerializeField] public Sprite[] upgrades;
    [HideInInspector] public int upgradedTable = -1;

    [SerializeField] Button repairButton;
    [SerializeField] TMP_Text repairText;
    [SerializeField] int repairPrice;
    [SerializeField] int repairPriceIncease;

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

        if (CurrencyManager.Instance.currency >= repairPrice && tables[0].currentHealth < tables[0].maxHealth * minHealthPercentage)
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
            if (upgradedTable < upgrades.Length)
            {
                Debug.Log(upgradedTable);
                
                CurrencyManager.Instance.SpendMoney(barrierPrice);
                AudioManager.Instance.Play("BarrierUpAndRep");

                for (int i = 0; i < tables.Length; i++)
                {
                    tables[i].maxHealth += healthIncrease;
                    tables[i].currentHealth = tables[i].maxHealth;
                    tables[i].isPlayed = false;
                    tables[i].isPlayed2 = false;
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
            AudioManager.Instance.Play("BarrierUpAndRep");

            for (int i = 0; i < tables.Length; i++)
            {
                tables[i].Repair();
                tables[i].isPlayed = false;
                tables[i].isPlayed2 = false;
            }

            repairPrice += repairPriceIncease;
            repairText.text = repairPrice.ToString();
        }
    }

    public void SuperPill()
    {
        if (CurrencyManager.Instance.currency >= pillPrice)
        {
            CurrencyManager.Instance.SpendMoney(pillPrice);
            AudioManager.Instance.Play("SuperP");

            if (!boost)
            {
                PM.walkSpeed *= 1.40f;
                PM.tempSpeed = PM.walkSpeed;
                boost = true;
            }
        }
    }

    public void UpgradeCook()
    {
        if (CurrencyManager.Instance.currency >= cookPrice)
        {
            CurrencyManager.Instance.SpendMoney(cookPrice);
            AudioManager.Instance.Play("ChefUp");

            if (upgradedCook < 5)
            {
                FS.spawnDelay -= cookTimeDecrease;
                upgradedCook += 1;
                cookPrice += cookPriceIncease;
                cookText.text = cookPrice.ToString();
            }
        }
    }

    public void PoisonGas()
    {
        if (CurrencyManager.Instance.currency >= poisonPrice)
        {
            CurrencyManager.Instance.SpendMoney(poisonPrice);
            AudioManager.Instance.Play("PoisonG");

            if (!poisoned)
            {
                poisoned = true;
            }
        }
    }
}