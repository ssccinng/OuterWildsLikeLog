
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class BaseObject
{
    public GameObject GameObject;

    public virtual void Destroy()
    {
        GameObject.Destroy(GameObject);
    }
}

// public class BindAbleObject<T>: BaseObject
// {
//     public T Data;

//     public void Bind(T data) 
// }


public class LogNodeGO : BaseObject
{
    // 给我乖乖听话 自己去注册一下
    public GameObject LogPanel;
    public LogMono LogMono;

    public LogNode LogNode; // 别人不许乱动

    RectTransform rectTransform;

    public void SetTitle(string text)
    {
        LogMono.LogTitle.text = text;
        LogNode.Name = text;

    }
    public void SetScale(float scale)
    {
        Debug.Log("SetScale" + scale);
        rectTransform.localScale = new Vector3(scale, scale, 1);
        LogNode.Scale = scale;
    }

    public void SetColor(Color color)
    {
        LogMono.backGroundImage.color = color;
        LogNode.Color = color;
    }


    public LogNode GetLogNode()
    {
        LogMono.UpdateInfo(); // lazy撕烤
        return LogNode;
    }
    public LogNodeGO()
    {
        GameObject = LogPanel = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/LogTemplate"));
        LogMono = LogPanel.GetComponent<LogMono>();



        rectTransform = LogPanel.GetComponent<RectTransform>();

        LogPanel.transform.SetParent(UIManager.Instance.LogPanelHud.transform);

        LogPanel.transform.localPosition = new Vector3(0, 0, 0);

        LogMono.LogNodeGO = this;


    }

    public void WhenEnable()
    {
        GameObject.SetActive(true);
        UIManager.Instance.SpaceShipFocusOnObj(LogPanel);
        LogUIAnimator.AnimateScaleIn(GameObject, 0.5f);
    }


   

    public LogNodeGO Bind(LogNode logNode)
    {
        LogNode = logNode;

        SetTitle(logNode.Name);

        LogPanel.transform.localPosition = logNode.Position;
        // Task.Run(async () =>
        // {
        //     await Task.Delay(100);
        //            SetScale(logNode.Scale);

        // });

        SetColor(logNode.Color);
                   SetScale(logNode.Scale);
        // LogUIAnimator.AnimateScaleIn(GameObject, 0.5f);
    
        return this;
    }

}