using System.Collections.Generic;
using UnityEngine;
public class LogLineGO : BaseObject
{
    public LogLine Logline;
    public LogNodeGO node1, node2;
    LogLineScript logLineScript;
    public LogLineGO()
    {
        GameObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/LogLineTemplate"));
        rectTransform = GameObject.GetComponent<RectTransform>();
        GameObject.transform.SetParent(UIManager.Instance.LineHud.transform);
        logLineScript = GameObject.GetComponent<LogLineScript>();
        logLineScript.loglineGO = this; // 这个有办法通用化吗
    }

    RectTransform rectTransform;
    public void BindLogNodes(LogNodeGO node1, LogNodeGO node2)
    {
        // 在这里实现连线逻辑
        // 例如，可以使用LineRenderer组件来绘制连线

        // 1. 获取两个节点的位置

        this.node1 = node1;
        this.node2 = node2;

        // SyncNode();



    }


    // 副作用到底算了（？
    public void SyncNode()
    {
        LogNode logNode1 = node1.GetLogNode();
        LogNode logNode2 = node2.GetLogNode();

        // 2. 计算连线的起始点和终点
        var data = LogHelper.GetLineImageInfo(logNode1, logNode2);


        // 3. 设置连线的位置和宽度
        rectTransform.localPosition = data.Position;
        rectTransform.sizeDelta = new Vector2(data.Width, 10);

        // 4. 设置连线的旋转角度

        rectTransform.localRotation = Quaternion.Euler(0, 0, data.RotationZ);
    }

    public LogLineGO Bind(List<LogNodeGO> logNodes, LogLine logline)
    {
        Logline = logline;

        var func = SpaceShipLoggerSystem.GetLogNodeGOById(logNodes);
        // 柯里化和纯洁性密不可分
        BindLogNodes(func(logline.LogIdStart), func(logline.LogIdEnd));

        return this;
    }

    public void WhenEnable()
    {
        GameObject.SetActive(true);
        logLineScript.animator.enabled = true;
        // logLineScript.animator.SetTrigger("Show");
    }


}