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

    private void OnEnable()
    {
        // S'abonne à l'événement de changement de monnaie
        CurrencyManager.Instance.onCurrencyChanged.AddListener(UpdatePlayerCurrency);   

        // Attend une frame pour s'assurer que le CurrencyManager est instancié
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
