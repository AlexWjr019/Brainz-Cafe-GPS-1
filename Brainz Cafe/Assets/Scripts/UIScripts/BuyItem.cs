using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyItem : MonoBehaviour
{
    [SerializeField] FoodSpawn FS;

    [SerializeField] PlayerMovement PM;

    [HideInInspector] Counter[] upgradable;

    [SerializeField] Button cookButton;
    [SerializeField] TMP_Text cookText;
    [SerializeField] int cookPrice;
    int upgradedCook;

    [SerializeField] Button barrierButton;
    [SerializeField] TMP_Text barrierText;
    [SerializeField] int barrierPrice;
    [SerializeField] int healthIncrease;
    [SerializeField] Sprite[] upgrades;
    [HideInInspector] public int upgradedTable = -1;

    [SerializeField] Button repairButton;
    [SerializeField] TMP_Text repairText;
    [SerializeField] int repairPrice;

    [SerializeField] Button pillButton;
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

    private void Update()
    {
        if (CurrencyManager.Instance.currency >= barrierPrice && upgradedTable < upgrades.Length)
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
        if (CurrencyManager.Instance.currency >= pillPrice)
        {
            pillButton.interactable = true;
        }
        else
        {
            pillButton.interactable = false;
        }
        if (CurrencyManager.Instance.currency >= cookPrice)
        {
            cookButton.interactable = true;
        }
        else
        {
            cookButton.interactable = false;
        }
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