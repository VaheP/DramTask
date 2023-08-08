using System;
using AirFishLab.ScrollingList;
using AirFishLab.ScrollingList.ContentManagement;
using AnimalInformation;
using UnityEngine.Serialization;

namespace CircularListContent
{
    public class AnimalInfoBank: BaseListBank
    {
        public AnimalListContent[] AnimalInfos;
        
        
        public void SetAnimalInfos(AnimalListContent[] animalInfos)
        {
            AnimalInfos = animalInfos;
        }
        
        public override IListContent GetListContent(int index)
        {
            return AnimalInfos[index];
        }

        public override int GetContentCount()
        {
            return AnimalInfos.Length;
        }
    }
    
    [Serializable]
    public class AnimalListContent : IListContent
    {
        public string type;
    }

}