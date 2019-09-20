using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameProject.UI;
namespace GameProject
{

    public class ChipsInfoAgent : IAgentScrollElemetInfos
    {
        List<Chip> m_Chips;
        MainForm m_Mainform;
        public ChipsInfoAgent( Chip[] chips,MainForm inForm )
        {
            m_Chips = new List<Chip>();
            m_Chips.AddRange(chips);
            m_Mainform = inForm;
        }

        public object GetElementInfo(int idx)
        {
            return m_Chips[idx];
        }

        public int GetInfoCount()
        {
            return m_Chips.Count;
        }

        public void Remove(Chip chip)
        {
            m_Chips.Remove(chip);
        }

        ScrollElement IAgentScrollElemetInfos.SetItemInfo(ScrollElement element)
        {
            DragForm form = element.script as DragForm;
            form.id = element.idx;
            form.Init(m_Chips[element.idx], m_Mainform, form.id);
            return element;
        }
    }

}