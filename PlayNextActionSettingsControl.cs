using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.VisualTree;
using ClassIsland.Core.Abstractions.Controls;

namespace MusicBell.ClassIsland;

public class PlayNextActionSettingsControl : ActionSettingsControlBase<PlayNextActionSettings>
{
    private readonly TextBox _exe;
    private bool _ready;

    public PlayNextActionSettingsControl()
    {
        var root = new StackPanel { Spacing = 10 };
        root.Children.Add(new TextBlock
        {
            Text = "轮到 MusicBell 按自己的排歌表放下一首。行动本身不需要填歌名。",
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
