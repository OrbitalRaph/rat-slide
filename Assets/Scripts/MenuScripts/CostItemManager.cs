using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Cette classe gère les coûts d'un item.
/// </summary>
public class CostItemManager : MonoBehaviour, ICurrencyDisplay
{
    public Image icon;
    public TextMeshProUGUI costText;
    private int cost;
    private CurrencyType currencyType;

    /// <summary>
    /// Cette méthode permet d'initialiser le cost manager.
    /// </summary>
    public void SetCostItem(CurrencyType currencyType, int cost)
    {
        this.currencyType = currencyType;
        icon.sprite = currencyType.icon;
        this.cost = cost;
        UpdatePlayerCurrency();
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

    /// <summary>
    /// Cette méthode met à jour l'affichage de la monnaie du joueur.
    /// </summary>
    public void UpdatePlayerCurrency()
    {
        
        if (costText != null)
        costText.text = CurrencyManager.Instance.playerCurrency[currencyType.uniqueName] + "/" + cost;
        if (CurrencyManager.Instance.CanAfford(currencyType.uniqueName, cost))
        {
            costText.color = Color.green;
        }
        else
        {
            costText.color = Color.red;
        }
    }
}
