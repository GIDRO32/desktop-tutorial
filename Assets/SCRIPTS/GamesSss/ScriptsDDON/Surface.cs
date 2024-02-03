using System;
using UnityEngine;

namespace GamesSss.ScriptsDDON
{
    public class Surface : MonoBehaviour
    {
        public delegate void PageCompletedHandler(object sender, EventArgs e);

        public delegate bool RequestCloseHandler(object sender);

        public event PageCompletedHandler OnPageCompleted;
        public event RequestCloseHandler OnRequestClose;

        private string _currentPosStr;

        public void NavigateTotransform(string pos)
        {
            _currentPosStr = pos;

            Debug.Log($"Navigating to: {pos}");

            // Notify page completion
            NotifyPageCompleted();
        }

        public void SetFrame(Rect rect)
        {
            Debug.Log($"Setting frame to: {rect}");
        }

        public void ShowNavigationBar(bool isMake, bool isShow, bool isWork)
        {
            // Implement show navigation bar logic here
            Debug.Log($"Show Navigation Bar - Make: {isMake}, Show: {isShow}, Work: {isWork}");
        }

        public void ShowNavigationBar(bool isMako)
        {
            // Implement show navigation bar logic here
            Debug.Log($"Show Navigation Bar - Make: {isMako},");
        }

        public void Close()
        {
            Debug.Log("Closing browser surface");

            if (OnRequestClose != null && OnRequestClose(this))
            {
                Debug.Log("Request close approved");
            }
        }

        private void NotifyPageCompleted()
        {
            // Notify subscribers that the page has been completed
            if (OnPageCompleted != null)
            {
                EventArgs args = EventArgs.Empty;
                OnPageCompleted(this, args);
            }
        }
    }
}