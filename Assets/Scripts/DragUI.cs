using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour, IPointerClickHandler,
    IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    bool gx = false;

    private DateTime _lastLeftClickTime = DateTime.Now;
    public event Action<PointerEventData> LeftClick, RightClick, DoubleLeftClick, Click;
    public void OnPointerClick(PointerEventData eventData)
    {

    }
    private Vector2 offset;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {


            UIManager.Instance.IsDraggingSpaceShipLog = false;
            gx = true;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out offset
        );
        }

    }
    RectTransform rectTransform;
    private void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();

    }

    private void Update()
    {


        if (gx)
        {

            Vector2 mousePosition = Input.mousePosition;

            // 将鼠标位置转换为 UI 元素的局部位置
            Vector2 localPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform.parent.GetComponent<RectTransform>(),
                mousePosition,
                null, // Camera 参数，传 null 代表使用屏幕坐标
                out localPosition
            );

            // 设置 UI 元素的 anchoredPosition
            rectTransform.anchoredPosition = localPosition - offset;

        }

    }

    bool resize = false;

    public void OnPointerExit(PointerEventData eventData)
    {
        resize = false;
        //throw new System.NotImplementedException();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        resize = true;

        //throw new System.NotImplementedException();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        gx = false;
        UIManager.Instance.IsDraggingSpaceShipLog = true;


    }
    private void ChoosePivot()
    {
        float width = Screen.width / 2;
        float height = Screen.height / 2;
        if (Input.mousePosition.x < width)
        {
            gameObject.GetComponent<RectTransform>().pivot
                = new Vector2(0, gameObject.GetComponent<RectTransform>().pivot.y);
        }
        else
        {
            gameObject.GetComponent<RectTransform>().pivot = new Vector2(1, gameObject.GetComponent<RectTransform>().pivot.y);
        }
        if (Input.mousePosition.y < height)
        {
            gameObject.GetComponent<RectTransform>().pivot = new Vector2(gameObject.GetComponent<RectTransform>().pivot.x, 0);
        }
        else
        {
            gameObject.GetComponent<RectTransform>().pivot = new Vector2(gameObject.GetComponent<RectTransform>().pivot.x, 1);
        }
    }
}
