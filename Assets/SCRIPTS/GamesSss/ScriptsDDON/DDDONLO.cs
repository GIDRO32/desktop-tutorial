using UnityEngine;

namespace GamesSss.ScriptsDDON
{
    public class DDDONLO : MonoBehaviour
    {
        public void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}