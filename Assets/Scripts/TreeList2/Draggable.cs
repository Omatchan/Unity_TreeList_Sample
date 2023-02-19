using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class Draggable : MonoBehaviour
{
    [SerializeField] private GameObject dragObject;
    [SerializeField] private bool horizontal = true;
    [SerializeField] private bool vertical = true;
    [SerializeField] private List<GameObject> resizeObjects;
    private Transform root;
    private Transform self;
    private CanvasGroup canvasGroup = null;

    private Vector2 startPos;
    private Vector2 resizeStartSize;
    private Vector2 mouseStartPos;

    public void Awake()
    {
        this.self = this.transform;
        this.root = this.self.parent;
        this.canvasGroup = this.GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(BaseEventData eventData)
    {
        // UI 機能を一時的無効化
        this.canvasGroup.blocksRaycasts = false;

        //移動したいUIオブジェクトの最初の位置
        startPos = dragObject.transform.position;
        if (resizeObjects?.Count > 0)
            resizeStartSize = (resizeObjects[0].transform as RectTransform).sizeDelta;
        //ドラッグを開始したときのマウスの位置
        mouseStartPos = ((PointerEventData)eventData).position;
    }

    public void OnDrag(BaseEventData eventData)
    {
        //ドラッグ中のUIオブジェクトの位置
        var moveAmount = (((PointerEventData)eventData).position - mouseStartPos);
        var dragPosition = startPos + moveAmount;

        if (horizontal && vertical)
        {
            dragObject.transform.position = dragPosition;
        }
        else if (horizontal)
        {
            dragObject.transform.position = new Vector3(dragPosition.x, dragObject.transform.position.y, dragObject.transform.position.z);
            if (resizeObjects?.Count > 0)
            {
                foreach(var resizeObject in resizeObjects)
                {
                    var sizeDelta = (resizeObject.transform as RectTransform).sizeDelta;
                    var resizeDelta = new Vector2(resizeStartSize.x + moveAmount.x, sizeDelta.y);
                    Debug.Log($"sx: {resizeStartSize.x,11:F5}  mx: {moveAmount.x,11:F5}  rx: {resizeDelta.x,11:F5}");
                    (resizeObject.transform as RectTransform).sizeDelta = resizeDelta;
                }

                //resizeObject.transform.position = resizeStartPos - new Vector2(moveAmount.x / 2, 0);
            }
        }
        else if (vertical)
        {
            dragObject.transform.position = new Vector3(dragObject.transform.position.x, dragPosition.y, dragObject.transform.position.z); ;
        }
    }

    private static Vector3 GetLocalPosition(Vector3 position, Transform transform)
    {
        // 画面上の座標 (Screen Point) を RectTransform 上のローカル座標に変換
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            position,
            Camera.main,
            out var result);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.GetComponent<RectTransform>(),
            position,
            Camera.main,
            out var result2);
        
        return new Vector3(-result2.x, (transform.parent as RectTransform).rect.height + result2.y, 0);
        //return new Vector3(position.x, -position.y, 0);
    }

    public void OnEndDrag(BaseEventData eventData)
    {
        // UI 機能を復元
        this.canvasGroup.blocksRaycasts = true;
    }
}
