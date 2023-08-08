using AirFishLab.ScrollingList;
using AirFishLab.ScrollingList.ContentManagement;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CircularListContent
{
    public class AnimalInfoListBox: ListBox
    {
        [SerializeField]
        private TMP_Text animalType;
        [SerializeField]
        private Button button;
        
        public static readonly UnityEvent<AnimalListContent> OnAnimalSelected = new();

        protected override void UpdateDisplayContent(IListContent content)
        {
            var animalListContent = (AnimalListContent)content;
            animalType.text = animalListContent.type;
            
            button.onClick.AddListener(() => OnAnimalSelected.Invoke(animalListContent));
        }
    }
}