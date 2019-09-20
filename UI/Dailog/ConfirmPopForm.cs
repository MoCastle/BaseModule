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
    public class ConfirmPopForm : Form
    {
        [SerializeField]
        public Text tipsText;
        [SerializeField]
        public Button noBtn;
        [SerializeField]
        public Button okBtn;
        [SerializeField]
        public Image dailogimg;

        Action m_Confirm;
        Action m_Cancel;

        Action m_LeaveAction;
        public override void Init(UIFormData data, object arg = null)
        {
            base.Init(data, arg);
            ConfirmPopFormArg confirmArg = (arg as ConfirmPopFormArg?)??new ConfirmPopFormArg();
            tipsText.text = confirmArg.message;
            m_Confirm = confirmArg.confirmAction;
            m_Cancel = confirmArg.cancelAction;
            m_LeaveAction = m_Cancel;
        }

        private void Start()
        {
        }

        protected override void InternalClose()
        {
            if (m_LeaveAction != null)
                m_LeaveAction();
            base.InternalClose();
        }
        #region 按钮事件
        public void Confirm()
        {
            m_LeaveAction = m_Confirm;
            Close();
        }
        public void Cancel()
        {
            Close();
        }

        #endregion
    }
}