using ClassIsland.Core.Abstractions;
using ClassIsland.Core.Attributes;
using ClassIsland.Core.Extensions.Registry;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MusicBell.ClassIsland;

/// <summary>
/// MusicBell（点歌铃）与 ClassIsland 的桥。
/// 只做一件事：把「到这个时间了，放这首歌」翻译成对 MusicBell 的调用。
/// </summary>
[PluginEntrance]
public class Plugin : PluginBase
{
    public override void Initialize(HostBuilderContext context, IServiceCollection services)
    {
        services.AddAction<PlaySongAction, PlaySongActionSettingsControl>();
        services.AddAction<PlayNextAction, PlayNextActionSettingsControl>();
    }
}
