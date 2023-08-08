using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace AnimalInfoFetching
{
    public static class AnimalInfoFetcher
    {
        private const string AnimalInfoUrl =
            "https://cat-fact.herokuapp.com/facts/random?animal_type=cat,dog,horse&amount=200";
        
        public static async Task<string> GetAnimalInfoJson()
        {
            using var webRequest = UnityWebRequest.Get(AnimalInfoUrl);
            
            webRequest.SetRequestHeader("Content-Type", "application/json");
            var operation = webRequest.SendWebRequest();
            
            while (!operation.isDone)
            {
                await Task.Yield();
            }
            
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);
                return null;
            }

            Debug.Log("Received: " + webRequest.downloadHandler.text);
            return webRequest.downloadHandler.text;
        }
        
    }
}