
// 还是要以这个为主 管理层
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public static class SpaceShipLoggerSystem
{
    // 现在显示的
    public static SpaceShipLog SpaceShipLog;
    // public static void CreateNode(string name, string description)
    // {
    //     // Create a new node
    // }

    public static List<LogNodeGO> LogNodesGOs = new();
    public static List<LogLineGO> LogLineGOs = new();

    public static LogNodeGO CreateNode(string name)
    {
        var node = new LogNode
        {
            Id = SpaceShipLog.GetUniqueId(),
            Name = name,
            Description = "This is a node",
            // Connections = new System.Collections.Generic.List<LogNode>(),
            IsDiscovered = false,
            Position = -UIManager.Instance.RectTransformSpace.localPosition,
            Scale = 1,
        };

        SpaceShipLog.LogNodes.Add(node);
        var logNodeGO = new LogNodeGO().Bind(node);
        LogNodesGOs.Add(logNodeGO);
        return logNodeGO;
    }

    public static LogLineGO CreateLine(LogNodeGO node1, LogNodeGO node2)
    {
        var logLine = new LogLine
        {
            Id = SpaceShipLog.GetUniqueId(),
            LogIdStart = node1.LogNode.Id,
            LogIdEnd = node2.LogNode.Id
        };

        node1.LogNode.ConnectionLines.Add(logLine.Id);

        node1.LogNode.ConnectionNodes.Add(node2.LogNode.Id);

        SpaceShipLog.LogLines.Add(logLine);
        var logLineGO = new LogLineGO().Bind(LogNodesGOs, logLine);
        LogLineGOs.Add(logLineGO);
        logLineGO.SyncNode();// 可能要考虑协程
        return logLineGO;
    }


    public static void DeleteNode(LogNodeGO logNodeGO)
    {
        SpaceShipLog.LogNodes.Remove(logNodeGO.GetLogNode());
        LogNodesGOs.Remove(logNodeGO);
        logNodeGO.Destroy();
    }

    public static void DeleteLine(LogLineGO logLineGO)
    {
        SpaceShipLog.LogLines.Remove(logLineGO.Logline);
        LogLineGOs.Remove(logLineGO);
        logLineGO.Destroy();
    }

    public static LogLine GetLogLineById(List<LogLine> logLines, int id)
    {
        return logLines.Find(s => s.Id == id);
    }

    public static LogNode GetLogNodeById(List<LogNode> logNodes, int id)
    {
        return logNodes.Find(s => s.Id == id);
    }

    public static LogGroup GetLogGroupById(List<LogGroup> logGroups, int id)
    {
        return logGroups.Find(s => s.Id == id);
    }

    public static Func<int, LogGroup> GetLogGroupById(List<LogGroup> logGroups) => (int id) =>
    {
        return logGroups.Find(s => s.Id == id);
    };

    // public static Func<int, LogLineGO> GetLogLineGOById(List<LogLineGO> logLineGOs)
    // {
    //     return id => logLineGOs.Find(s => s.Logline.Id == id);
    // }
    public static Func<int, LogLineGO> GetLogLineGOById(List<LogLineGO> logLineGOs) => (int id) =>
    {
        return logLineGOs.Find(s => s.Logline.Id == id);
    };

    public static LogLineGO GetLogLineGOById(List<LogLineGO> logLineGOs, int id)
    {
        return logLineGOs.Find(s => s.Logline.Id == id);
    }

    public static Func<int, LogNodeGO> GetLogNodeGOById(List<LogNodeGO> logNodeGOs) => (int id) =>
    {
        return logNodeGOs.Find(s => s.LogNode.Id == id);
    };
    public static LogNodeGO GetLogNodeGOById(List<LogNodeGO> logNodeGOs, int id)
    {
        return logNodeGOs.Find(s => s.LogNode.Id == id);
    }

    static string savePath = @".\SPSave";

    public static string[] GetSaves()
    {
        // 如果存档路径不存在
        if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);

        return Directory.GetFiles(savePath, "*.json");
    }

    public static void Load(string name)
    {
        if (SpaceShipLog != null)
        {
            // todo: 清空
            Clear();
        }
        SpaceShipLog = JsonConvert.DeserializeObject<SpaceShipLog>(File.ReadAllText(
            $"{savePath}/{name}.json"
            ), new JsonSerializerSettings() { ReferenceLoopHandling =  ReferenceLoopHandling.Ignore }
          );
        ApplyToGame(SpaceShipLog);

    }

    public static void Save(string name)
    {
LogNodesGOs.ForEach(s => s.LogMono.UpdateInfo());
        File.WriteAllText($"{savePath}/{name}.json",
         JsonConvert.SerializeObject(SpaceShipLog,
          new JsonSerializerSettings() { ReferenceLoopHandling =  ReferenceLoopHandling.Ignore }
          )
          
          );

    }
    // 创建一个新的
    public static void Create()
    {
        if (SpaceShipLog != null)
        {
            // todo: 清空
            Clear();
        }
        SpaceShipLog = new();
        ApplyToGame(SpaceShipLog);
    }

    public static void Clear() 
    {
        LogNodesGOs.ForEach(s => s.Destroy());
        LogLineGOs.ForEach(s => s.Destroy());
        LogNodesGOs.Clear();
        LogLineGOs.Clear();
    }


    public static void ApplyToGame(SpaceShipLog spaceShipLog)
    {
        foreach (var logNode in spaceShipLog.LogNodes)
        {
            LogNodesGOs.Add(new LogNodeGO().Bind(logNode));
        }
        // 延时一帧



        // 你不纯洁
        foreach (var logLine in spaceShipLog.LogLines)
        {
            LogLineGOs.Add(new LogLineGO().Bind(LogNodesGOs, logLine));
        }






    }

}