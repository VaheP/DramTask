using System;
using System.Collections.Generic;
using AnimalInfoFetching;
using AnimalInformation;
using CircularListContent;
using UIManagement;
using UnityEngine;

namespace GameManagement
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UIManager uiManager;
        [SerializeField] private List<AnimalInfo> filteredAnimalInfoList;

        private void Awake()
        {
            uiManager.onFetchButtonClicked.AddListener(OnFetchButtonClicked);
            uiManager.onAnimalSelected.AddListener(OnAnimalSelected);
        }

        private void Start()
        {

        }
        
        private async void OnFetchButtonClicked(Action<List<AnimalInfo>> onFetchFinished)
        {
            string json = await AnimalInfoFetcher.GetAnimalInfoJson();
            Debug.Log(json);
            
            List<AnimalInfo> animalInfoList = AnimalInfoOutputter.GetAnimalInfoList(json);
            List<AnimalInfo> filteredAnimalInfo = AnimalInfoFilter.FilterAnimalInfo(animalInfoList);
            filteredAnimalInfoList = new List<AnimalInfo>(filteredAnimalInfo);

            onFetchFinished.Invoke(filteredAnimalInfo);
        }
        
        private string FindDescription(string type)
        {
            foreach (var animalInfo in filteredAnimalInfoList)
            {
                if (animalInfo.Type == type)
                {
                    return animalInfo.Text;
                }
            }

            return "";
        }
        
        private void OnAnimalSelected(AnimalListContent listContent, Action<string> onAnimalSelected)
        {
            onAnimalSelected.Invoke(FindDescription(listContent.type));
        }
    }
}