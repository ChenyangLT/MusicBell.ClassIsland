using System.Threading.Tasks;
using ClassIsland.Core.Abstractions.Automation;
using ClassIsland.Core.Attributes;

namespace MusicBell.ClassIsland;

/// <summary>把歌名交给 MusicBell：它会打开（或唤醒）网易云，在搜索框里找回这首歌。</summary>
[ActionInfo("musicbell.play", "MusicBell 放一首歌", "\uE8D6", true, "音乐铃")]
public class PlaySongAction : ActionBase<PlaySongActionSettings>
{
    protected override async Task OnInvoke()
    {
        await base.OnInvoke();
        MusicBellLauncher.Play(Settings.Query, Settings.Label, Settings.SongId, Settings.ExePath);
    }
}
