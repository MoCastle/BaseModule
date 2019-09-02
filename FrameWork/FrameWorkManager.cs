using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FrameWork
{

    public class FrameWorkManager
    {
        Dictionary<string, BaseManager> m_ManagerDict;
        #region  流程
        public FrameWorkManager()
        {
            m_ManagerDict = new Dictionary<string, BaseManager>();
        }
        public void Init()
        {
            m_ManagerDict.Add(typeof(EventManager).Name, new EventManager());
        }
        #endregion

        #region 管理
        public T GetManager<T>() where T : BaseManager
        {
            BaseManager manager = null;
            m_ManagerDict.TryGetValue(typeof(T).Name, out manager);

            return manager as T;
        }
        #endregion
    }

}