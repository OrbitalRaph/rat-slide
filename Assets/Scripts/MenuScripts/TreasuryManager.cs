using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gère l'affichage de tous les monnaies du joueur.
/// </summary>
public class TreasuryManager : MonoBehaviour
{
    public GameObject CurrencyItemPrefab;
    public Transform itemListParent;

    private void Start()
    {
        StartCoroutine(WaitForCurrencyManager());
    }

    /// <summary>
    /// coroutine qui permet d'attendre que le CurrencyManager soit initialisé.
    /// </summary>
    private IEnumerator WaitForCurrencyManager()
    {
        yield return null;
        PopulateTreasury();
    }

    /// <summary>
    /// Popule la liste des monnaies du joueur.
    /// </summary>
    private void PopulateTreasury()
    {
        List<CurrencyType> currencyTypes = CurrencyManager.Instance.currencyTypes;
        SerializableDictionary<string, int> playerCurrency = CurrencyManager.Instance.playerCurrency;

        // Pour chaque type de monnaie, on crée un item dans la liste des monnaies du joueur.
        foreach (CurrencyType currencyType in currencyTypes)
        {
            if (playerCurrency.ContainsKey(currencyType.uniqueName))
            {
                GameObject currencyItem = Instantiate(CurrencyItemPrefab, itemListParent);
                PlayerCurrencyItemManager currencyItemManager = currencyItem.GetComponent<PlayerCurrencyItemManager>();
                currencyItemManager.Initialize(currencyType, playerCurrency[currencyType.uniqueName]);
            }
        }

        CurrencyManager.Instance.UpdateCurrencyDisplay();
    }
}
