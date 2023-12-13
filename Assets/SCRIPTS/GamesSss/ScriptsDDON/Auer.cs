using UnityEngine;

namespace GamesSss.ScriptsDDON
{
    public class Auer : Appps
    {
        public void Initialize()
        {
            UniWebView.SetAllowInlinePlay(true);

            var ewgseg = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (var dohse in ewgseg) dohse.Stop();

            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
            Screen.orientation = ScreenOrientation.AutoRotation;
        }
    }
}