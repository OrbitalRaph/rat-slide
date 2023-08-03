using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MenuScripts
{
    public class TabGroup : MonoBehaviour
    {
        public List<TabButton> tabButtons;
        public Sprite tabIdle;
        public Sprite tabActive;
        public TabButton selectedTab;
        public PanelGroup panelGroup;

        public void Subscribe(TabButton button)
        {
            tabButtons ??= new List<TabButton>();

            tabButtons.Add(button);
        }

        public void OnTabSelected(TabButton button)
        {
            if (panelGroup.isChanging) { return; }

            if (selectedTab == button) { 
                selectedTab.Deselect();
                selectedTab = null;
                ResetTabs();
                
                panelGroup?.SetPageIndex(-1);
                return;
            }
            
            selectedTab = button;
            selectedTab.Select();

            ResetTabs();
            button.image.sprite = tabActive;
            button.iconPosition.transform.localPosition = new Vector3(0, -5, 0);
            
            panelGroup?.SetPageIndex(button.transform.GetSiblingIndex());
        }

        public void ResetTabs()
        {
            foreach (TabButton button in tabButtons)
            {
                if (button == selectedTab) { continue; }
                button.image.sprite = tabIdle;
                button.iconPosition.transform.localPosition = new Vector3(0, 8, 0);
            }
        }
    }
}
