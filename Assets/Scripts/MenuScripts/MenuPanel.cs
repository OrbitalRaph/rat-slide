using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MenuScripts
{

    /// <summary>
    /// Cette classe permet de gérer un onglet du menu
    /// Elle permet d'afficher ou de cacher le panel associé à l'onglet
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class MenuPanel : MonoBehaviour
    {   
        public bool fromRight = false;

        /// <summary>
        /// Cette fonction permet d'afficher le panel
        /// </summary>
        public void ShowMenu()
        {
            if (fromRight)
            {
                gameObject.transform.localPosition = new Vector3(1000, 0, 0);
            }
            else
            {
                gameObject.transform.localPosition = new Vector3(-1000, 0, 0);
            }
            LeanTween.moveLocalX(gameObject, 0, 0.5f).setEaseOutCubic();
            LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 1, 0.5f).setEaseOutCubic();
        }

        /// <summary>
        /// Cette fonction permet de cacher le panel
        /// </summary>
        public void HideMenu()
        {
            if (fromRight)
            { 
                LeanTween.moveLocalX(gameObject, 1000, 0.5f).setEaseOutCubic();
            }
            else
            {
                LeanTween.moveLocalX(gameObject, -1000, 0.5f).setEaseOutCubic();
            }
            LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 0, 0.5f).setEaseOutCubic().setOnComplete(() => gameObject.SetActive(false));
        }
    }
}
