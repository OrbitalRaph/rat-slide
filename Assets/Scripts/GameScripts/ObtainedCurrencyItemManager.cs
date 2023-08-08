using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Cette classe gère l'affichage de la monnaie du joueur d'un type spécifié.
/// </summary>
public class ObtainedCurrencyItemManager : MonoBehaviour, ICurrencyDisplay
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

    private IEnumerator WaitForGameManager()
    {

        yield return null;
        GameManager.Instance.onCurrencyChanged.AddListener(UpdateCurrencyDisplay);
        UpdateCurrencyDisplay();
    }

    private void OnEnable()
    {
        StartCoroutine(WaitForGameManager());
    }

    private void OnDisable()
    {
        GameManager.Instance.onCurrencyChanged.RemoveListener(UpdateCurrencyDisplay);
    }

    public void UpdateCurrencyDisplay()
    {
        if (currencyAmountText != null)
            currencyAmountText.text = GameManager.Instance.obtainedCurrency[currencyType.uniqueName].ToString();
    }
}