using UnityEngine;
using UnityEngine.UI;

using Newtonsoft.Json;
using TMPro;
using System;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }


    public GameObject SpaceShipLog;
    public RectTransform RectTransformSpace;
    public GameObject SpaceShipLogScale;
    public GameObject LogPanelHud;
    public GameObject LineHud;
    public Animator animator;

    public LogEditScript LogEditScript;

    // public GameManager LogPanel;
    public TextMeshProUGUI LogTitleInputField;

    public bool IsDraggingSpaceShipLog = true;


    #region UI组件
    public Button AddLogButton;
    public Button SaveButton;
    public Button LoadButton;
    public Button TogglePanelButton;

    public Button HeroToZeroButton;
    public Button AllShowButton;
    #endregion


    #region 运行模式
    public Toggle DragMode;
    public Toggle LineMode;
    public Toggle LookMode;

    #endregion


    bool panelExpanded = true;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public UnityAction<bool> CheckModeChange(SystemMode mode)
    {
        return mode switch
        {
            SystemMode.LookMode => (b) =>
            {
                if (b)
                {
                    SystemStateManager.SystemMode = mode; DragMode.isOn = LineMode.isOn = false;

                }
                // LookMode.isOn = true;
            }
            ,
            SystemMode.LineMode => (b) =>
            {
                if (b)
                {
                    SystemStateManager.SystemMode = mode; DragMode.isOn = LookMode.isOn = false;
                }
                // LineMode.isOn = true;
            }
            ,
            _ => (b) =>
            {
                if (b)
                {
                    SystemStateManager.SystemMode = mode; LineMode.isOn = LookMode.isOn = false;
                }
                // DragMode.isOn = true;
            }
            ,
        };
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpaceShipLoggerSystem.GetSaves();
        SpaceShipLoggerSystem.Create();
        RectTransformSpace = SpaceShipLog.GetComponent<RectTransform>();
        // animator.SetTrigger("SidePanelInTrigger");
        AddLogButton.onClick.AddListener(() =>
        {
            SpaceShipLoggerSystem.CreateNode(LogTitleInputField.text);
            // Save the log
            // var logNode = new LogNode
            // {
            //     Name = LogTitleInputField.text,
            //     Description = "This is a node",
            //     // Connections = new System.Collections.Generic.List<LogNode>(),
            //     IsDiscovered = false,
            //     Position = new (0, 0)
            // };

            // new LogNodeGO().Bind(logNode).SetTitle(LogTitleInputField.text);

            // var json = JsonConvert.SerializeObject(logNode);
            // Debug.Log(json);
        });

        SaveButton.onClick.AddListener(() =>
        {
            SpaceShipLoggerSystem.Save("test");
        }
        );

        LoadButton.onClick.AddListener(() =>
        {
            SpaceShipLoggerSystem.Load("test");
        }
        );

        TogglePanelButton.onClick.AddListener(() =>
        {
            ToggleSidePanel();
        });

        HeroToZeroButton.onClick.AddListener(() =>
        {
            SpaceShipLoggerSystem.HeroToZero();
        });

        AllShowButton.onClick.AddListener(() =>
        {
            SpaceShipLoggerSystem.AllShow();
        });

        DragMode.onValueChanged.AddListener(CheckModeChange(SystemMode.DragMode));
        LineMode.onValueChanged.AddListener(CheckModeChange(SystemMode.LineMode));
        LookMode.onValueChanged.AddListener(CheckModeChange(SystemMode.LookMode));
    }

    private void ToggleSidePanel()
    {
        if (panelExpanded)
        {
            animator.SetTrigger("SideOut");
            panelExpanded = false;
        }
        else
        {
            animator.SetTrigger("SideIn");
            panelExpanded = true;
        }
    }

    private void SetSidePanel(bool expanded)
    {
        if (expanded != panelExpanded)
        {
            ToggleSidePanel();
        }

    }

    // Update is called once per frame
    void Update()
    {
        DragSpaceShipLog();

    }


    void DragSpaceShipLog()
    {
        if (IsDraggingSpaceShipLog)
        {
            // when mouse is clicked and dragged on the screen, move the spaceship log

            if (Input.GetMouseButton(0))
            {
                var spacePosition = SpaceShipLog.transform.position;
                // move the spaceship log , delta is the difference mouse position in the current frame and the previous frame
                SpaceShipLog.transform.position = new Vector3(spacePosition.x + Input.GetAxis("Mouse X") * 20, spacePosition.y + Input.GetAxis("Mouse Y") * 20, 0);
            }

            // when mouse wheel is scrolled, change SpaceShipLog scale
            if (Input.mouseScrollDelta.y != 0)
            {
                var scale = SpaceShipLogScale.transform.localScale;
                var mutilplier = 1 + Input.mouseScrollDelta.y / 10;
                Vector3 GetNewScale(Vector3 scale, float mutilplier)
                {
                    Debug.Log("scale.x: " + scale.x);
                    Debug.Log("mutilplier: " + mutilplier);
                    if (mutilplier > 1)
                    {
                        if (scale.x > 4)
                        {
                            return Vector3.one * 4;
                        }
                    }
                    else
                    { // mutilplier < 1
                        if (scale.x < 0.3f)
                        {
                            return Vector3.one * 0.3f;
                        }
                    }
                    return new Vector3(scale.x * mutilplier, scale.y * mutilplier, scale.z);
                }

                var newScale = GetNewScale(scale, mutilplier);
                LeanTween.scale(SpaceShipLogScale, newScale, 0.1f).setEase(LeanTweenType.easeOutQuad);

                // Vector3 mousePosition = Input.mousePosition;
                // Vector3 objectPosition = SpaceShipLog.transform.position;
                // Vector3 offset = objectPosition - Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.WorldToScreenPoint(objectPosition).z));

                // SpaceShipLog.transform.localScale = newScale;
            }

        }

    }

    public void SpaceShipFocusOnObj(GameObject obj)
    {
        var rectSpace = SpaceShipLogScale.GetComponent<RectTransform>();
        var rectObj = obj.GetComponent<RectTransform>();

        var target = -rectObj.localPosition;


        LeanTween.moveLocal(SpaceShipLog, target, 0.5f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.scale(SpaceShipLogScale, Vector3.one * 1.5f / rectObj.localScale.x, 0.5f).setEase(LeanTweenType.easeInCubic);

    }

    public void EditLog(LogNodeGO logNodeGO)
    {
        SpaceShipFocusOnObj(logNodeGO.LogPanel);
        LogEditScript.ShowLogEdit(logNodeGO);
        SetSidePanel(false);
    }

}
