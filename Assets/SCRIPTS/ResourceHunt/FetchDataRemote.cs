using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace ResourceHunt
{
    public class FetchDataRemote : IDataFetcher
    {
        public IEnumerator FetchDataFromServer(string data, StringConcatenator stringConcatenator,
            List<string> idSprites, Action EndTrue, Action<UnityWebRequest> AEndFalse)
        {
            using var webRequest = UnityWebRequest.Get(stringConcatenator.ConcatenateStrings(idSprites));
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.DataProcessingError)
                EndTrue?.Invoke();
            else
                AEndFalse?.Invoke(webRequest);
        }
    }
}