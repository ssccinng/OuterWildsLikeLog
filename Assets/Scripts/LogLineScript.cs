using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LogLineScript : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    bool selected = false;
    public LogLineGO loglineGO;
    public Outline selectedOutline;
    public Outline backOutline;
    public Animator animator;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (SystemStateManager.SystemMode == SystemMode.LineMode)
        {
            selected = !selected;
            if (selected)
            {
                // 选中
                // 选中的时候，显示选中的效果
                // 选中的时候，将选中的日志UI加入到一个列表中

                selectedOutline.enabled = true;

            }
            else
            {
                // 取消选中
                // 取消选中的时候，取消选中的效果
                // 取消选中的时候，将取消选中的日志UI从列表中移除
                selectedOutline.enabled = false;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();
        backOutline.enabled = true;
        LeanTween.scale(gameObject, new Vector3(1f, 2f, 1f), 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();
        backOutline.enabled = false;
        LeanTween.scale(gameObject, new Vector3(1.0f, 1.0f, 1f), 0.1f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var outlines = GetComponents<Outline>();
        selectedOutline = outlines[0];
        backOutline = outlines[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            // 如果按下Delete键，删除连线
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                // 删除连线

                SpaceShipLoggerSystem.DeleteLine(loglineGO);
            }
        }

        // 同步线位姿 丹需要撕烤需要实时吗，大部分时间不用吧
        if (SystemStateManager.SystemMode == SystemMode.DragMode)
        {
            // Debug.Log("SyncNode");
            loglineGO.SyncNode();
        }
    }

    public void MuteAmni()
    {
        animator.enabled = false;
    }
}
