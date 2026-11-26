using System.Threading.Tasks;
using ClassIsland.Core.Abstractions.Automation;
using ClassIsland.Core.Attributes;

namespace MusicBell.ClassIsland;

/// <summary>关掉网易云音乐（同时停掉内置播放器）。</summary>
[ActionInfo("musicbell.close", "MusicBell 关闭网易云音乐", "\uE711", true, "音乐铃")]
public class CloseMusicAction : ActionBase<SimpleActionSettings>
{
    protected override async Task OnInvoke()
    {
        await base.OnInvoke();
        MusicBellLauncher.CloseMusic(Settings.ExePath);
    }
}

/// <summary>打开 / 叫出网易云音乐。</summary>
[ActionInfo("musicbell.open", "MusicBell 打开网易云音乐", "\uE8A7", true, "音乐铃")]
public class OpenMusicAction : ActionBase<SimpleActionSettings>
{
    protected override async Task OnInvoke()
    {
        await base.OnInvoke();
        MusicBellLauncher.OpenMusic(Settings.ExePath);
    }
}

/// <summary>停止内置播放器。</summary>
[ActionInfo("musicbell.stop", "MusicBell 停止播放", "\uE71A", true, "音乐铃")]
public class StopPlaybackAction : ActionBase<SimpleActionSettings>
{
    protected override async Task OnInvoke()
    {
        await base.OnInvoke();
        MusicBellLauncher.Stop(Settings.ExePath);
    }
}

/// <summary>播放 / 暂停切换。</summary>
[ActionInfo("musicbell.pause", "MusicBell 播放 / 暂停", "\uE769", true, "音乐铃")]
public class PausePlaybackAction : ActionBase<SimpleActionSettings>
{
    protected override async Task OnInvoke()
    {
        await base.OnInvoke();
        MusicBellLauncher.Pause(Settings.ExePath);
    }
}
