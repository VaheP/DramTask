using AirFishLab.ScrollingList.ContentManagement;
using Newtonsoft.Json;

namespace AnimalInformation
{
    public class AnimalInfo : IListContent
    {
        [JsonProperty("status")] public AnimalInfoStatus Status;
        [JsonProperty("_id")] public string ID;
        [JsonProperty("user")] public string User;
        [JsonProperty("text")] public string Text;
        [JsonProperty("type")] public string Type;
        [JsonProperty("deleted")] public bool Deleted;
        [JsonProperty("createdAt")] public string CreatedAt;
        [JsonProperty("updatedAt")] public string UpdatedAt;
        [JsonProperty("__v")] public int V;
        
        public override string ToString()
        {
            return $"ID: {ID}\nUser: {User}\nText: {Text}\nType: {Type}\nDeleted: {Deleted}\nCreatedAt: {CreatedAt}\nUpdatedAt: {UpdatedAt}\nV: {V}";
        }
    }

    public class AnimalInfoStatus
    {
        [JsonProperty("verified")] public bool? Verified;
        [JsonProperty("sentCount")] public int SentCount;
    }
}
