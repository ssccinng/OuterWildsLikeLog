using UnityEngine;
public class LoglineGO : BaseObject
{
    public LogLine logline;
    public LogPanelGO node1, node2;
    LogLineScript logLineScript;
    public LoglineGO()
    {
        GameObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/LogLineTemplate"));
        rectTransform = GameObject.GetComponent<RectTransform>();
        GameObject.transform.SetParent(UIManager.Instance.LineHud.transform);  
        logLineScript = GameObject.GetComponent<LogLineScript>();
        logLineScript.loglineGO = this; // 这个有办法通用化吗
    }

    RectTransform rectTransform;



    public void BindLogNodes(LogPanelGO node1, LogPanelGO node2)
    {
        // 在这里实现连线逻辑
        // 例如，可以使用LineRenderer组件来绘制连线

        // 1. 获取两个节点的位置

        this.node1 = node1;
        this.node2 = node2;

        SyncNode();
         


    } 


    // 副作用到底算了（？
    public void SyncNode() {
         LogNode logNode1 = node1.GetLogNode();
        LogNode logNode2 = node2.GetLogNode();

        // 2. 计算连线的起始点和终点
        var data = LogHelper.GetLineImageInfo(logNode1, logNode2);


        // 3. 设置连线的位置和宽度
        rectTransform.localPosition = new Vector3(data.Position.X, data.Position.Y, 0);
        rectTransform.sizeDelta = new Vector2(data.Width, 10);

        // 4. 设置连线的旋转角度

        rectTransform.localRotation = Quaternion.Euler(0, 0, data.RotationZ);
    }



}