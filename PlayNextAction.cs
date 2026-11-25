using System.Threading.Tasks;
using ClassIsland.Core.Abstractions.Automation;
using ClassIsland.Core.Attributes;

namespace MusicBell.ClassIsland;

/// <summary>让 MusicBell 立刻放「下一个排歌」——排歌在 MusicBell 里维护，行动只负责催一下。</summary>
[ActionInfo("musicbell.next", "MusicBell 放下一首排歌", "\uE893", true, "音乐铃")]
public class PlayNextAction : ActionBase<PlayNextActionSettings>
{
    protected override async Task OnInvoke()
    {
        await base.OnInvoke();
        MusicBellLauncher.PlayNext(Settings.ExePath);
    }
}
