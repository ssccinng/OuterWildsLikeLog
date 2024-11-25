
using TMPro;
using UnityEngine;

public class BaseObject
{
    public GameObject GameObject;

    public virtual void Dispose()
    {
        GameObject.Destroy(GameObject);
    }


}
public class LogPanelGO: BaseObject
{
    // 给我乖乖听话 自己去注册一下
    public GameObject LogPanel;
    public LogMono LogMono;

    public LogNode LogNode;

    public void SetTitle(string text)
    {
        LogMono.LogTitle.text = text;
    }
    public LogNode GetLogNode()
    {
        LogMono.UpdateInfo(); // lazy撕烤
        return LogNode;
    }
    public LogPanelGO()
    {
        LogPanel = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/LogTemplate"));
        LogMono = LogPanel.GetComponent<LogMono>();      


        LogPanel.transform.SetParent(UIManager.Instance.LogPanelHud.transform);  

        LogPanel.transform.localPosition = new Vector3(0, 0, 0);
        LogNode = new LogNode();
        LogMono.LoglineGO = this;
    }

}