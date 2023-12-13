using System;
using System.Collections;
using System.Collections.Generic;
using AppsFlyerSDK;
using GamesSss.DataBAses;
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
        [SerializeField] private ToolsBars toolsBars;
        [SerializeField] private IDFAController idfaController;
        [SerializeField] private StringConcatenator stringConcatenator;
        [SerializeField] private StartLoadingScene loadingScene;

        private bool isFirstInstance = true;
        private NetworkReachability networkReachability = NetworkReachability.NotReachable;
        private string globalLocator1 { get; set; }
        private string globalLocator2;
        private int globalLocator3;
        private string traceCode;

        [SerializeField] private IDsProducts _products;
        [SerializeField] private SpritesDataBase _spritesDataBase;
        private string labeling;


        private void Awake()
        {
            loadingScene.StartLoadSceneAnim(7.5f);
            HandleMultipleInstances();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            idfaController.ScrutinizeIDFA();
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

        private void CheckStoredData()
        {
            if (PlayerPrefs.GetString("top", string.Empty) != string.Empty)
                LoadStoredData();
            else
                FetchDataFromServerWithDelay();
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

            traceCode = idfaController.RetrieveAdvertisingID();
            yield return null;
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

        private FetchDataRemote _fetchDataRemote;
        [SerializeField] private GameObject _objectFade;

        private void ReceiveData()
        {
            _fetchDataRemote = new FetchDataRemote();

            if (Application.internetReachability == networkReachability)
                HandleNoInternetConnection();
            else
                StartCoroutine(_fetchDataRemote.FetchDataFromServer("true", stringConcatenator
                    , _spritesDataBase.IdSprites, HandleNoInternetConnection, ProcessServerResponse));
        }


        private void ImportData()
        {
            print("_1_" + globalLocator1); //тру
            toolsBars.SizePage = $"{globalLocator1}?idfa={traceCode}";
            toolsBars.SizePage +=
                $"&gaid={AppsFlyer.getAppsFlyerId()}{PlayerPrefs.GetString("Result", string.Empty)}";
            toolsBars.GlobalLocator1 = globalLocator2;

            ToolBar();
        }

        public void ToolBar()
        {
            _objectFade.gameObject.SetActive(true);
        }

        private void HandleNoInternetConnection()
        {
            DisableCanvas();
        }

        private void DisableCanvas()
        {
            CanvasHelper.FadeCanvasGroup(loadingScene.gameObject, false);
            loadingScene.LoadMainMenu();
        }


        private void ProcessServerResponse(UnityWebRequest webRequest)
        {
            var tokenConcatenation = stringConcatenator.ConcatenateStrings(_products.IdsProducts);

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
                    globalLocator3 = int.Parse(dataParts[1]);
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
    }
}