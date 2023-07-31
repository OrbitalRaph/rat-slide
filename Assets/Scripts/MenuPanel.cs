using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MenuScripts
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MenuPanel : MonoBehaviour
    {   
        public void ShowMenu()
        {
            LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 1, 0.5f).setEaseOutBack();
        }

        public void HideMenu()
        {
            LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 0, 0.5f).setEaseInBack().setOnComplete(() => gameObject.SetActive(false));
        }
    }
}
