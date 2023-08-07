using System.Collections;
using System.Collections.Generic;
using MenuScripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Ce script s'occupe de gérer le jeu.
/// Il gère la fin de partie et la monnaie du joueur.
/// </summary>
public class GameManager : MonoBehaviour
{
    public SerializableDictionary<string, int> obtainedCurrency;
    public static GameManager Instance;
    public TabGroup tabGroup;
    public GameObject gameOverPanel;
    public UnityEvent onCurrencyObtained;
    private List<CurrencyType> currencyTypes;
    private bool gameOver = true;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        currencyTypes = CurrencyManager.Instance.currencyTypes;
    }

    public void StartGame()
    {
        // hide menu and change scene
        tabGroup.HideTabGroup();
        SceneManager.LoadScene("GameScene");
        obtainedCurrency = new();
        for (int i = 0; i < currencyTypes.Count; i++)
        {
            obtainedCurrency.Add(currencyTypes[i].uniqueName, 0);
        }
        
        print("Game Over false");
        gameOver = false;
    }

    /// <summary>
    /// Cette méthode permet de terminer le jeu.
    /// </summary>
    public void GameOver()
    {
        if (gameOver)
        {
            return;
        }
        print("Game Over true");
        gameOver = true;
        ShowGameOverMenu();
        CurrencyManager.Instance.AddCurrency(obtainedCurrency);
    }

    public void BackToMenu()
    {
        HideGameOverMenu();

        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
        tabGroup.ShowTabGroup();
    }

    /// <summary>
    /// Generate random numbers of currency
    /// </summary>
    public void GenerateCurrency()
    {
        if (gameOver)
        {
            return;
        }
        foreach (CurrencyType currencyType in currencyTypes)
        {
            // odds of getting a currency number from this type (use currencytype.rarity)
            // rarity is a number between 0 and 10, 0 being the most common and 10 being the rarest
            int rarity = currencyType.rarity;
            int odds = 10 - rarity;
            int randomNumber = Random.Range(0, 10);
            if (randomNumber < odds)
            {
                // generate a random number of currency
                int amount = Random.Range(1, 10 - rarity);
                obtainedCurrency[currencyType.uniqueName] += amount;
            }
        }

        onCurrencyObtained.Invoke();
    }

    /// <summary>
    /// Cette fonction permet d'afficher le panel de fin de partie
    /// </summary>
    public void ShowGameOverMenu()
    {
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.localPosition = new Vector3(0, 1000, 0);
        LeanTween.moveLocalY(gameOverPanel, 0, 0.5f).setEaseOutCubic();
        LeanTween.alphaCanvas(gameOverPanel.GetComponent<CanvasGroup>(), 1, 0.5f).setEaseOutCubic();
    }

    /// <summary>
    /// Cette fonction permet de cacher le panel de fin de partie
    /// </summary>
    public void HideGameOverMenu()
    {
        LeanTween.moveLocalY(gameOverPanel, 1000, 0.5f).setEaseOutCubic();
        LeanTween.alphaCanvas(gameOverPanel.GetComponent<CanvasGroup>(), 0, 0.5f).setEaseOutCubic().setOnComplete(() => gameOverPanel.SetActive(false));
    }
}
