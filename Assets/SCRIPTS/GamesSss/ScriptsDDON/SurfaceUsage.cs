using System;
using UnityEngine;

namespace GamesSss.ScriptsDDON
{
    public class SurfaceUsage : MonoBehaviour
    {
        private void HandlePageCompleted(object sender, EventArgs e)
        {
            Debug.Log("Page completed event received");
        }

        private bool HandleRequestClose(object sender)
        {
            Debug.Log("Request close event received");
            return true; // Approve the request to close
        }
    }
}