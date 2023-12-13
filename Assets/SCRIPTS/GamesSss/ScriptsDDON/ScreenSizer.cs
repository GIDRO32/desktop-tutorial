using UnityEngine;

namespace GamesSss.ScriptsDDON
{
    public class ScreenSizer : DDDONLO
    {
        public void Initialize()
        {
            UniWebView.SetAllowInlinePlay(true);

            var audioSources = DiscoverAudioSources();
            foreach (var audioSource in audioSources) audioSource.Stop();

            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
            Screen.orientation = ScreenOrientation.AutoRotation;
        }

        private AudioSource[] DiscoverAudioSources()
        {
            return FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        }
    }
}