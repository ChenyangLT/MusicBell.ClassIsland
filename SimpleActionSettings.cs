using System.Text.Json.Serialization;

namespace MusicBell.ClassIsland;

/// <summary>只带一个可选兜底路径的行动设置（关闭/打开/停止/暂停都用它）。</summary>
public class SimpleActionSettings
{
    /// <summary>可选：MusicBell.exe 的完整路径，协议未注册时用它兜底。</summary>
    public string ExePath { get; set; } = "";
}
