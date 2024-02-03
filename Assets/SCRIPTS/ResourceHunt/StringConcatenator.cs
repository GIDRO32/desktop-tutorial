using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ResourceHunt
{
    public class StringConcatenator : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public string ConcatenateStrings(List<string> stringsToConcatenate)
        {
            var resultBuilder = new StringBuilder();
            foreach (var str in stringsToConcatenate) resultBuilder.Append(str);

            return resultBuilder.ToString();
        }
    }
}