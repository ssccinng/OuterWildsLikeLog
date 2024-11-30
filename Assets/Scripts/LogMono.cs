using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LogMono : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    /// <summary>
    /// 是否可以被拖动
    /// </summary>
    public bool CanDrag;
    
    public Button LogMainButton;
    public Animator animator; 
    public TextMeshProUGUI LogTitle;

    public Image backGroundImage;
    public Image InfoImage;
    public Outline backOutline;
    public Outline selectedOutline;

    public bool Selected = false;

    public LogNodeGO LogNodeGO;

    Sprite ok ;
    Sprite ques ;


    RectTransform rectTransform;


    private static List<LogMono> selectedLogs = new List<LogMono>();

    // public GameObject textMesh;
    public void OnPointerClick(PointerEventData eventData)
    {
        // Debug.Log("OnPointerClick");
          if (SystemStateManager.SystemMode == SystemMode.LineMode)
        {
            Selected = !Selected;
            if (Selected)
            {
                selectedOutline.enabled = true;
                selectedLogs.Add(this);
            }
            else
            {
                selectedOutline.enabled = false;
                selectedLogs.Remove(this);
            }

            // 如果选中了两个日志UI，进行连线
            if (selectedLogs.Count == 2)
            {
                DrawLineBetweenLogs(selectedLogs[0], selectedLogs[1]);
                // 重置选中状态
                selectedLogs[0].ResetSelected();
                selectedLogs[1].ResetSelected();
                selectedLogs.Clear();
            }
        }
       

    }

    private void DrawLineBetweenLogs(LogMono log1, LogMono log2)
    {
        // 在这里实现连线逻辑
        // 例如，可以使用LineRenderer组件来绘制连线

        SpaceShipLoggerSystem.CreateLine(log1.LogNodeGO, log2.LogNodeGO);

        // var line = new LogLineGO();

        // line.BindLogNodes(log1.LoglineGO, log2.LoglineGO);
        
    }

    public void ResetSelected()
    {
        Selected = false;
        selectedOutline.enabled = false;

      
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        backOutline.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        backOutline.enabled = false;

    }
    bool isUnkown = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ok = Resources.Load<Sprite>("Images/trueImg");
        ques= Resources.Load<Sprite>("Images/Unknown");
        LogUIAnimator.AnimateScaleIn(gameObject, 0.5f);

        rectTransform = gameObject.GetComponent<RectTransform>();

        LogMainButton.onClick.AddListener(async () =>
        {
            // LogUIAnimator.AnimateScaleIn(gameObject, 0.5f);

            UIManager.Instance.EditLog(LogNodeGO);

            // animator.SetTrigger("ImageOut");
            // await Task.Delay(500);
            // if (!isUnkown)
            // {
            //     InfoImage.sprite = ques;
            //     isUnkown = true;
            // }
            // else
            // {
            //     InfoImage.sprite = ok;
            //     isUnkown = false;
            // }
            // animator.SetTrigger("ImageIn");
        });
        var outlines = backGroundImage.GetComponents<Outline>();
        backOutline = outlines[0];
        selectedOutline = outlines[1];



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInfo() {

        LogNodeGO.LogNode.Position = new (rectTransform.localPosition.x, rectTransform.localPosition.y);
        
    }

    private void FocusOnNode()
    {
        

        // // 获取相机
        // Camera mainCamera = Camera.main;
        // if (mainCamera != null)
        // {
        //     // 将相机移动到节点位置
        //     Vector3 targetPosition = new Vector3(rectTransform.position.x, rectTransform.position.y, mainCamera.transform.position.z);
        //     mainCamera.transform.position = targetPosition;
        // }
    }
}
