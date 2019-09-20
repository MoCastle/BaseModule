using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameProject.UI.Scroll
{

    public interface IScrollAgent
    {
        float elementSpace
        {
            get;
        }
        Vector2 elementLineDirector
        {
            get;
        }
        bool isDraging
        {
            get;
        }
        void Init(BaseScrollRect scrollRect);
        Vector2 CountCenterPosOfView();
        Vector2 CountHeadPosOfView();
        Vector2 CountTailPosOfView();
    }

}