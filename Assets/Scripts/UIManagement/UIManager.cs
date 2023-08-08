using System;
using System.Collections.Generic;
using AnimalInformation;
using CircularListContent;
using CircularListManagement;
using MenuManagement;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UIManagement
{
    public class UIManager : MonoBehaviour
    {
        [Header("Managers")]
        [SerializeField] private MenuController menuController;
        [SerializeField] private CircularListManager circularListManager;
        
        [Header("Panels")]
        [SerializeField] private GameObject loadingPanel;
        
        [Header("Pages")]
        [SerializeField] private Page animalListPage;
        [SerializeField] private Page animalInfoPage;
        
        [Header("Buttons")]
        [SerializeField] private Button fetchButton;
        
        [Header("Texts")]
        [SerializeField] private TMP_Text animalDescriptionText;
        
        public UnityEvent<Action<List<AnimalInfo>>> onFetchButtonClicked = new();
        public UnityEvent<AnimalListContent, Action<string>> onAnimalSelected = new();
        
        private void Start()
        {
            fetchButton.onClick.AddListener(() =>
            {
                loadingPanel.SetActive(true);
                onFetchButtonClicked.Invoke((list) =>
                {
                    circularListManager.InitializeTheList(list);
                    menuController.PushPage(animalListPage);
                    loadingPanel.SetActive(false);
                });
            });
            
            AnimalInfoListBox.OnAnimalSelected.AddListener((animalListContent) =>
            {
                onAnimalSelected.Invoke(animalListContent, (description) =>
                {
                    animalDescriptionText.text = description;
                });
                menuController.PushPage(animalInfoPage);
            });
        }
    }
    
}
