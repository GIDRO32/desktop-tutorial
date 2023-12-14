using System.Collections.Generic;
using UnityEngine;

namespace GamesSss.DataBAses
{
    public class IDsProducts : MonoBehaviour
    {
        [SerializeField] private List<string> idsProducts;

        public List<string> IdsProducts => idsProducts;
    }
}