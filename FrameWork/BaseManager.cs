using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FrameWork
{
    public abstract class BaseManager
    {
        protected FrameWorkManager m_FrameWorkManager;
        public BaseManager(FrameWorkManager frameWorkManager )
        {
            m_FrameWorkManager = frameWorkManager;
        }
        virtual public void Init()
        { }
    }
}