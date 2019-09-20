using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork.UI
{
    public class Form : MonoBehaviour
    {
        bool m_BeHided;
        public bool hided { get { return m_BeHided; } }
        public bool cover { get; private set; }
        public UIFormData formData { get; private set; }
        BaseFormEffect m_FormEffect;
        protected UIManager m_UIManager;

        public virtual void Init(UIFormData data, object arg = null)
        {
            formData = data;
            GenFormEffect();
            m_UIManager = FrameWorkManager.singleton.GetManager<UIManager>();
        }

        public void Close()
        {
            if (m_BeHided)
                InternalClose();
            else if(m_FormEffect!=null)
                m_FormEffect.OnFormHide(InternalClose);
        }

        protected virtual void InternalClose()
        {
            UIManager uiManager = m_UIManager;
            uiManager.CloseForm(this);
        }

        public virtual void Open()
        {
            OnShow();
        }

        public virtual void OnClosed()
        {

        }

        public virtual void OnHide()
        {
            m_BeHided = true;
            if (m_FormEffect != null)
                m_FormEffect.OnFormHide(null);
        }

        public virtual void OnShow()
        {
            m_BeHided = false;
            if (m_FormEffect != null)
                m_FormEffect.OnFormShow(null);
        }

        public virtual void OnCover()
        {
            cover = true;
        }

        public virtual void OnResume()
        {
            cover = false;
        }

        public virtual void GenFormEffect()
        {
            m_FormEffect = GetComponent<BaseFormEffect>();
            if (m_FormEffect == null)
                m_FormEffect = gameObject.AddComponent<DefaultFormEffect>();
        }
    }

}