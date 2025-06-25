using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler // 드래그 핸들러, 드래그 시작, 드래그 엔드 인터페이스 상속
{
    [SerializeField] Transform itemTr;
    [SerializeField] Transform inventoryTr;
    public static GameObject draggingItem = null;
    CanvasGroup canvasGroup;
    [SerializeField] Transform itemListTr;
    void Start()
    {
        itemTr = transform;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // 드래그 이벤트
    public void OnDrag(PointerEventData eventData)
    {
        itemTr.position = Input.mousePosition;
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.transform.SetParent(inventoryTr); // 드래그가 시작 되었을때 부모를 인벤토리로 한다
        draggingItem = this.gameObject; // 드래그가 시작되면 드래그 되는 아이템 정보를 저장함
        canvasGroup.blocksRaycasts = false; // 드래그가 시작되면 다른 UI 이벤트를 받지 않도록 설정 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draggingItem = null; // 드래그가 종료 되었을 때 null 정보를 넣어준다    
        canvasGroup.blocksRaycasts = true; // 드래그가 끝나면 UI 이벤트를 받는다.

        if(itemTr.parent == inventoryTr) // 슬롯에 드래그 되어 있지 않았을때는 원래의 itemListTr로 되돌아간다.
        {
            itemTr.SetParent(itemListTr.transform);
        }
    }
}
