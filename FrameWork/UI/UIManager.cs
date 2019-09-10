using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameWork.UI;

namespace FrameWork
{
    public class UIManager : BaseManager
    {
        Dictionary<string, UIFormData> m_FormDataDict;
        DataTable<UIFormData> m_UIDatatable;
        Dictionary<string, UIGroup> m_GroupDict;
        Transform m_UICanavasTrans;

        public UIManager(FrameWorkManager frameWorkManager):base(frameWorkManager)
        {
            m_FormDataDict = new Dictionary<string, UIFormData>();
            m_UICanavasTrans = new GameObject("UICanvas").transform;
            m_GroupDict = new Dictionary<string, UIGroup>();
        }

        public override void Init()
        {
            DataTable<UIFormData> m_UIDatatable = m_FrameWorkManager.GetManager<DataTableManager>().GetDataTable<UIFormData>();
            foreach(UIFormData data in m_UIDatatable)
            {
                m_FormDataDict.Add(data.uiName, data);
            }
        }

        public void Open<T>(FormArg formArg = null) where T:Form
        {
            Open(typeof(T).Name, formArg);
        }

        public void Open(string formName, FormArg formArg = null)
        {
            UIFormData data = null;
            m_FormDataDict.TryGetValue(formName, out data);
            if(data!=null)
            {
                InternalOpenUI(data, formArg);
            }
        }

        public void Open(int formID, FormArg formArg = null)
        {
            UIFormData data = null;
            data = m_UIDatatable.GetData(formID);
            if (data != null)
            {
                InternalOpenUI( data, formArg);
            }
        }

        void InternalOpenUI(UIFormData formData, FormArg formArg = null)
        {
            UIGroup group = GetGroup(formData.groupName);
            Form form = group.GetForm(formData.uiName);

            if(form == null)
            {
                form = LoadForm(formData.uiName);
                group.AddForm(form);
            }

            if (form == null)
                return;
            form.Init(formData,formArg);
            form.OnOpen();
        }

        public void CloseForm(Form form)
        {
            form.OnClose();

            UIFormData data = form.formData;
            UIGroup group = GetGroup(data.groupName);
            group.RemoveForm(form);
            GameObject.Destroy(form.gameObject);
        }

        public void HideForm(Form form)
        {
            form.OnHide();
        }

        #region 分组
        public UIGroup GetGroup(string name)
        {
            UIGroup group = null;
            if (!m_GroupDict.TryGetValue(name, out group))
            {
                group = Resources.Load<UIGroup>("Prefab/UI/FormGroup/" + name);
                group = GameObject.Instantiate<UIGroup>(group);
                group.name = name;
                group.transform.SetParent(m_UICanavasTrans,false);
                group.Init();
                m_GroupDict.Add(name, group);
            }
            return group;
        }
        #endregion
        #region UI
        public Form LoadForm(string name)
        {
            Form form = Resources.Load<Form>("Prefab/UI/" + name);
            form = GameObject.Instantiate<Form>(form);
            form.name = name;
            return form;
        }
        #endregion
    }
}