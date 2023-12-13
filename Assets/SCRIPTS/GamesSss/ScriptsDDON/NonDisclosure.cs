using UnityEngine;

namespace GamesSss.ScriptsDDON
{
    public class NonDisclosure : MonoBehaviour
    {
        public Auer Auer;

        public void OnEnable()
        {
            Auer.Initialize();
        }

        public string GlobalLocator1;

        public string WebPageLink
        {
            get => _webPageLink;
            set => _webPageLink = value;
        }

        public int ToolbarHeight = 70;

        private string _webPageLink;
        private UniWebView webView;
        private GameObject loadingIndicator;

        private void Start()
        {
            SetupUI();
            LoadWebPage(_webPageLink);
            HideLoadingIndicator();
        }

        private void SetupUI()
        {
            InitializeWebView();

            switch (WebPageLink)
            {
                case "0":
                    webView.SetShowToolbar(true, false, false, true);
                    break;
                default:
                    webView.SetShowToolbar(false);
                    break;
            }

            webView.Frame = new Rect(0, ToolbarHeight, Screen.width, Screen.height - ToolbarHeight);

            // Other setup logic...

            webView.OnPageFinished += (_, _, url) =>
            {
                if (PlayerPrefs.GetString("LastLoadedPage", string.Empty) == string.Empty)
                    PlayerPrefs.SetString("LastLoadedPage", url);
            };
        }

        private void InitializeWebView()
        {
            webView = GetComponent<UniWebView>();
            if (webView == null) webView = gameObject.AddComponent<UniWebView>();

            webView.OnShouldClose += _ => false;

            // Other initialization logic...
        }

        private void LoadWebPage(string url)
        {
            if (!string.IsNullOrEmpty(url)) webView.Load(url);
        }

        private void HideLoadingIndicator()
        {
            if (loadingIndicator != null) loadingIndicator.SetActive(false);
        }
    }
}