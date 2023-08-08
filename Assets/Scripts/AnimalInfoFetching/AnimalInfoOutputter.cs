using System.Collections.Generic;
using AnimalInformation;
using Newtonsoft.Json;

namespace AnimalInfoFetching
{
    public class AnimalInfoOutputter
    {
        public static List<AnimalInfo> GetAnimalInfoList(string json)
        {
            return JsonConvert.DeserializeObject<List<AnimalInfo>>(json);
        }
    }
}