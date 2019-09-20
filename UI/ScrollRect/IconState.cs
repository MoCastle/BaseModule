using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameProject
{

    public class IconState : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
    {
        public bool isPressed
        {
            get;
            private set;
        }
        public static IconState GetIconState(GameObject gameObject)
        {
            IconState stateMono;
            stateMono = gameObject.GetComponent<IconState>();
            if (stateMono == null)
                stateMono = gameObject.AddComponent<IconState>();

            return stateMono;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPressed = false;
        }
    }

}