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
        icon.sprite = currencyType.icon;
        currencyAmountText.text = currencyAmount.ToString();
    }

    private void OnEnable()
    {
        CurrencyManager.Instance.onCurrencyChanged.AddListener(UpdatePlayerCurrency);   

        // Problème Rencontrer : Lorsque L'objet viens d'être instancier, il n'est pas possible d'appeler la fonction UpdatePlayerCurrency
        StartCoroutine(WaitForCurrencyManager());
    }

    private IEnumerator WaitForCurrencyManager()
    {
        yield return null;
        UpdatePlayerCurrency();
    }

    private void OnDisable()
    {
        CurrencyManager.Instance.onCurrencyChanged.RemoveListener(UpdatePlayerCurrency);
    }

    public void UpdatePlayerCurrency()
    {
        if (currencyAmountText != null)
            currencyAmountText.text = CurrencyManager.Instance.playerCurrency[currencyType.uniqueName].ToString();
    }
}
