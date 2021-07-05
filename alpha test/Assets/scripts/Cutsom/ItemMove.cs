using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ItemMove : MonoBehaviour
{
    private Transform beginParentTransform; //记录开始拖动时的父级对象        

    private Transform canvasParent;
    private EventTrigger trigger;
    // Use this for initialization
    void Start()
    {
        canvasParent = GameObject.Find("UIRoot").transform;
        if (!GetComponent<EventTrigger>())
        { 
            trigger = gameObject.AddComponent<EventTrigger>();
        }
        else
        {
            trigger = gameObject.GetComponent<EventTrigger>();
        }
        //开始拖拽的事件
        UnityAction<BaseEventData> click = new UnityAction<BaseEventData>(Begin);
        EventTrigger.Entry myclick = new EventTrigger.Entry();
        myclick.eventID = EventTriggerType.BeginDrag;
        myclick.callback.AddListener(click);

        trigger.triggers.Add(myclick);

        //拖拽中的事件
        UnityAction<BaseEventData> _click = new UnityAction<BaseEventData>(OnDrag);
        EventTrigger.Entry myclick_ = new EventTrigger.Entry();
        myclick_.eventID = EventTriggerType.Drag;
        myclick_.callback.AddListener(_click);
        trigger.triggers.Add(myclick_);
        //结束拖拽的事件
        UnityAction<BaseEventData> Endclick = new UnityAction<BaseEventData>(End);
        EventTrigger.Entry Endclick_ = new EventTrigger.Entry();
        Endclick_.eventID = EventTriggerType.EndDrag;
        Endclick_.callback.AddListener(Endclick);
        trigger.triggers.Add(Endclick_);
    }
    /// <summary>
    /// 开始拖动时
    /// </summary>
    public void Begin(BaseEventData data)
    {
        if (transform.parent == canvasParent) return;
        beginParentTransform = transform.parent;
        transform.SetParent(canvasParent);
    }


    /// <summary>
    /// 拖动中
    /// </summary>
    /// <param name="e">UI事件数据</param>
    public void OnDrag(BaseEventData e)
    {
        transform.position = Input.mousePosition;
        if (transform.GetComponent<Image>().raycastTarget)
            transform.GetComponent<Image>().raycastTarget = false;
    }


    /// <summary>
    /// 结束时
    /// </summary>
    public void End(BaseEventData data)
    {
        PointerEventData e = data as PointerEventData;
        if (e == null)
        {
            return;
        }
        GameObject target = e.pointerCurrentRaycast.gameObject;
        if (!target)
        {
            SetPos_Parent(transform, beginParentTransform);
            transform.GetComponent<Image>().raycastTarget = true;
            return;
        }
        if (target.tag == "Grid") //如果当前拖动物体下是：格子（没有物品）时
        {
            SetPos_Parent(transform, target.transform);
            transform.GetComponent<Image>().raycastTarget = true;
        }
        else if (target.tag == "Good") //如果是物品
        {
            SetPos_Parent(transform, target.transform.parent);                              //将当前拖动物品设置到目标位置
            target.transform.SetParent(canvasParent);                                             //目标物品设置到 UI 顶层
            SetPos_Parent(target.transform, beginParentTransform); //再将目标物品设置到开始拖动物品的父物体 来完成物品的切换

            transform.GetComponent<Image>().raycastTarget = true;
        }
        else //其他任何情况，物体回归原始位置
        {
            SetPos_Parent(transform, beginParentTransform);
            transform.GetComponent<Image>().raycastTarget = true;
        }
        transform.localPosition = Vector3.zero;
    }


    /// <summary>
    /// 设置父物体，UI位置归正
    /// </summary>
    /// <param name="targetods">对象Transform</param>
    /// <param name="parent">要设置到的父级</param>
    private void SetPos_Parent(Transform targetods, Transform parent)
    {
        targetods.SetParent(parent);
        targetods.localPosition = Vector3.zero;
    }
}