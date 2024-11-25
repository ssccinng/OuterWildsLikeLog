using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.Mathematics;
public struct LineInfo
{
    public Vector2 Start, End;
}

public struct LineImageInfo
{
    public static readonly int Height = 10;
    public float Width;

    public Vector2 Position;
    public float RotationZ;

    /// <summary>
    /// 箭头所在位置
    /// </summary>
    public Vector2 ArrowPosition;
}

public class SpaceShipLog
{
    // 日志组记录
    public List<LogGroup> LogGroups = new();

    public List<LogNode> LogNodes = new();

    // 日志线记录
    public List<LogLine> LogLines = new();
}

public static class LogHelper
{

    public static void CreateNode(string name, string description)
    {
        // Create a new node
    }

    public static LogNode? GetNodeById(IEnumerable<LogNode> logNodes, int id) 
    {
        return logNodes.FirstOrDefault(s => s.Id == id);
    }

    public static LogLine? CreateLogLine(LogNode startNode, LogNode endNode)
    {
        // 照道理来说 这里不会有null? 或者有null也不该这么处理
        if (startNode is null || endNode is null)
        {
            return null;
        }
        return new LogLine
        {
            LogIdStart = startNode.Id,
            LogIdEnd = endNode.Id
        };
    }

    public static LineInfo GetLinePosition(LogNode startNode, LogNode endNode) 
    {
        return new LineInfo 
        {
            Start = startNode.Position,
            End = endNode.Position,
        };
    }
    // todo: 还需要计算箭头在哪
    public static LineImageInfo GetLineImageInfo(Vector2 start, Vector2 end) {
        return new LineImageInfo {
             Position = start,
             Width = Vector2.Distance(start, end),


        };

    }
public static LineImageInfo GetLineImageInfo(LogNode start, LogNode end) {
    UnityEngine.Debug.Log($"start: {start.Position}, end: {end.Position}");
        return new LineImageInfo {
            //  Position = start,
            //  Width = Vector2.Distance(start, end),

            Position = start.Position,
            Width = Vector2.Distance(start.Position, end.Position),
            RotationZ = math.atan2(end.Position.Y - start.Position.Y, end.Position.X - start.Position.X) * 180 / math.PI,

             
        };

    }
    

}