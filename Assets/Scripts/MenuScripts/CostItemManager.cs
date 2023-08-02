using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CostItemManager : MonoBehaviour, ICurrencyDisplay
{
    public Image icon;
    public TextMeshProUGUI costText;
    private int cost;
    private CurrencyType currencyType;

    public void SetCostItem(CurrencyType currencyType, int cost)
    {
        this.currencyType = currencyType;
        icon.sprite = currencyType.icon;
        this.cost = cost;
        UpdatePlayerCurrency();
    }

    public void UpdatePlayerCurrency()
    {
        costText.text = CurrencyManager.Instance.playerCurrency[currencyType.uniqueName] + "/" + cost;
        if (CurrencyManager.Instance.canAfford(currencyType.uniqueName, cost))
        {
            costText.color = Color.green;
        }
        else
        {
            costText.color = Color.red;
        }
    }
}
