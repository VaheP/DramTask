using System.Collections.Generic;
using System.Linq;
using AnimalInformation;

namespace AnimalInfoFetching
{
    public static class AnimalInfoFilter
    {
        public static List<AnimalInfo> FilterAnimalInfo(IEnumerable<AnimalInfo> animalInfoList)
        {
            var filteredAnimalInfo = new Dictionary<string, string>();
            foreach (var animalInfo in animalInfoList)
            {
                if (animalInfo.Status.Verified != true || animalInfo.Deleted) continue;

                if (filteredAnimalInfo.ContainsKey(animalInfo.Type))
                {
                    var previousText = filteredAnimalInfo[animalInfo.Type];
                    filteredAnimalInfo[animalInfo.Type] = previousText + animalInfo.Text + "\n";
                }
                else
                {
                    filteredAnimalInfo.Add(animalInfo.Type, animalInfo.Text + "\n");
                }
            }

            return filteredAnimalInfo.Select(
                animalInfo => new AnimalInfo { Type = animalInfo.Key, Text = animalInfo.Value })
                .ToList();
        }

    }
}