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
    public class ContinuePopForm : Form
    {
        [SerializeField]
        Text gametime;
        [SerializeField]
        Text historytime;
        [SerializeField]
        GameObject m_NewRecorderTitle;
        Action m_LeaveAction;
        private void Start()
        {
            int costTime = (int)ModelFixManager.singleton.modelGameDirector.costTime;
            string history = Constant.GetPassGameTime(AppConfig.playerData.GetSelectGameId());
            if (history == "无")
            {
                history = ((int)ModelFixManager.singleton.modelGameDirector.costTime).ToString();
                m_NewRecorderTitle.gameObject.active = true;
            }
            else
            {
                m_NewRecorderTitle.gameObject.active = false;
            }
            int historyTime = int.Parse(history);
            this.gametime.text = "Time used:" + costTime;
            this.historytime.text = "Histarical records:" + history;
            
        }
        
        protected override void InternalClose()
        {
            base.InternalClose();
            if(m_LeaveAction!=null)
            {
                m_LeaveAction();
            }
        }
        #region 按钮事件
        public void OnLeave()
        {
            m_LeaveAction = ModelFixManager.singleton.modelGameDirector.CompleteGame;
            Close();
            //m_UIManager.Open<ConfirmPopForm>(new ConfirmPopFormArg(InternalLeave, null, "MakeSureToLeave"));
        }
        void InternalLeave()
        {
            m_LeaveAction = ModelFixManager.singleton.modelGameDirector.CompleteGame;
            Close();
        }

        public void OnNext()
        {
            m_LeaveAction = ModelFixManager.singleton.modelGameDirector.MoveNextLevel;
            Close();
        }
        public void OnRestart()
        {
            m_LeaveAction = ModelFixManager.singleton.modelGameDirector.Restart;
            Close();
        }

        #endregion
    }
}