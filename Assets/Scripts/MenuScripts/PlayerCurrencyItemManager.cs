using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Cette classe gère l'affichage de la monnaie du joueur d'un type spécifié.
/// </summary>
public class PlayerCurrencyItemManager : MonoBehaviour, ICurrencyDisplay
{
    
    public Image icon;
    public TMPro.TextMeshProUGUI currencyAmountText;
    private CurrencyType currencyType;

    /// <summary>
    /// Cette méthode permet d'initialiser le cost manager.
    /// </summary>
    public void Initialize(CurrencyType currencyType, int currencyAmount)
    {
        this.currencyType = currencyType;
        icon.sprite = currencyType.icon;
        currencyAmountText.text = currencyAmount.ToString();
    }

    private IEnumerator WaitForCurrencyManager()
    {

        yield return null;
        CurrencyManager.Instance.onCurrencyChanged.AddListener(UpdatePlayerCurrency);
        UpdatePlayerCurrency();
    }

    private void OnEnable()
    {
        StartCoroutine(WaitForCurrencyManager());
    }

    private void OnDisable()
    {
        CurrencyManager.Instance.onCurrencyChanged.RemoveListener(UpdatePlayerCurrency);
    }

    public void UpdatePlayerCurrency()
    {
        if (CurrencyManager.Instance == null)
            return;

        if (currencyAmountText != null)
            currencyAmountText.text = CurrencyManager.Instance.playerCurrency[currencyType.uniqueName].ToString();
    }
}
