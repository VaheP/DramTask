using System.Collections.Generic;
using AirFishLab.ScrollingList;
using AnimalInformation;
using CircularListContent;
using UnityEngine;
using UnityEngine.Serialization;

namespace CircularListManagement
{
    public class CircularListManager: MonoBehaviour
    {
        [SerializeField]
        private CircularScrollingList list;
        [SerializeField]
        private BaseListBank listBankSource;
        [SerializeField]
        private ListBox listBoxSource;

        
        public void InitializeTheList(List<AnimalInfo> animalInfos)
        {
            var animalInfoBank = listBankSource as AnimalInfoBank;
            
            switch (animalInfos.Count)
            {
                case 0:
                    return;
                case 1:
                    animalInfos.Add(animalInfos[0]);
                    break;
            }

            AnimalListContent[] animalListContents = new AnimalListContent[animalInfos.Count];
            for (int i = 0; i < animalInfos.Count; i++)
            {
                animalListContents[i] = new AnimalListContent();
                animalListContents[i].type = animalInfos[i].Type;
            }

            if (animalInfoBank != null) animalInfoBank.SetAnimalInfos(animalListContents);

            list.SetListBank(listBankSource);

            var boxSetting = list.BoxSetting;
            boxSetting.SetBoxPrefab(listBoxSource);
            boxSetting.SetNumOfBoxes(animalListContents.Length);

            var listSetting = list.ListSetting;
            listSetting.SetFocusSelectedBox(true);

            list.Initialize();
        }
    }
}