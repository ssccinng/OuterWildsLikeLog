using System.Collections.Generic;
using System.Linq;
using System.Numerics;
public struct LineInfo
{
    public Vector2 Start, End;
}

public struct LineImageInfo
{
    public static readonly int Height = 10;
    public int Width;

    public Vector2 Position;
    public float RotationZ;
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

    

}