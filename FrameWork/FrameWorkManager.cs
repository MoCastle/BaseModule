using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectBase;

namespace FrameWork
{
    public class FrameWorkManager: BaseSingleton<FrameWorkManager>
    {
        Dictionary<string, BaseManager> m_ManagerDict;
        #region  流程
        public FrameWorkManager()
        {
            m_ManagerDict = new Dictionary<string, BaseManager>();
        }

        public void Init()
        {
            AddManager(new EventManager(this));
            AddManager(new DataTableManager(this));
            AddManager(new UIManager(this));
            foreach( BaseManager manager in m_ManagerDict.Values)
            {
                manager.Init();
            }
        }

        #endregion

        #region 管理
        public void AddManager<T>( T manager ) where T:BaseManager
        {
            m_ManagerDict.Add(typeof(T).Name, manager);
        }

        public T GetManager<T>() where T : BaseManager
        {
            BaseManager manager = null;
            m_ManagerDict.TryGetValue(typeof(T).Name, out manager);

            return manager as T;
        }
        #endregion
    }

}