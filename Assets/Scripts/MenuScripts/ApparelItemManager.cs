using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ApparelShop {
    public class ApparelItemManager : MonoBehaviour
    {
        // public GameObject itemModelPreview;
        public Button actionButton;
        public GameObject costPrefab;
        public Transform costsListParent;
        private ApparelItem item;

        // Method to initialize the UI with item data
        public void Initialize(ApparelItem item, bool isPurchased, bool isEquipped, bool isAffordable)
        {
            this.item = item;
            PopulateCostsList();
            // set itemModelPreviewImage to the appropriate sprite or 3D model
            UpdateButtonAppearance(isPurchased, isEquipped, isAffordable);
        }

        private void PopulateCostsList()
        {
            // Clear the costs list
            foreach (KeyValuePair<string, int> cost in item.itemCosts)
            {
                // Instantiate the UI element for the cost
                GameObject costObject = Instantiate(costPrefab, costsListParent);
                CostItemManager costManager = costObject.GetComponent<CostItemManager>();
                // costManager.SetCostItem(CurrencyManager.Instance.getCurrencyType, cost.Value);
            }
        }

        // Method to update the button appearance based on item status
        public void UpdateButtonAppearance(bool isPurchased, bool isEquipped, bool isAffordable = false)
        {
            ColorBlock buttonColors = actionButton.colors;
            if (isPurchased)
            {
                if (isEquipped)
                {
                    // Set button color to green if purchased and equipped
                    buttonColors.normalColor = Color.green;
                }
                else
                {
                    // Set button color to blue if purchased but not equipped
                    buttonColors.normalColor = Color.blue;
                }
            }
            else
            {
                if (isAffordable)
                {
                    // Set button color to yellow if not purchased but affordable
                    buttonColors.normalColor = Color.yellow;
                }
                else
                {
                    // Set button color to gray if not purchased and not affordable
                    buttonColors.normalColor = Color.gray;
                }
            }

            // Apply the button color changes
            actionButton.colors = buttonColors;
        }

        // Method called when the Purchase button is clicked
        public void OnPurchaseButtonClick()
        {
            // Call the purchase logic in the ApparelShopManager
            // Example: ApparelShopManager.Instance.PurchaseItem(item);
        }

        // Method called when the Equip button is clicked
        public void OnEquipButtonClick()
        {
            // Call the equip logic in the ApparelShopManager
            // Example: ApparelShopManager.Instance.EquipItem(item);
        }   
    }
}