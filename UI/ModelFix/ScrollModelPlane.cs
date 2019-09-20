using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameProject.UI;
using UnityEngine.EventSystems;

namespace GameProject
{
    public class ScrollModelPlane : ExtendUGUIScroll
    {
        [SerializeField]
        GameObject m_ScrollBG;

        public Camera camera;


        public override void Reset()
        {
            base.Reset();
            camera = Camera.main;
            int elmentIdx;
            int elementNum = m_ElementsInfoAgent.GetInfoCount();
            if (camera != null)
            {
                for (elmentIdx = 0; elmentIdx < elementNum; ++elmentIdx)
                {
                    Vector2 chipPos = CountPosByIdx(elmentIdx);
                    chipPos.x += m_SampleElement.rect.width / 2;
                    Vector3 worldPositon = Vector3.zero;
                    worldPositon = camera.ScreenToWorldPoint(chipPos);
                    worldPositon += camera.transform.forward * 5;
                    Chip chip = m_ElementsInfoAgent.GetElementInfo(elmentIdx) as Chip;
                    chip.dragChip.position = worldPositon;
                }
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            m_ScrollBG.active = true;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            m_ScrollBG.active = false;
        }
    }
}