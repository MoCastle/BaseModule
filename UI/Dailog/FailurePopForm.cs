using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using FrameWork.UI;
using UnityEngine.UI;
using FrameWork;
using DG.Tweening;

namespace GameProject
{
    public class FailurePopForm : Form
    {
        [SerializeField]
        Text m_CostGold;
        [SerializeField]
        Text m_AddTime;

        Action m_LeaveAction;

        private void Start()
        {
        }

        public override void OnClosed()
        {
            base.OnClosed();
            if (m_LeaveAction != null)
            {
                m_LeaveAction();
            }
        }
        
        #region 按钮事件
        public void OnAddTime()
        {
            Close();
            m_LeaveAction = ModelFixManager.singleton.modelGameDirector.BuyTimeToContinue;
        }

        #endregion
    }
}