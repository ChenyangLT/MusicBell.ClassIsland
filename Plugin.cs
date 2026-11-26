using ClassIsland.Core.Abstractions;
using ClassIsland.Core.Attributes;
using ClassIsland.Core.Extensions.Registry;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MusicBell.ClassIsland;

/// <summary>
/// MusicBell（点歌铃）与 ClassIsland 的桥。
/// 把「到这个时间了，做点什么」翻译成对 MusicBell 的调用：放一首、放下一首、停、暂停、开客户端、关客户端。
/// </summary>
[PluginEntrance]
public class Plugin : PluginBase
{
    public override void Initialize(HostBuilderContext context, IServiceCollection services)
    {
        services.AddAction<PlaySongAction, PlaySongActionSettingsControl>();
        services.AddAction<PlayNextAction, PlayNextActionSettingsControl>();
        services.AddAction<CloseMusicAction, CloseMusicActionSettingsControl>();
        services.AddAction<OpenMusicAction, OpenMusicActionSettingsControl>();
        services.AddAction<StopPlaybackAction, StopPlaybackActionSettingsControl>();
        services.AddAction<PausePlaybackAction, PausePlaybackActionSettingsControl>();
    }
}
