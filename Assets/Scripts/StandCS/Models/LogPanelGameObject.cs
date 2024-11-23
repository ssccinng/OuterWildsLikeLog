
using TMPro;
using UnityEngine;

public class BaseObject
{
    public GameObject GameObject;
}
public class LogPanelGameObject: BaseObject
{
    public GameObject LogPanel;
    public LogMono LogMono;

    public void SetTitle(string text)
    {
        LogMono.LogTitle.text = text;
    }

    public LogPanelGameObject()
    {
        LogPanel = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/LogTemplate"));
        LogMono = LogPanel.GetComponent<LogMono>();      


        LogPanel.transform.SetParent(UIManager.Instance.SpaceShipLog.transform);  

        LogPanel.transform.localPosition = new Vector3(0, 0, 0);
    }

}