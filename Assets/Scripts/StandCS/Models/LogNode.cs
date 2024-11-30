using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public List<int> ConnectionNodes = new();
    public List<int> ConnectionLines = new(); // 仅记录发起的线？ 收到的线可以另外放

    public int GroupId;

    /// <summary>
    /// 是否已经被发现了 The discovered status of the node
    /// </summary>
    public bool IsDiscovered;

    /// <summary>
    /// 日志节点在面板中的坐标
    /// </summary>
    public Vector3 Position;
    
    /// <summary>
    /// 日志节点的缩放比例
    /// </summary>
    public float Scale;

    // 是否是默认显示的节点
    public bool IsRoot;

    [JsonConverter(typeof(ColorJsonConverter))]
    public Color Color = new(181.0f / 255, 122.0f / 255,80f / 255, 1);
    
}

public class LogCirticalMessage
{
    // 是否需要不同的触发点？
   public bool isNew;
}

//重写的JsonConverter<Color>基类中的抽象方法
public class ColorJsonConverter : JsonConverter<Color>
{
    public override void WriteJson(JsonWriter writer, Color value, 
        JsonSerializer serializer)
    {
        JObject colorObject = new JObject();
        colorObject.Add("r", value.r);
        colorObject.Add("g", value.g);
        colorObject.Add("b", value.b);
        colorObject.Add("a", value.a);
        colorObject.WriteTo(writer);
    }
 
    public override Color ReadJson(JsonReader reader, System.Type objectType, 
        Color existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject colorObject = JObject.Load(reader);
        float r = colorObject.GetValue("r").ToObject<float>();
        float g = colorObject.GetValue("g").ToObject<float>();
        float b = colorObject.GetValue("b").ToObject<float>();
        float a = colorObject.GetValue("a").ToObject<float>();
        return new Color(r, g, b, a);
    }
}