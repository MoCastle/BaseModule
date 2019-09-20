using System.Collections;
using System.Collections.Generic;
using GameProject.UI;
using UnityEngine;
namespace GameProject
{

    public class DefaultElementInfoAgent : MonoBehaviour, IAgentScrollElemetInfos
    {
        int[] m_Infos;

        public DefaultElementInfoAgent(int[] infos)
        {
            m_Infos = infos;
        }

        public void ResetInfos(int[] infos)
        {
            m_Infos = infos;
        }

        public object GetElementInfo(int idx)
        {
            return m_Infos[idx];
        }

        public int GetInfoCount()
        {
            return m_Infos.Length;
        }

        public ScrollElement SetItemInfo(ScrollElement element)
        {
            DefaultScrollElement elementScript = element.script as DefaultScrollElement;
            if (element.idx < 0 || element.idx > m_Infos.Length)
                Debug.Log(element.idx);
            elementScript.Init(m_Infos[element.idx]);
            return element;
        }
    }

}