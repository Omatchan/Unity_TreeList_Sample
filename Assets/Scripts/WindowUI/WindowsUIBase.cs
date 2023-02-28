using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WindowsUIBase : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    private GameObject title;
    private GameObject topBar;
    private GameObject bottomBar;
    private GameObject leftBar;
    private GameObject rightBar;
    private GameObject topLeftCorner;
    private GameObject topRightCorner;
    private GameObject bottomLeftCorner;
    private GameObject bottomRightCorner;
    private GameObject basePanel;

    private CanvasGroup canvasGroup = null;

    private Vector2 mouseStartPos;
    private int dragTyoe;
    private Dictionary<GameObject, Vector2> sizes = new Dictionary<GameObject, Vector2>();
    private Dictionary<GameObject, Vector3> positions = new Dictionary<GameObject, Vector3>();
    private Dictionary<GameObject, Vector3> localPositions = new Dictionary<GameObject, Vector3>();



    // Start is called before the first frame update
    void Start()
    {
        this.canvasGroup = this.GetComponent<CanvasGroup>();

        title = mainPanel.transform.Find("Title").gameObject;
        topBar = mainPanel.transform.Find("TopBar").gameObject;
        bottomBar = mainPanel.transform.Find("BottomBar").gameObject;
        leftBar = mainPanel.transform.Find("LeftBar").gameObject;
        rightBar = mainPanel.transform.Find("RightBar").gameObject;
        topLeftCorner = mainPanel.transform.Find("TopLeftCorner").gameObject;
        topRightCorner = mainPanel.transform.Find("TopRightCorner").gameObject;
        bottomLeftCorner = mainPanel.transform.Find("BottomLeftCorner").gameObject;
        bottomRightCorner = mainPanel.transform.Find("BottomRightCorner").gameObject;
        basePanel = mainPanel.transform.Find("Base").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBeginDrag(BaseEventData eventData)
    {
        // UI 機能を一時的無効化
        this.canvasGroup.blocksRaycasts = false;

        //移動したいUIオブジェクトの最初の位置
        //startPos = eventData.selectedObject.transform.position;

        positions[mainPanel] = (mainPanel.transform as RectTransform).position;
        positions[title] = (title.transform as RectTransform).position;
        positions[topBar] = (topBar.transform as RectTransform).position;
        positions[bottomBar] = (bottomBar.transform as RectTransform).position;
        positions[leftBar] = (leftBar.transform as RectTransform).position;
        positions[rightBar] = (rightBar.transform as RectTransform).position;
        positions[topLeftCorner] = (topLeftCorner.transform as RectTransform).position;
        positions[topRightCorner] = (topRightCorner.transform as RectTransform).position;
        positions[bottomLeftCorner] = (bottomLeftCorner.transform as RectTransform).position;
        positions[bottomRightCorner] = (bottomRightCorner.transform as RectTransform).position;
        positions[basePanel] = (basePanel.transform as RectTransform).position;

        localPositions[mainPanel] = (mainPanel.transform as RectTransform).localPosition;
        localPositions[title] = (title.transform as RectTransform).localPosition;
        localPositions[topBar] = (topBar.transform as RectTransform).localPosition;
        localPositions[bottomBar] = (bottomBar.transform as RectTransform).localPosition;
        localPositions[leftBar] = (leftBar.transform as RectTransform).localPosition;
        localPositions[rightBar] = (rightBar.transform as RectTransform).localPosition;
        localPositions[topLeftCorner] = (topLeftCorner.transform as RectTransform).localPosition;
        localPositions[topRightCorner] = (topRightCorner.transform as RectTransform).localPosition;
        localPositions[bottomLeftCorner] = (bottomLeftCorner.transform as RectTransform).localPosition;
        localPositions[bottomRightCorner] = (bottomRightCorner.transform as RectTransform).localPosition;
        localPositions[basePanel] = (basePanel.transform as RectTransform).localPosition;

        sizes[mainPanel] = (mainPanel.transform as RectTransform).sizeDelta;
        sizes[title] = (title.transform as RectTransform).sizeDelta;
        sizes[topBar] = (topBar.transform as RectTransform).sizeDelta;
        sizes[bottomBar] = (bottomBar.transform as RectTransform).sizeDelta;
        sizes[leftBar] = (leftBar.transform as RectTransform).sizeDelta;
        sizes[rightBar] = (rightBar.transform as RectTransform).sizeDelta;
        sizes[topLeftCorner] = (topLeftCorner.transform as RectTransform).sizeDelta;
        sizes[topRightCorner] = (topRightCorner.transform as RectTransform).sizeDelta;
        sizes[bottomLeftCorner] = (bottomLeftCorner.transform as RectTransform).sizeDelta;
        sizes[bottomRightCorner] = (bottomRightCorner.transform as RectTransform).sizeDelta;
        sizes[basePanel] = (basePanel.transform as RectTransform).sizeDelta;

        if (eventData.selectedObject.Equals(title))
            dragTyoe = 1;
        else if (eventData.selectedObject.Equals(topBar))
            dragTyoe = 2;
        else if (eventData.selectedObject.Equals(bottomBar))
            dragTyoe = 3;
        else if (eventData.selectedObject.Equals(leftBar))
            dragTyoe = 4;
        else if (eventData.selectedObject.Equals(rightBar))
            dragTyoe = 5;
        else if (eventData.selectedObject.Equals(topLeftCorner))
            dragTyoe = 6;
        else if (eventData.selectedObject.Equals(topRightCorner))
            dragTyoe = 7;
        else if (eventData.selectedObject.Equals(bottomLeftCorner))
            dragTyoe = 8;
        else if (eventData.selectedObject.Equals(bottomRightCorner))
            dragTyoe = 9;

        //ドラッグを開始したときのマウスの位置
        mouseStartPos = ((PointerEventData)eventData).position;
    }

    public void OnDrag(BaseEventData eventData)
    {
        //ドラッグ中のUIオブジェクトの位置
        var moveAmount = (((PointerEventData)eventData).position - mouseStartPos);

        if (dragTyoe != 1)
        {
            Vector2 scalingSize = new Vector2(0, 0);
            switch (dragTyoe)
            {
                case 2:
                    // 上側
                    scalingSize = new Vector2(0, moveAmount.y);
                    break;
                case 3:
                    // 下側
                    scalingSize = new Vector2(0, -moveAmount.y);
                    break;
                case 4:
                    // 左側
                    scalingSize = new Vector2(-moveAmount.x, 0);
                    break;
                case 5:
                    // 右側
                    scalingSize = new Vector2(moveAmount.x, 0);
                    break;
                case 6:
                    // 左上角
                    scalingSize = new Vector2(-moveAmount.x, moveAmount.y);
                    break;
                case 7:
                    // 右上角
                    scalingSize = new Vector2(moveAmount.x, moveAmount.y);
                    break;
                case 8:
                    // 左下角
                    scalingSize = new Vector2(-moveAmount.x, -moveAmount.y);
                    break;
                case 9:
                    // 右下角
                    scalingSize = new Vector2(moveAmount.x, -moveAmount.y);
                    break;
            }
            var panelSize = sizes[mainPanel] + scalingSize;
            Debug.Log($"SizeCheck  {dragTyoe}  MoveAmount   x: {moveAmount.x,11:F5}  y: {moveAmount.y,11:F5}");
            Debug.Log($"SizeCheck  {dragTyoe}  ScalingSize  x: {scalingSize.x,11:F5}  y: {scalingSize.y,11:F5}");
            Debug.Log($"SizeCheck  {dragTyoe}  PanelSize    x: {panelSize.x,11:F5}  y: {panelSize.y,11:F5}");
            if (panelSize.x < 100 || panelSize.y < 50)
            {
                Debug.Log($" -> Return");
                return;
            }
        }

        switch (dragTyoe)
        {
            case 1:
                // タイトル
                MoveXY(mainPanel, moveAmount, true);
                break;
            case 2:
                // 上側
                MoveY(mainPanel, moveAmount, true);
                ResizeY(mainPanel, moveAmount, false);
                ResizeY(leftBar, moveAmount, false);
                ResizeY(rightBar, moveAmount, false);

                MoveLocalY(bottomBar, moveAmount, false);
                MoveLocalY(bottomLeftCorner, moveAmount, false);
                MoveLocalY(bottomRightCorner, moveAmount, false);
                break;
            case 3:
                // 下側
                MoveY(bottomBar, moveAmount, true);
                MoveY(bottomLeftCorner, moveAmount, true);
                MoveY(bottomRightCorner, moveAmount, true);

                ResizeY(mainPanel, moveAmount, true);
                ResizeY(leftBar, moveAmount, true);
                ResizeY(rightBar, moveAmount, true);
                break;
            case 4:
                // 左側
                MoveX(mainPanel, moveAmount, true);
                ResizeX(mainPanel, moveAmount, false);
                ResizeX(title, moveAmount, false);
                ResizeX(topBar, moveAmount, false);
                ResizeX(bottomBar, moveAmount, false);
                
                MoveLocalX(rightBar, moveAmount, false);
                MoveLocalX(topRightCorner, moveAmount, false);
                MoveLocalX(bottomRightCorner, moveAmount, false);
                break;
            case 5:
                // 右側
                MoveX(rightBar, moveAmount, true);
                MoveX(topRightCorner, moveAmount, true);
                MoveX(bottomRightCorner, moveAmount, true);

                ResizeX(mainPanel, moveAmount, true);
                ResizeX(title, moveAmount, true);
                ResizeX(topBar, moveAmount, true);
                ResizeX(bottomBar, moveAmount, true);
                break;
            case 6:
                // 左上角
                MoveXY(mainPanel, moveAmount, true);
                ResizeXY(mainPanel, moveAmount, false);

                ResizeX(title, moveAmount, false);
                ResizeX(topBar, moveAmount, false);
                ResizeX(bottomBar, moveAmount, false);
                ResizeY(leftBar, moveAmount, false);
                ResizeY(rightBar, moveAmount, false);

                MoveLocalX(topRightCorner, moveAmount, false);
                MoveLocalY(bottomLeftCorner, moveAmount, false);
                MoveLocalXY(bottomRightCorner, moveAmount, false);

                MoveLocalX(rightBar, moveAmount, false);
                MoveLocalY(bottomBar, moveAmount, false);
                break;
            case 7:
                // 右上角
                var moveAmountInvY = new Vector2(moveAmount.x, -moveAmount.y);
                MoveY(mainPanel, moveAmount, true); 
                ResizeXY(mainPanel, moveAmountInvY, true);

                ResizeX(title, moveAmount, true);
                ResizeX(topBar, moveAmount, true);
                ResizeX(bottomBar, moveAmount, true);

                ResizeY(leftBar, moveAmountInvY, true);
                MoveLocalY(bottomBar, moveAmountInvY, true);
                MoveLocalY(bottomLeftCorner, moveAmountInvY, true);

                MoveLocalX(topRightCorner, moveAmount, true);
                MoveLocalX(rightBar, moveAmount, true);
                ResizeY(rightBar, moveAmountInvY, true);
                MoveLocalXY(bottomRightCorner, moveAmountInvY, true);
                break;
            case 8:
                // 左下角
                var moveAmountInvX = new Vector2(-moveAmount.x, moveAmount.y);
                MoveX(mainPanel, moveAmount, true);
                ResizeXY(mainPanel, moveAmountInvX, true);

                ResizeX(title, moveAmountInvX, true);
                ResizeX(topBar, moveAmountInvX, true);
                ResizeX(bottomBar, moveAmountInvX, true);

                ResizeY(leftBar, moveAmount, true);
                MoveLocalY(bottomBar, moveAmount, true);
                MoveLocalY(bottomLeftCorner, moveAmount, true);

                MoveLocalX(topRightCorner, moveAmountInvX, true);
                MoveLocalX(rightBar, moveAmountInvX, true);
                ResizeY(rightBar, moveAmount, true);
                MoveLocalXY(bottomRightCorner, moveAmountInvX, true);
                break;
            case 9:
                // 右下角
                MoveXY(bottomRightCorner, moveAmount, true);
                MoveX(rightBar, moveAmount, true);
                MoveX(topRightCorner, moveAmount, true);
                MoveY(bottomBar, moveAmount, true);
                MoveY(bottomLeftCorner, moveAmount, true);

                ResizeXY(mainPanel, moveAmount, true);
                ResizeX(title, moveAmount, true);
                ResizeX(topBar, moveAmount, true);
                ResizeX(bottomBar, moveAmount, true);
                ResizeY(leftBar, moveAmount, true);
                ResizeY(rightBar, moveAmount, true);
                break;
        }
    }

    public void OnEndDrag(BaseEventData eventData)
    {
        // UI 機能を復元
        this.canvasGroup.blocksRaycasts = true;
    }

    private void MoveX(GameObject target, Vector2 moveAmount, bool sign)
    {
        var beforePosition = positions[target];
        var position = new Vector3(
            sign ? beforePosition.x + moveAmount.x: beforePosition.x - moveAmount.x,
            beforePosition.y,
            beforePosition.z);
        //Debug.Log($"MoveX  {target.name}  Position  sx: {beforePosition.x,11:F5}  mx: {moveAmount.x,11:F5}  rx: {position.x,11:F5}");
        (target.transform as RectTransform).position = position;
        //Debug.Log($"MoveX  {target.name}  Position  x: {(target.transform as RectTransform).position.x,11:F5}  y: {(target.transform as RectTransform).position.y,11:F5}");
    }

    private void MoveY(GameObject target, Vector2 moveAmount, bool sign)
    {
        var beforePosition = positions[target];
        var position = new Vector3(
            beforePosition.x,
            sign ? beforePosition.y + moveAmount.y : beforePosition.y - moveAmount.y,
            beforePosition.z);
        //Debug.Log($"MoveY  {target.name}  Position  sy: {beforePosition.y,11:F5}  my: {moveAmount.y,11:F5}  ry: {position.y,11:F5}");
        (target.transform as RectTransform).position = position;
        //Debug.Log($"MoveY  {target.name}  Position  x: {(target.transform as RectTransform).position.x,11:F5}  y: {(target.transform as RectTransform).position.y,11:F5}");
    }

    private void MoveXY(GameObject target, Vector2 moveAmount, bool sign)
    {
        var beforePosition = positions[target];
        var position = new Vector3(
            sign ? beforePosition.x + moveAmount.x : beforePosition.x - moveAmount.x,
            sign ? beforePosition.y + moveAmount.y : beforePosition.y - moveAmount.y,
            beforePosition.z);
        //Debug.Log($"MoveXY  {target.name}  Position  sx: {beforePosition.x,11:F5}  mx: {moveAmount.x,11:F5}  rx: {position.x,11:F5}");
        //Debug.Log($"MoveXY  {target.name}  Position  sy: {beforePosition.y,11:F5}  my: {moveAmount.y,11:F5}  ry: {position.y,11:F5}");
        (target.transform as RectTransform).position = position;
        //Debug.Log($"MoveXY  {target.name}  Position  x: {(target.transform as RectTransform).position.x,11:F5}  y: {(target.transform as RectTransform).position.y,11:F5}");
    }

    private void MoveLocalX(GameObject target, Vector2 moveAmount, bool sign)
    {
        var beforePosition = localPositions[target];
        var position = new Vector3(
            sign ? beforePosition.x + moveAmount.x : beforePosition.x - moveAmount.x,
            beforePosition.y,
            beforePosition.z);
        //Debug.Log($"MoveX  {target.name}  Position  sx: {beforePosition.x,11:F5}  mx: {moveAmount.x,11:F5}  rx: {position.x,11:F5}");
        (target.transform as RectTransform).localPosition = position;
        //Debug.Log($"MoveX  {target.name}  Position  x: {(target.transform as RectTransform).position.x,11:F5}  y: {(target.transform as RectTransform).position.y,11:F5}");
    }

    private void MoveLocalY(GameObject target, Vector2 moveAmount, bool sign)
    {
        var beforePosition = localPositions[target];
        var position = new Vector3(
            beforePosition.x,
            sign ? beforePosition.y + moveAmount.y : beforePosition.y - moveAmount.y,
            beforePosition.z);
        //Debug.Log($"MoveY  {target.name}  Position  sy: {beforePosition.y,11:F5}  my: {moveAmount.y,11:F5}  ry: {position.y,11:F5}");
        (target.transform as RectTransform).localPosition = position;
        //Debug.Log($"MoveY  {target.name}  Position  x: {(target.transform as RectTransform).position.x,11:F5}  y: {(target.transform as RectTransform).position.y,11:F5}");
    }

    private void MoveLocalXY(GameObject target, Vector2 moveAmount, bool sign)
    {
        var beforePosition = localPositions[target];
        var position = new Vector3(
            sign ? beforePosition.x + moveAmount.x : beforePosition.x - moveAmount.x,
            sign ? beforePosition.y + moveAmount.y : beforePosition.y - moveAmount.y,
            beforePosition.z);
        //Debug.Log($"MoveXY  {target.name}  Position  sx: {beforePosition.x,11:F5}  mx: {moveAmount.x,11:F5}  rx: {position.x,11:F5}");
        //Debug.Log($"MoveXY  {target.name}  Position  sy: {beforePosition.y,11:F5}  my: {moveAmount.y,11:F5}  ry: {position.y,11:F5}");
        (target.transform as RectTransform).localPosition = position;
        //Debug.Log($"MoveXY  {target.name}  Position  x: {(target.transform as RectTransform).position.x,11:F5}  y: {(target.transform as RectTransform).position.y,11:F5}");
    }

    private void ResizeX(GameObject target, Vector2 moveAmount, bool sign)
    {
        var beforeSizeDelta = sizes[target];
        var sizeDelta = new Vector2(
            sign ? beforeSizeDelta.x + moveAmount.x : beforeSizeDelta.x - moveAmount.x,
            beforeSizeDelta.y);
        //Debug.Log($"ResizeX  {target.name}  Size  sx: {beforeSizeDelta.x,11:F5}  mx: {moveAmount.x,11:F5}  rx: {sizeDelta.x,11:F5}");
        (target.transform as RectTransform).sizeDelta = sizeDelta;
        //Debug.Log($"ResizeX  {target.name}  Size  x: {(target.transform as RectTransform).sizeDelta.x,11:F5}  y: {(target.transform as RectTransform).sizeDelta.y,11:F5}");
    }

    private void ResizeY(GameObject target, Vector2 moveAmount, bool sign)
    {
        var beforeSizeDelta = sizes[target];
        var sizeDelta = new Vector2(
            beforeSizeDelta.x,
            sign ? beforeSizeDelta.y - moveAmount.y : beforeSizeDelta.y + moveAmount.y);
        //Debug.Log($"ResizeY  {target.name}  Size  sy: {beforeSizeDelta.y,11:F5}  my: {moveAmount.y,11:F5}  ry: {sizeDelta.y,11:F5}");
        (target.transform as RectTransform).sizeDelta = sizeDelta;
        //Debug.Log($"ResizeY  {target.name}  Size  x: {(target.transform as RectTransform).sizeDelta.x,11:F5}  y: {(target.transform as RectTransform).sizeDelta.y,11:F5}");
    }

    private void ResizeXY(GameObject target, Vector2 moveAmount, bool sign)
    {
        var beforeSizeDelta = sizes[target];
        var sizeDelta = new Vector2(
            sign ? beforeSizeDelta.x + moveAmount.x : beforeSizeDelta.x - moveAmount.x,
            sign ? beforeSizeDelta.y - moveAmount.y : beforeSizeDelta.y + moveAmount.y);
        //Debug.Log($"ResizeXY  {target.name}  Size  sx: {beforeSizeDelta.x,11:F5}  mx: {moveAmount.x,11:F5}  rx: {sizeDelta.x,11:F5}");
        //Debug.Log($"ResizeXY  {target.name}  Size  sy: {beforeSizeDelta.y,11:F5}  my: {moveAmount.y,11:F5}  ry: {sizeDelta.y,11:F5}");
        (target.transform as RectTransform).sizeDelta = sizeDelta;
        //Debug.Log($"ResizeXY  {target.name}  Size  x: {(target.transform as RectTransform).sizeDelta.x,11:F5}  y: {(target.transform as RectTransform).sizeDelta.y,11:F5}");
    }
}
