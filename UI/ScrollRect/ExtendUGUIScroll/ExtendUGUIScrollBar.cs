using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameProject.UI.Scroll
{

    public class ExtendUGUIScrollBar : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        public bool isDragging
        {
            get;
            private set;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            isDragging = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;
        }
    }

}