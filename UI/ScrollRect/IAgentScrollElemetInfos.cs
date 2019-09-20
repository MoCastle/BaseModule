using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameProject.UI;
namespace GameProject
{

    public interface IAgentScrollElemetInfos
    {
        object GetElementInfo(int idx);
        ScrollElement SetItemInfo(ScrollElement element);
        int GetInfoCount();
    }

}