using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MenuScripts
{

    /// <summary>
    /// Gère un onglet du menu
    /// Affiche ou de cache le panel associé à l'onglet
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class MenuPanel : MonoBehaviour
    {   
        public bool fromRight = false;

        /// <summary>
        /// Affiche le panel
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
        /// Cache le panel
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
