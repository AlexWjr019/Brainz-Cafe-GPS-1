using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;
    
    public TMP_Text currencyText;

    [HideInInspector]
    public int currency;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currency = 0;
        currencyText.text = "Brainz: " + currency.ToString();
    }

    public void AddMoney(int p)
    {
        currency += p;
        currencyText.text = "Brainz: " + currency.ToString();
    }

    public void SpendMoney(int p)
    {
        currency -= p;
        currencyText.text = "Brainz: " + currency.ToString();
    }
}
