using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameWork.UI;
using UnityEngine.UI;
using FrameWork;
using DG.Tweening;

namespace GameProject
{
    public class ItemPopForm : Form
    {
        public Text numText;
        public Button close;
        public Button buy;
        public Slider chooseSlider;
        public Image itemIcon;
        public Text warning;
        public Text costMoney;
        public Text money;
        int m_idx;
        Tweener m_PunchAnim;

        public override void Init(UIFormData data, object arg = null)
        {
            m_idx = (arg as int?)??0;
            base.Init(data, arg);
            RectTransform warningRect = warning.transform as RectTransform;
            m_PunchAnim = warningRect.DOPunchScale(new Vector3(2, 2), 0.3f,1);
            m_PunchAnim.Pause();
            //m_PunchAnim.setAutoKill(false);
            m_PunchAnim.SetAutoKill(false);
            ShopInfo info = ShopInfo.TryGet(m_idx.ToString());
            itemIcon.sprite = Resources.Load<Sprite>(info.icon);
        }

        private void Awake()
        {
            chooseSlider.onValueChanged.AddListener(OnValueChange);
            chooseSlider.value = 0;
            close.onClick.AddListener(Close);
            buy.onClick.AddListener(OnBuy);
            money.text = AppConfig.playerData.Gold.ToString();
        }

        private void Start()
        {
            OnValueChange(chooseSlider.value);
        }

        void OnValueChange(float addNum)
        {
            numText.text = (1 + addNum).ToString();
            int costNum = GetTotalCost(addNum);
            costMoney.text = "-" + costNum;
            int playerGold = AppConfig.playerData.Gold;
            if (playerGold < costNum)
            {
                warning.gameObject.active = true;
            } else
            {
                warning.gameObject.active = false;
            }

        }

        void OnBuy()
        {
            int value = (int)chooseSlider.value + 1;
            int totalCost = GetTotalCost(chooseSlider.value);
            //m_PunchAnim.Pause();
            if (warning.gameObject.active)
            {
                m_PunchAnim.Restart();
                return;
            }
            Constant.BuyToolById(m_idx, (int)chooseSlider.value + 1);
            Close();
            
        }

        int GetTotalCost(float addNum)
        {
            ShopInfo info = ShopInfo.TryGet(m_idx.ToString());
            return (int)((1 + addNum) * info.costgold);
        }

        public override void OnClosed()
        {
            base.OnClosed();
            m_PunchAnim.Kill();
        }
    }
}