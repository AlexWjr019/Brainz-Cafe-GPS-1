using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;

public class BuyItem : MonoBehaviour
{
    [SerializeField]
    FoodSpawn FS;
    [SerializeField]
    PlayerMovement PM;

    [SerializeField]
    TMP_Text CookCost;
    [SerializeField]
    int cookPrice;
    private int upgradedCook;

    [SerializeField]
    TMP_Text TableCost;
    [SerializeField]
    int barrierPrice;
    private int upgradedTable;

    [SerializeField]
    TMP_Text RepairCost;
    [SerializeField]
    int repairPrice;

    [SerializeField]
    TMP_Text PillCost; 
    [SerializeField]
    int pillPrice;
    [HideInInspector]
    public bool boost;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UgradeTable()
    {
        if (upgradedTable == 0)
        {
            //get barrier cord
            //spawn upgraded barrier at cord
        }
    }
    
    public void SuperPill()
    {
        if (!boost)
        {
            PM.walkSpeed = PM.walkSpeed * 0.40f;
            boost = true;
        }
    }

    public void UpgradeCook()
    {
        if (upgradedCook < 5)
        {
            FS.spawnDelay -= 0.2f;
            cookPrice += 10;
            upgradedCook += 1;
        }
    }
}