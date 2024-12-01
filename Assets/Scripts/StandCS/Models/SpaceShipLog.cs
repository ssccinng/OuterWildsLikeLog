using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.Mathematics;

using UnityEngine;
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
    public int _uniqueId = 0;
    public int GetUniqueId() => Interlocked.Increment(ref _uniqueId);
    public void ResetUniqueId() => _uniqueId = 0;
    // 日志组记录
    public List<LogGroup> LogGroups = new();




    // 下面是动态生成出来的数据

    public List<LogNode> LogNodes = new();

    // 日志线记录
    public List<LogLine> LogLines = new();
}

public static class LogHelper
{


    public static LineInfo GetLinePosition(LogNode startNode, LogNode endNode)
    {
        return new LineInfo
        {
            Start = startNode.Position,
            End = endNode.Position,
        };
    }
    // todo: 还需要计算箭头在哪
    public static LineImageInfo GetLineImageInfo(Vector2 start, Vector2 end)
    {
        return new LineImageInfo
        {
            Position = start,
            Width = Vector2.Distance(start, end),


        };

    }
    public static LineImageInfo GetLineImageInfo(LogNode start, LogNode end)
    {
        // UnityEngine.Debug.Log($"start: {start.Position}, end: {end.Position}");
        //  var dd = -math.atan2(end.Position.x - start.Position.x, end.Position.y - start.Position.y) * 180 / math.PI;
        return new LineImageInfo
        {
            //  Position = start,
            //  Width = Vector2.Distance(start, end),

            Position = start.Position,
            Width = Vector2.Distance(start.Position, end.Position),
            // RotationZ = math.atan2(end.Position.x - start.Position.x, end.Position.y - start.Position.y) * 180 / math.PI,
            RotationZ = (-math.atan2(end.Position.x - start.Position.x, end.Position.y - start.Position.y) * 180) / math.PI + 90,

        };

    }


}