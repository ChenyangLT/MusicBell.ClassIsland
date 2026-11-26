using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.VisualTree;
using ClassIsland.Core.Abstractions.Controls;

namespace MusicBell.ClassIsland;

/// <summary>简单行动的设置面板：一句说明 + 可选 exe 路径。</summary>
public abstract class SimpleActionSettingsControlBase : ActionSettingsControlBase<SimpleActionSettings>
{
    private readonly TextBox _exe;
    private bool _ready;

    protected abstract string Hint { get; }

    protected SimpleActionSettingsControlBase()
    {
        var root = new StackPanel { Spacing = 10 };
        root.Children.Add(new TextBlock
        {
            Text = Hint,
            FontSize = 12,
            Opacity = 0.7,
            TextWrapping = TextWrapping.Wrap
        });
        root.Children.Add(new TextBlock
        {
            Text = "MusicBell.exe 路径（可选兜底；协议没注册时用它直接拉起）。",
            FontSize = 12,
            Opacity = 0.7,
            TextWrapping = TextWrapping.Wrap
        });
        _exe = new TextBox { Watermark = "D:\\Apps\\MusicBell\\MusicBell.exe", MinWidth = 260 };
        _exe.TextChanged += (_, _) => { if (_ready) Settings.ExePath = _exe.Text ?? ""; };
        root.Children.Add(_exe);
        Content = root;
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        _ready = false;
        _exe.Text = Settings.ExePath;
        _ready = true;
    }
}

public class CloseMusicActionSettingsControl : SimpleActionSettingsControlBase
{
    protected override string Hint => "直接结束网易云音乐客户端（连同内置播放器一起停），上课、下课时很省事。";
}

public class OpenMusicActionSettingsControl : SimpleActionSettingsControlBase
{
    protected override string Hint => "把网易云音乐打开并叫到前台，不搜索、不放歌。";
}

public class StopPlaybackActionSettingsControl : SimpleActionSettingsControlBase
{
    protected override string Hint => "只停掉 MusicBell 内置播放器，网易云客户端不受影响。";
}

public class PausePlaybackActionSettingsControl : SimpleActionSettingsControlBase
{
    protected override string Hint => "内置播放器在放歌时按一下暂停，再按一下继续。";
}
