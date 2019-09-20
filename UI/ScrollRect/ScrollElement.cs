using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameProject.UI
{

    public struct ScrollElement
    {
        public RectTransform rect;
        public int idx;
        public BaseScrollFormElement script;

        public ScrollElement(RectTransform inRect, BaseScrollFormElement inScript)
        {
            rect = inRect;
            script = inScript;
            idx = -1;
        }
    }

}