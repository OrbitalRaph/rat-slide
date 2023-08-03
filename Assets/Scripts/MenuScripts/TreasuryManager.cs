using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasuryManager : MonoBehaviour
{
    public GameObject CurrencyItemPrefab;
    public Transform itemListParent;

    // Start is called before the first frame update
    private void Start()
    {
        PopulateTreasury();
    }

    private void PopulateTreasury()
    {
        List<CurrencyType> currencyTypes = CurrencyManager.Instance.currencyTypes;
        Dictionary<string, int> playerCurrency = CurrencyManager.Instance.playerCurrency;
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
