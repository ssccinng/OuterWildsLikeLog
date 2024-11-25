
// public class LogCirticalNode : LogNode
// {
//     /// <summary>
//     /// The critical value of the node
//     /// </summary>
//     public float CriticalValue;
// }

public record LogLine {
    // public System.Numerics.Vector2 Start, End;

    // Log的起点和终点节点
    public int LogIdStart, LogIdEnd;

    // 关联信息
    public string Message;
}

