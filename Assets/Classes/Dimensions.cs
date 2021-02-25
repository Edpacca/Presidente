using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Classes
{
    public static class Dimensions
    {
        public static Vector2 CornerPosition(GameObject gameObject)
        {
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            Vector2 position = gameObject.transform.position;
            return rectTransform.offsetMin;
        }
    }
}
