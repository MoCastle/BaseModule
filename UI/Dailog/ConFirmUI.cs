using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ConFirmUI : MonoBehaviour
{
    public Text tipsText;
    public Button noBtn;
    public Button okBtn;
    public Image dailogimg;

    private System.Action okCallBak;
    private System.Action noCallBak;

    void Start()
    {
        float aspectRatio = Screen.width * 1.0f / Screen.height;
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(AppConfig.designWidth, AppConfig.designWidth / aspectRatio);
        this.dailogimg.transform.localScale = new Vector2(0.2f, 0.2f);
        this.transform.localScale = new Vector3(1f, 1f, 1f);
        this.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
    }

    public void InitConFirmContent(string txt, int type, System.Action okCallBak, System.Action noCallBak = null)
    {
        if(type == 1)
        {
            this.noBtn.gameObject.SetActive(false);
            this.okBtn.transform.localPosition = new Vector2(this.okBtn.transform.localPosition.x - 160, this.okBtn.transform.localPosition.y);
        } 
        this.tipsText.text = txt;
        this.noCallBak = noCallBak;
        this.okCallBak = okCallBak;
        Sequence s = DOTween.Sequence();
        s.AppendInterval(0.02f);
        s.AppendCallback(()=>{
            this.dailogimg.transform.DOScale(new Vector2(1f, 1f), 0.2f).SetEase(Ease.OutSine);
        });
        
    }

    public void OnOkBtn()
    {
        if(this.okCallBak != null)
        {
            this.okCallBak();
        }
        Destroy(this.gameObject);
    }

    public void OnNoBtn()
    {
        if (this.noCallBak != null)
        {
            this.noCallBak();
        }
        Destroy(this.gameObject);
    }
}
