using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork.UI
{
    public class Form : MonoBehaviour
    {
        public bool hided { get { return gameObject.active; } }
        public bool cover { get; private set; }
        public UIFormData formData { get; private set; }
        public virtual void Init(UIFormData data, FormArg arg = null)
        {
            formData = data;
        }

        public void Close()
        {
            FrameWorkManager.singleton.GetManager<UIManager>().CloseForm(this);
        }

        public virtual void OnOpen()
        {

        }

        public virtual void OnClose()
        {

        }

        public virtual void OnHide()
        {

        }

        public virtual void OnShow()
        {

        }

        public virtual void OnCover()
        {
            cover = true;
        }

        public virtual void OnResume()
        {
            cover = false;
        }
    }

}