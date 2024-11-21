using System.Collections.Generic;

using UnityEngine;

public class LogNode
{
    /// <summary>
    /// 节点ID
    /// </summary>
    public int Id;
    /// <summary>
    /// The name of the node
    /// </summary>
    public string Name;
    /// <summary>
    /// The description of the node
    /// </summary>
    public string Description;
    /// <summary>
    /// The connections of the node
    /// </summary>
    public List<LogNode> Connections;

    /// <summary>
    /// The discovered status of the node
    /// </summary>
    public bool IsDiscovered;

    /// <summary>
    /// 日志节点在面板中的坐标
    /// </summary>
    public System.Numerics.Vector2 Position;
    
}
public class LogLine {
    public System.Numerics.Vector2 Start, End;
}