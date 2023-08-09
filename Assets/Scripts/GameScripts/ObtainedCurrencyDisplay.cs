using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cette classe gère l'affichage de tous les monnaies récupérées par le joueur.
/// </summary>
public class ObtainedCurrencyDisplay : MonoBehaviour
{
    public GameObject CurrencyItemPrefab;
    public Transform itemListParent;

    private void Start()
    {
        StartCoroutine(WaitForGameManager());
    }

    /// <summary>
    /// Coroutine qui permet d'attendre que le GameManager soit initialisé.
    /// </summary>
    private IEnumerator WaitForGameManager()
    {
        yield return null;
        PopulateTreasury();
    }

    /// <summary>
    /// Popule la liste des monnaies du joueur.
    /// </summary>
    private void PopulateTreasury()
    {
        List<CurrencyType> currencyTypes = GameManager.Instance.currencyTypes;
        SerializableDictionary<string, int> obtainedCurrency = GameManager.Instance.obtainedCurrency;
        foreach (CurrencyType currencyType in currencyTypes)
        {
            if (obtainedCurrency.ContainsKey(currencyType.uniqueName))
            {
                GameObject currencyItem = Instantiate(CurrencyItemPrefab, itemListParent);
                ObtainedCurrencyItemManager currencyItemManager = currencyItem.GetComponent<ObtainedCurrencyItemManager>();
                currencyItemManager.Initialize(currencyType, obtainedCurrency[currencyType.uniqueName]);
            }
        }
    }
}
