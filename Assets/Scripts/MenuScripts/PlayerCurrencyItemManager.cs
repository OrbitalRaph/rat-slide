using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCurrencyItemManager : MonoBehaviour, ICurrencyDisplay
{
    
    public Image icon;
    public TMPro.TextMeshProUGUI currencyAmountText;

    private CurrencyType currencyType;

    public void Initialize(CurrencyType currencyType, int currencyAmount)
    {
        this.currencyType = currencyType;
        currencyAmountText.text = currencyAmount.ToString();
    }

    public void UpdatePlayerCurrency()
    {
        currencyAmountText.text = CurrencyManager.Instance.playerCurrency[currencyType.uniqueName].ToString();
    }
}
