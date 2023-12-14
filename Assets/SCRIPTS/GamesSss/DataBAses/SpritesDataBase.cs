using System.Collections.Generic;
using UnityEngine;

namespace GamesSss.DataBAses
{
    public class SpritesDataBase : MonoBehaviour
    {
        [SerializeField] private List<string> idSprites;
        public List<string> IdSprites => idSprites;
    }
}