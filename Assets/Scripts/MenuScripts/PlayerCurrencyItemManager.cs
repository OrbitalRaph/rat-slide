using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gère l'affichage de la monnaie du joueur d'un type spécifié.
/// </summary>
public class PlayerCurrencyItemManager : MonoBehaviour, ICurrencyDisplay
{   
    public Image icon;
    public TMPro.TextMeshProUGUI currencyAmountText;
    private CurrencyType currencyType;

    /// <summary>
    /// Initialise le cost manager.
    /// </summary>
    public void Initialize(CurrencyType currencyType, int currencyAmount)
    {
        this.currencyType = currencyType;
        icon.sprite = currencyType.icon;
        currencyAmountText.text = currencyAmount.ToString();
    }

    /// <summary>
    /// coroutine qui permet d'attendre que le CurrencyManager soit initialisé.
    /// </summary>
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

    public void UpdateCurrencyDisplay()
    {
        if (currencyAmountText != null)
            currencyAmountText.text = CurrencyManager.Instance.playerCurrency[currencyType.uniqueName].ToString();
    }
}
