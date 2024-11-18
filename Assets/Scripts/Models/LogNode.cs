using System.Collections.Generic;

using UnityEngine;

public class LogNode
{
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
    public System.Numerics.Vector2 Position;
    
}

