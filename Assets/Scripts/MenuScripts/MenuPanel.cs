using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MenuScripts
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MenuPanel : MonoBehaviour
    {   
        public void ShowMenu(bool fromRight = false)
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

        public void HideMenu(bool toRight = false)
        {
            if (toRight)
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
