using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyItem : MonoBehaviour
{
    [SerializeField] FoodSpawn FS;

    [SerializeField] PlayerMovement PM;

    [SerializeField] TMP_Text cookText;
    [SerializeField] int cookPrice;
    int upgradedCook;

    [SerializeField] Counter[] upgradable;
 
    [SerializeField] TMP_Text barrierText;
    [SerializeField] int barrierPrice;
    [SerializeField] int healthIncrease;
    [SerializeField] Sprite[] upgrades;
    [HideInInspector] public int upgradedTable = -1;

    [SerializeField] TMP_Text repairText;
    [SerializeField] int repairPrice;

    [SerializeField] TMP_Text pillText; 
    [SerializeField] int pillPrice;
    [HideInInspector] public bool boost;

    private void Start()
    {
        upgradable = FindObjectsOfType<Counter>();

        cookText.text = cookPrice.ToString();
        barrierText.text = barrierPrice.ToString();
        repairText.text = repairPrice.ToString();
        pillText.text = pillPrice.ToString();
    }

    public void UpgradeTable()
    {
        if (CurrencyManager.Instance.currency >= barrierPrice)
        {
            CurrencyManager.Instance.SpendMoney(barrierPrice);

            if (upgradedTable < upgrades.Length)
            {
                for (int i = 0; i < upgradable.Length; i++)
                {
                    upgradable[i].sr.sprite = upgrades[upgradedTable];

                    upgradable[i].maxHealth += healthIncrease;
                    upgradable[i].currentHealth = upgradable[i].maxHealth;
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

            for (int i = 0; i < upgradable.Length; i++)
            {
                upgradable[i].Repair();
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
}