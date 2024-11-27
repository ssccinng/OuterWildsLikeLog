
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

        LogMono.LoglineGO = this;


    }

    public LogNodeGO Bind(LogNode logNode)
    {
        LogNode = logNode;

        SetTitle(logNode.Name);

        LogPanel.transform.localPosition = logNode.Position;

        rectTransform.localScale = new Vector3(logNode.Scale, logNode.Scale, 1);
    
        return this;
    }

}