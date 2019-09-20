using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.gameObject.AddComponent<EventTrigger>();
        EventTrigger trigger = btn.gameObject.GetComponent<EventTrigger>();

        EventTrigger.Entry beginDragEntry = new EventTrigger.Entry();

        EventTrigger.Entry combinationChangeColorEntry = new EventTrigger.Entry();
        combinationChangeColorEntry.eventID = EventTriggerType.PointerDown;
        combinationChangeColorEntry.callback = new EventTrigger.TriggerEvent();
        combinationChangeColorEntry.callback.AddListener(OnPointerDown);
        trigger.triggers.Add(combinationChangeColorEntry);

        EventTrigger.Entry combinationClearColorEntry = new EventTrigger.Entry();
        combinationClearColorEntry.eventID = EventTriggerType.PointerUp;
        combinationClearColorEntry.callback = new EventTrigger.TriggerEvent();
        combinationClearColorEntry.callback.AddListener(OnPointerUp);
        trigger.triggers.Add(combinationClearColorEntry);
    }

    public void OnPointerDown(BaseEventData eventData)
    {
        this.gameObject.transform.DOScale(new Vector2(1.1f, 1.1f), 0.1f).SetEase(Ease.OutSine);
    }

    public void OnPointerUp(BaseEventData eventData)
    {
        this.gameObject.transform.DOKill(true);
        this.gameObject.transform.DOScale(new Vector2(1f, 1f), 0.1f).SetEase(Ease.InSine);
    }
}
