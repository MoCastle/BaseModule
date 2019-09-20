using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork.UI
{
    public class UIGroup : MonoBehaviour
    {
        Dictionary<string, Form> m_FormDict;
        LinkedList<Form> m_FormList;

        public void Init()
        {
            m_FormDict = new Dictionary<string, Form>();
            m_FormList = new LinkedList<Form>();
        }

        public Form GetForm(string name)
        {
            Form form;
            m_FormDict.TryGetValue(name,out form);
            return form;
        }

        public void AddForm(Form form)
        {
            if(form == null)
            {
                return;
            }
            m_FormDict.Add(form.name,form);
            m_FormList.AddFirst(form);

            //RectTransform formTrans = form.GetComponent<RectTransform>();
            //RectTransform groupTrans = transform.GetComponent<RectTransform>();
            //formTrans.P
            Transform formTransform = form.transform;
            formTransform.SetParent(transform,false);
            formTransform.localScale = Vector3.one;
        }

        public void RemoveForm(Form form)
        {
            if (form == null)
                return;
            m_FormDict.Remove(form.name);
            m_FormList.Remove(form);
        }

        public void Refresh()
        {
            bool hide = false;
            bool cover = false;
            IEnumerator<Form> formEnumerator = m_FormList.GetEnumerator();
            formEnumerator.Reset();

            while (formEnumerator.MoveNext())
            {
                if(hide)
                {
                    if(!formEnumerator.Current.hided)
                    {
                        formEnumerator.Current.OnHide();
                    }
                }else
                {
                    if(formEnumerator.Current.hided)
                    {
                        formEnumerator.Current.OnShow();
                    }
                    if(formEnumerator.Current.cover)
                    {
                        formEnumerator.Current.OnResume();
                    }
                    hide = true;
                }
            }
        }
    }
}