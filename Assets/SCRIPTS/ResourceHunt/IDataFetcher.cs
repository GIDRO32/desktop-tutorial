using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace ResourceHunt
{
    public interface IDataFetcher
    {
        IEnumerator FetchDataFromServer(string data, StringConcatenator stringConcatenator,
            List<string> idSprites, Action EndTrue, Action<UnityWebRequest> AEndFalse);
    }
}