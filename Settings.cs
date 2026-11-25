using System.Text.Json.Serialization;

namespace MusicBell.ClassIsland;

public class PlaySongActionSettings
{
    /// <summary>交给网易云搜索框的文字，建议「歌手 歌名」。</summary>
    public string Query { get; set; } = "";

    /// <summary>呼出时显示的排歌名（可留空，默认用歌名）。</summary>
    public string Label { get; set; } = "";

    /// <summary>已知网易云歌曲 ID 时可直接跳到那首歌。</summary>
    public long? SongId { get; set; }

    /// <summary>可选：MusicBell.exe 的完整路径，协议未注册时用它兜底。</summary>
    public string ExePath { get; set; } = "";
}

public class PlayNextActionSettings
{
    public string ExePath { get; set; } = "";
}
