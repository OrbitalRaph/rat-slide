using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ApparelShop
{
    public class ApparelItemManager : MonoBehaviour
    {
        // public GameObject itemModelPreview;
        public Button actionButton;
        [SerializeField] private TextMeshProUGUI actionButtonText;
        [SerializeField] private Image backdropImage;
        [SerializeField] private RawImage itemImage;
        public GameObject costPrefab;
        public Sprite greyButtonSprite;
        public Sprite yellowButtonSprite;
        public Sprite greenButtonSprite;
        public Sprite blueButtonSprite;
        public Sprite greyBackdropSprite;
        public Sprite blueBackdropSprite;
        public Sprite greenBackdropSprite;
        public Transform costsListParent;
        private ApparelItem item;
        private List<GameObject> costsObjects = new List<GameObject>();

        // Method to initialize the UI with item data
        public void Initialize(ApparelItem item, bool isPurchased, bool isEquipped)
        {
            this.item = item;
            itemImage.texture = item.itemTexture;

            if (!isPurchased)
                PopulateCostsList();

            UpdateItemAppearance(isPurchased, isEquipped);
        }

        private void PopulateCostsList()
        {
            foreach (KeyValuePair<string, int> cost in item.itemCosts)
            {
                // Instantiate the UI element for the cost
                GameObject costObject = Instantiate(costPrefab, costsListParent);
                CostItemManager costManager = costObject.GetComponent<CostItemManager>();
                CurrencyType currencyType = CurrencyManager.Instance.GetCurrencyType(cost.Key);
                costManager.SetCostItem(currencyType, cost.Value);
                costsObjects.Add(costObject);
            }
        }

        // Method to update the button appearance based on item status
        public void UpdateItemAppearance(bool isPurchased, bool isEquipped)
        {
            actionButtonText.rectTransform.offsetMin = new Vector2(0, 5);
            actionButtonText.rectTransform.offsetMax = new Vector2(0, 0);

            if (isPurchased)
            {
                backdropImage.color = Color.white;
                if (isEquipped)
                {
                    
                    actionButtonText.rectTransform.offsetMin = new Vector2(0, 0);
                    actionButtonText.rectTransform.offsetMax = new Vector2(0, -8);
                    // Set button color to green if purchased and equipped
                    actionButton.image.sprite = greenButtonSprite;
                    backdropImage.sprite = greenBackdropSprite;
                    actionButtonText.text = "Enlever";
                }
                else
                {
                    // Set button color to blue if purchased but not equipped
                    actionButton.image.sprite = blueButtonSprite;
                    backdropImage.sprite = blueBackdropSprite;
                    actionButtonText.text = "Enfiler";
                }
            }
            else
            {
                backdropImage.sprite = greyBackdropSprite;
                backdropImage.color = new Color(0.3294118f, 0.3607843f, 0.4078431f);
                if (CurrencyManager.Instance.CanAfford(item.itemCosts))
                {
                    // Set button color to yellow if not purchased but affordable
                    actionButton.image.sprite = yellowButtonSprite;
                    actionButtonText.text = "Acheter";
                }
                else
                {
                    // Set button color to gray if not purchased and not affordable
                    actionButton.image.sprite = greyButtonSprite;
                    backdropImage.color = Color.gray;
                    actionButtonText.text = "Acheter";
                }
            }
        }

        public void RemoveCostsList()
        {
            foreach (GameObject costObject in costsObjects)
            {
                Destroy(costObject);
            }
        }
    }
}