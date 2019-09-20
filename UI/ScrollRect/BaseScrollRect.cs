using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GameProject.UI.Scroll
{
    public abstract class BaseScrollRect : MonoBehaviour
    {
        protected RectTransform m_ElementContainer;
        protected RectTransform m_Rect;
        protected internal IAgentScrollElemetInfos m_ElementsInfoAgent;
        protected List<ScrollElement> m_ShowingElements;
        protected LinkedList<ScrollElement> m_PoolOfElements;
        protected bool m_Seted;
        protected bool m_IsForceToJumpTo;

        [SerializeField]
        protected RectTransform m_SampleElement;
        [SerializeField]
        protected float m_SpaceBTElements;
        [SerializeField]
        protected bool m_AutoBounceToMidle;

        #region 流程
        public void Init()
        {
            m_SampleElement.gameObject.active = false;
            m_Rect = GetComponent<RectTransform>();
            m_ShowingElements = new List<ScrollElement>();
            m_PoolOfElements = new LinkedList<ScrollElement>();
            OnInit();
        }

        protected virtual void OnInit()
        {
            
        }

        private void Update()
        {
            if (!m_Seted)
                return;
            OnScroll();
            if(m_AutoBounceToMidle)
                OnBounce();
        }

        public void Reset(IAgentScrollElemetInfos elementInfosAgent)
        {
            m_ElementsInfoAgent = elementInfosAgent;
            Reset();
        }
        public virtual void Reset()
        {
            m_Seted = true;
        }
        #endregion
        #region 回弹
        public int nearlyElementIdx
        {
            get;
            protected set;
        }

        protected virtual void OnBounce()
        {
            if (CheckCanBounce())
            {
                Bounce();
            }else if(m_IsForceToJumpTo)
            {
                m_IsForceToJumpTo = false;
            }
        }

        protected virtual bool CheckCanBounce()
        {
            if (scrolling)
                return false;
            if (isDragging)
                return false;
            if (showingElementsNum < 1)
                return false;
            return true;
        }

        protected abstract void Bounce();

        protected abstract int GetCenterNearlyElement();

        #endregion
        #region 列表功能
        protected virtual bool scrolling
        {
            get
            {
                return false;
            }
        }
        protected virtual bool isDragging
        {
            get;
            set;
        }

        protected virtual void OnScroll()
        {
            int count = 0;
            while (Scroll() && count <1000)
            {
                ++count;
            }
        }

        protected bool Scroll()
        {
            if (showingElementsNum <= 0)
            {
                if (m_ElementsInfoAgent.GetInfoCount() <= 0)
                    return false;
            }

            if(showingElementsNum > 0)
            {
                if (IsOutHeadBounce())
                {
                    ScrollElement headElement = RemoveElementFromShoing(0);
                    DisPawnElement(headElement);
                    return true;
                }

                if (IsOutTailBounce())
                {
                    ScrollElement tailElement = RemoveElementFromShoing(showingElementsNum - 1);
                    DisPawnElement(tailElement);
                    return true;
                }
            }
            
            if (IsNeedAddHead())
            {
                ScrollElement headElement = SpawnElement();
                AddHead(headElement);
                return true;
            }

            if (IsNeedAddTail())
            {
                ScrollElement tailElement = SpawnElement();
                AddTail(tailElement);
                return true;
            }
            return false;
        }

        #endregion
        #region 元素位置
        protected abstract bool IsOutHeadBounce();
        protected abstract bool IsOutTailBounce();
        protected abstract bool IsNeedAddHead();
        protected abstract bool IsNeedAddTail();
        #endregion
        #region 数据管理

        #endregion
        #region 元素管理
        protected abstract ScrollElement RemoveElementFromShoing(int Idx);

        protected abstract void AddHead(ScrollElement headElement);

        protected abstract void AddTail(ScrollElement tailElement);

        protected int showingElementsNum
        {
            get { return m_ShowingElements.Count; }
        }

        protected ScrollElement GetElementByShowingIdx(int Idx)
        {
            return m_ShowingElements[Idx];
        }

        protected ScrollElement InitScrollElement(ScrollElement scrollElement,int idx)
        {
            scrollElement.idx = idx;
            
            scrollElement = m_ElementsInfoAgent.SetItemInfo(scrollElement);
            return scrollElement;
        }

        protected virtual ScrollElement SpawnElement()
        {
            ScrollElement element;
            if (m_PoolOfElements.Count > 0)
            {
                element = m_PoolOfElements.First.Value;
                m_PoolOfElements.RemoveFirst();
            }
            else
            {
                RectTransform newElementRect = GameObject.Instantiate<RectTransform>(m_SampleElement);
                BaseScrollFormElement elementScript = newElementRect.GetComponent<BaseScrollFormElement>();
                element = new ScrollElement(newElementRect, elementScript);
                if (m_ElementContainer != null)
                    element.rect.SetParent(m_ElementContainer, false);
            }
            element.rect.gameObject.active = true;
            return element;
        }

        protected virtual void DisPawnElement(ScrollElement element)
        {
            element.rect.gameObject.active = false;
            element.script.Despawn();
            m_PoolOfElements.AddLast(element);
        }
        #endregion
    }
}