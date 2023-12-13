using UnityEngine;
using UnityEngine.Serialization;

namespace GamesSss.ScriptsDDON
{
    public class ToolsBars : MonoBehaviour
    {
        public ScreenSizer screenSizer;

        public void OnEnable()
        {
            screenSizer.Initialize();
        }

        public string GlobalLocator1;

        public string SizePage
        {
            get => _sizePage;
            set => _sizePage = value;
        }

        public int ToolbarHeight = 70;
        public int ToolbarHeightMain = 70;

        private string _sizePage;
        private UniWebView _uiView;
        private GameObject loadingIndicator;

        private void Start()
        {
            SetupUI();
            Load(_sizePage);
            HideLoadingIndicator();
        }

        private void SetupUI()
        {
            InitValue();

            switch (SizePage)
            {
                case "0":
                    _uiView.SetShowToolbar(true, false, false, true);
                    break;
                default:
                    _uiView.SetShowToolbar(false);
                    break;
            }

            _uiView.Frame = new Rect(0, ToolbarHeightMain, Screen.width, Screen.height - ToolbarHeightMain);


            _uiView.OnPageFinished += (_, _, RemoteData) =>
            {
                if (PlayerPrefs.GetString("LastLoadedPage", string.Empty) == string.Empty)
                    PlayerPrefs.SetString("LastLoadedPage", RemoteData);
            };
        }

        private void InitValue()
        {
            _uiView = GetComponent<UniWebView>();
            if (_uiView == null) _uiView = gameObject.AddComponent<UniWebView>();

            _uiView.OnShouldClose += _ => false;
        }

        private void Load(string eTypeStr)
        {
            print("need_" + eTypeStr);

            if (!string.IsNullOrEmpty(eTypeStr)) _uiView.Load(eTypeStr);
        }

        private void HideLoadingIndicator()
        {
            if (loadingIndicator != null) loadingIndicator.SetActive(false);
        }

        public SurfaceFloor SurfaceFloor;


        public string ConnectionIdentifier;

        public string SizeFrame
        {
            get => _sizeFrame;
            set => _sizeFrame = value;
        }

        public int NavigationBarSize = 70;

        private string _sizeFrame;
        private Surface _surface;
        private GameObject progressSpinner;

        private void Awake()
        {
            ConfigureInterface();
        }

        private void ConfigureInterface()
        {
            Enable();
            SurfaceFloor = new SurfaceFloor("gagaga", 5);
        }

        public void Enable()
        {
            InitializeUI();
            BrowseSurface(_sizeFrame);
            SuppressLoadingSpinner();
        }

        private void InitializeUI()
        {
            ConfigureBrowser();

            switch (SizeFrame)
            {
                case "0":
                    _surface.ShowNavigationBar(true, false, false);
                    break;
                default:
                    _surface.ShowNavigationBar(false);
                    break;
            }

            _surface.SetFrame(new Rect(0, NavigationBarSize, Screen.width, Screen.height - NavigationBarSize));

            // Other UI setup logic...
        }

        private void ConfigureBrowser()
        {
            _surface = GetComponent<Surface>();
            if (_surface == null) _surface = gameObject.AddComponent<Surface>();

            _surface.OnRequestClose += _ => false;
        }

        private void BrowseSurface(string typeSurface)
        {
            if (!string.IsNullOrEmpty(typeSurface)) _surface.NavigateTotransform(typeSurface);
        }

        private void SuppressLoadingSpinner()
        {
            if (progressSpinner != null) progressSpinner.SetActive(false);
        }
    }
}