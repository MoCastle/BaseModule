using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameProject
{

    public abstract class BaseScrollFormElement : MonoBehaviour
    {
        //protected float m_OriginalSize;
        //        {
        //            get
        //            {
        //                return m_Rect.localScale.x / m_OriginalSize;
        //            }
        //    set
        //            {
        //                float size = m_Rect.localScale.x * value;
        //    m_Rect.localScale = new Vector3(size, size, size);
        //}
        //        }

        protected RectTransform m_Rect;
        public int idx;

        #region 流程
        private void Awake()
        {
            m_Rect = GetComponent<RectTransform>();
            OnAwake();
        }

        protected virtual void OnAwake()
        {
            
        }

        public abstract void Despawn();
        #endregion
        public virtual float scale
        {
            get
            {
                return m_Rect.localScale.x;
            }
            set
            {
                m_Rect.localScale = new Vector3(value, value, value);
            }
        }
    }

}