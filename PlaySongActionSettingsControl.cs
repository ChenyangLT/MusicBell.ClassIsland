using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.VisualTree;
using ClassIsland.Core.Abstractions.Controls;

namespace MusicBell.ClassIsland;

/// <summary>
/// 行动设置界面。刻意用纯代码搭界面：插件里少一层 Avalonia XAML 编译，
/// 换 ClassIsland 版本时不容易因为加载器差异整个行动面板白掉。
/// </summary>
public class PlaySongActionSettingsControl : ActionSettingsControlBase<PlaySongActionSettings>
{
    private readonly TextBox _query;
    private readonly TextBox _label;
    private readonly TextBox _songId;
    private readonly TextBox _exe;
    private bool _ready;

    public PlaySongActionSettingsControl()
    {
        var root = new StackPanel { Spacing = 10 };

        root.Children.Add(Hint("歌名：写给网易云搜索框的字，建议「歌手 歌名」。"));
        _query = new TextBox { Watermark = "周杰伦 晴天", MinWidth = 260 };
        _query.TextChanged += (_, _) => { if (_ready) { Settings.Query = _query.Text ?? ""; Rename(); } };
        root.Children.Add(_query);

        root.Children.Add(Hint("呼出时显示的名字（可留空，默认用歌名）。"));
        _label = new TextBox { Watermark = "课前一歌", MinWidth = 260 };
        _label.TextChanged += (_, _) => { if (_ready) { Settings.Label = _label.Text ?? ""; Rename(); } };
        root.Children.Add(_label);

        root.Children.Add(Hint("网易云歌曲 ID（可留空；填了会直接播这一首，不再走搜索框）。"));
        _songId = new TextBox { Watermark = "186016", MinWidth = 260 };
        _songId.TextChanged += (_, _) => { if (_ready) Settings.SongId = ParseId(_songId.Text); };
        root.Children.Add(_songId);

        root.Children.Add(Hint("MusicBell.exe 路径（可选兜底；协议没注册时用它直接拉起）。"));
        _exe = new TextBox { Watermark = "D:\\Apps\\MusicBell\\MusicBell.exe", MinWidth = 260 };
        _exe.TextChanged += (_, _) => { if (_ready) Settings.ExePath = _exe.Text ?? ""; };
        root.Children.Add(_exe);

        Content = root;
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        _ready = false;
        _query.Text = Settings.Query;
        _label.Text = Settings.Label;
        _songId.Text = Settings.SongId?.ToString() ?? "";
        _exe.Text = Settings.ExePath;
        _ready = true;
        Rename();
    }

    private void Rename()
    {
        var name = string.IsNullOrWhiteSpace(Settings.Label) ? Settings.Query : Settings.Label;
        if (!string.IsNullOrWhiteSpace(name)) ChangeActionName($"MusicBell 放歌：{name}");
    }

    private static long? ParseId(string? text)
        => long.TryParse((text ?? "").Trim(), out var id) && id > 0 ? id : null;

    private static Control Hint(string text) => new TextBlock
    {
        Text = text,
        FontSize = 12,
        Opacity = 0.7,
        TextWrapping = TextWrapping.Wrap,
        VerticalAlignment = VerticalAlignment.Center
    };
}
