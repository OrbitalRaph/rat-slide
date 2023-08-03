using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ApparelShop {
    public class ApparelShopManager : MonoBehaviour, IDataSaving
    {
        public ApparelItem[] ApparelItems;
        private Dictionary<string, bool> unlockedApparels;
        private string equippedApparel;
        public GameObject itemManagerPrefab;
        public Transform itemListParent;
        private void Start()
        {
            // Populate the shop with available items
            PopulateShop();
        }

        private void PopulateShop()
        {
            foreach (ApparelItem item in ApparelItems)
            {
                bool isPurchased = IsItemPurchased(item);
                bool isEquipped = IsItemEquipped(item);
                bool isAffordable = IsItemAffordable(item);

                // Instantiate the UI element for the item
                GameObject itemManagerObject = Instantiate(itemManagerPrefab, itemListParent);
                ApparelItemManager itemManager = itemManagerObject.GetComponent<ApparelItemManager>();
                itemManager.Initialize(item, isPurchased, isEquipped, isAffordable);

                // Attach button click event handlers
                Button actionButton = itemManager.actionButton;

                // If the item is purchased, add the equip functionality
                actionButton.onClick.AddListener(() => OnButtonClick(item, itemManager));
            }

            // Update the displayed currency value
            CurrencyManager.Instance.UpdateCurrencyDisplay();
        }

        public void LoadGameData(GameData gameData)
        {
            unlockedApparels = new Dictionary<string, bool>();
            foreach (ApparelItem item in ApparelItems)
            {
                unlockedApparels.Add(item.uniqueName, false);
            }
            
            // Load the purchased items dictionary from the game data
            foreach (KeyValuePair<string, bool> item in gameData.unlockedApparels)
            {
                unlockedApparels[item.Key] = item.Value;
            }

            // Load the equipped item from the game data
            equippedApparel = gameData.equippedApparel;
        }

        public void SaveGameData(ref GameData gameData)
        {
            // Save the purchased items dictionary to the game data
            gameData.unlockedApparels = unlockedApparels;
            gameData.equippedApparel = equippedApparel;
        }

        private void OnButtonClick(ApparelItem item, ApparelItemManager itemManager)
        {
            if (IsItemPurchased(item))
            {
                // If the item is purchased, equip it
                OnEquipButtonClick(item, itemManager);
            }
            else
            {
                // If the item is not purchased, try to purchase it
                OnPurchaseButtonClick(item, itemManager);
            }
        }

        private void OnPurchaseButtonClick(ApparelItem item, ApparelItemManager itemManager)
        {
            if (IsItemAffordable(item) && !IsItemPurchased(item))
            {
                MarkItemAsPurchased(item);

                // Deduct the item price from player currency
                CurrencyManager.Instance.DeductCurrency(item.itemCosts);

                // Update the UI elements
                itemManager.UpdateButtonAppearance(true, false);

                // Update the displayed currency value
                CurrencyManager.Instance.UpdateCurrencyDisplay();
            }
            else
            {
                // Play a sound or shake button
            }
        }

        private void OnEquipButtonClick(ApparelItem item, ApparelItemManager itemManager)
        {
            // Equip the item to the player character
            EquipItem(item);

            // Update the UI elements
            itemManager.UpdateButtonAppearance(true, true);
        }


        private bool IsItemAffordable(ApparelItem item)
        {
            return CurrencyManager.Instance.CanAfford(item.itemCosts);
        }

        private bool IsItemPurchased(ApparelItem item)
        {
            // Implement logic to check if the item is purchased
            return false;
        }

        private void MarkItemAsPurchased(ApparelItem item)
        {
            // Implement logic to mark the item as purchased
        }

        // private void SaveItemPurchaseStatus(ApparelItem item)
        // {
        //     // Save the item purchase status to the data file
        // }

        private bool IsItemEquipped(ApparelItem item)
        {
            // Implement logic to check if the item is equipped
            return false;
        }

        private void EquipItem(ApparelItem item)
        {
            // Implement logic to equip the item to the player character
        }

        // private void SaveItemEquipStatus(ApparelItem item)
        // {
        //     // Save the item equip status to the data file
        // }
    }
}
