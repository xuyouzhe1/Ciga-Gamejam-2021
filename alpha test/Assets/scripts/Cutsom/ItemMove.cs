using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ItemMove : MonoBehaviour
{
    private Transform beginParentTransform; //��¼��ʼ�϶�ʱ�ĸ�������        

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
        //��ʼ��ק���¼�
        UnityAction<BaseEventData> click = new UnityAction<BaseEventData>(Begin);
        EventTrigger.Entry myclick = new EventTrigger.Entry();
        myclick.eventID = EventTriggerType.BeginDrag;
        myclick.callback.AddListener(click);

        trigger.triggers.Add(myclick);

        //��ק�е��¼�
        UnityAction<BaseEventData> _click = new UnityAction<BaseEventData>(OnDrag);
        EventTrigger.Entry myclick_ = new EventTrigger.Entry();
        myclick_.eventID = EventTriggerType.Drag;
        myclick_.callback.AddListener(_click);
        trigger.triggers.Add(myclick_);
        //������ק���¼�
        UnityAction<BaseEventData> Endclick = new UnityAction<BaseEventData>(End);
        EventTrigger.Entry Endclick_ = new EventTrigger.Entry();
        Endclick_.eventID = EventTriggerType.EndDrag;
        Endclick_.callback.AddListener(Endclick);
        trigger.triggers.Add(Endclick_);
    }
    /// <summary>
    /// ��ʼ�϶�ʱ
    /// </summary>
    public void Begin(BaseEventData data)
    {
        if (transform.parent == canvasParent) return;
        beginParentTransform = transform.parent;
        transform.SetParent(canvasParent);
    }


    /// <summary>
    /// �϶���
    /// </summary>
    /// <param name="e">UI�¼�����</param>
    public void OnDrag(BaseEventData e)
    {
        transform.position = Input.mousePosition;
        if (transform.GetComponent<Image>().raycastTarget)
            transform.GetComponent<Image>().raycastTarget = false;
    }


    /// <summary>
    /// ����ʱ
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
        if (target.tag == "Grid") //�����ǰ�϶��������ǣ����ӣ�û����Ʒ��ʱ
        {
            SetPos_Parent(transform, target.transform);
            transform.GetComponent<Image>().raycastTarget = true;
        }
        else if (target.tag == "Good") //�������Ʒ
        {
            SetPos_Parent(transform, target.transform.parent);                              //����ǰ�϶���Ʒ���õ�Ŀ��λ��
            target.transform.SetParent(canvasParent);                                             //Ŀ����Ʒ���õ� UI ����
            SetPos_Parent(target.transform, beginParentTransform); //�ٽ�Ŀ����Ʒ���õ���ʼ�϶���Ʒ�ĸ����� �������Ʒ���л�

            transform.GetComponent<Image>().raycastTarget = true;
        }
        else //�����κ����������ع�ԭʼλ��
        {
            SetPos_Parent(transform, beginParentTransform);
            transform.GetComponent<Image>().raycastTarget = true;
        }
        transform.localPosition = Vector3.zero;
    }


    /// <summary>
    /// ���ø����壬UIλ�ù���
    /// </summary>
    /// <param name="targetods">����Transform</param>
    /// <param name="parent">Ҫ���õ��ĸ���</param>
    private void SetPos_Parent(Transform targetods, Transform parent)
    {
        targetods.SetParent(parent);
        targetods.localPosition = Vector3.zero;
    }
}