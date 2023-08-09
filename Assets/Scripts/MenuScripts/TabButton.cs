using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace MenuScripts
{
    /// <summary>
    /// Gère les boutons d'onglets du menu
    /// appel des évènements lorsqu'un onglet est sélectionné ou désélectionné
    /// Le TabButton s'enregistre au TabGroup lors de son instanciation
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class TabButton : MonoBehaviour, IPointerClickHandler
    {
        public TabGroup tabGroup;
        public Image image;
        public Transform iconPosition;
        public UnityEvent onTabSelected;
        public UnityEvent onTabDeselected;

        private void Start()
        {
            image = GetComponent<Image>();
            tabGroup.Subscribe(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            tabGroup.OnTabSelected(this);
        }

        public void Select()
        {
            onTabSelected?.Invoke();
        }

        public void Deselect()
        {
            onTabDeselected?.Invoke();
        }
    }
}