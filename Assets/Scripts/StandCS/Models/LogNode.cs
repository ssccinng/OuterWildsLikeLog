using System.Collections.Generic;

using UnityEngine;

// 还有颜色，或是分组

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
    /// 是否已经被发现了 The discovered status of the node
    /// </summary>
    public bool IsDiscovered;

    /// <summary>
    /// 日志节点在面板中的坐标
    /// </summary>
    public System.Numerics.Vector2 Position;
    
    /// <summary>
    /// 日志节点的缩放比例
    /// </summary>
    public float Scale;
    
}

public class LogCirticalMessage
{
    // 是否需要不同的触发点？
   public bool isNew;
}

