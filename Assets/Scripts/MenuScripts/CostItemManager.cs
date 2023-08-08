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
        UpdateCurrencyDisplay();
    }

    private IEnumerator WaitForCurrencyManager()
    {
        yield return null;
        CurrencyManager.Instance.onCurrencyChanged.AddListener(UpdateCurrencyDisplay);
        UpdateCurrencyDisplay();
    }

    private void OnEnable()
    {
        StartCoroutine(WaitForCurrencyManager());
    }

    private void OnDisable()
    {
        CurrencyManager.Instance.onCurrencyChanged.RemoveListener(UpdateCurrencyDisplay);
    }

    /// <summary>
    /// Cette méthode met à jour l'affichage de la monnaie du joueur.
    /// </summary>
    public void UpdateCurrencyDisplay()
    {
        if (CurrencyManager.Instance == null)
            return;

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
