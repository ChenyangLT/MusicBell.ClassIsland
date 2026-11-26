using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace MusicBell.ClassIsland;

/// <summary>
/// 统一在这里拼调用地址：优先走 musicbell:// 协议（MusicBell 常驻托盘时由单实例接住），
/// 协议没注册时退回直接启动 MusicBell.exe --play。
/// </summary>
internal static class MusicBellLauncher
{
    public const string Scheme = "musicbell";

    public static void Play(string? query, string? label = null, long? songId = null, string? exePath = null)
    {
        query = query?.Trim();
        if (string.IsNullOrEmpty(query) && songId is null)
            throw new InvalidOperationException("还没有填歌名。请点开这个行动，在「歌名」里写上要放的歌。");

        var url = Build($"play?q={Escape(query)}&id={songId?.ToString() ?? ""}&label={Escape(label)}&src=classisland");

        // 协议走不通（没注册 / 没装）时，用用户填的路径直接拉一次。
        if (!TryStartUrl(url))
        {
            var exe = FindExe(exePath);
            if (exe is null)
                throw new InvalidOperationException(
                    "没能叫起 MusicBell。请先在 MusicBell 的「设置 → 联动」里注册 musicbell 协议，或在行动设置里填上 MusicBell.exe 的完整路径。");

            var args = new StringBuilder();
            args.Append("--play ").Append(Quote(query ?? ""));
            if (!string.IsNullOrWhiteSpace(label)) args.Append(" --label ").Append(Quote(label!));
            if (songId is not null) args.Append(" --id ").Append(songId.Value);

            Process.Start(new ProcessStartInfo
            {
                FileName = exe,
                Arguments = args.ToString(),
                UseShellExecute = true,
                WorkingDirectory = Path.GetDirectoryName(exe) ?? Environment.CurrentDirectory
            });
        }
    }

    public static void PlayNext(string? exePath = null)
    {
        if (TryStartUrl(Build("next?src=classisland"))) return;

        var exe = FindExe(exePath);
        if (exe is null)
            throw new InvalidOperationException(
                "没能叫起 MusicBell。请先在 MusicBell 的「设置 → 联动」里注册 musicbell 协议，或在行动设置里填上 MusicBell.exe 的完整路径。");

        Process.Start(new ProcessStartInfo
        {
            FileName = exe,
            Arguments = "--play-next",
            UseShellExecute = true,
            WorkingDirectory = Path.GetDirectoryName(exe) ?? Environment.CurrentDirectory
        });
    }

    /// <summary>停掉内置播放器。</summary>
    public static void Stop(string? exePath = null) => Send("stop", exePath, "--stop", "停止播放");

    /// <summary>播放 / 暂停切换。</summary>
    public static void Pause(string? exePath = null) => Send("pause", exePath, "--pause", "播放/暂停");

    /// <summary>关掉网易云客户端（同时停掉内置播放器）。</summary>
    public static void CloseMusic(string? exePath = null) => Send("close", exePath, "--close", "关闭网易云音乐");

    /// <summary>打开（或叫到前台）网易云客户端。</summary>
    public static void OpenMusic(string? exePath = null) => Send("open", exePath, "--open-native", "打开网易云音乐");

    static void Send(string host, string? exePath, string fallbackArg, string actionName)
    {
        if (TryStartUrl(Build($"{host}?src=classisland"))) return;

        var exe = FindExe(exePath);
        if (exe is null)
            throw new InvalidOperationException(
                $"没能叫起 MusicBell，所以没法{actionName}。请先在 MusicBell 的「设置 → 联动」里注册 musicbell 协议，或在行动设置里填上 MusicBell.exe 的完整路径。");

        Process.Start(new ProcessStartInfo
        {
            FileName = exe,
            Arguments = fallbackArg,
            UseShellExecute = true,
            WorkingDirectory = Path.GetDirectoryName(exe) ?? Environment.CurrentDirectory
        });
    }

    static string Build(string tail) => $"{Scheme}://{tail}";

    static bool TryStartUrl(string url)
    {
        try
        {
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            return true;
        }
        catch
        {
            return false;
        }
    }

    static string? FindExe(string? configured)
    {
        if (!string.IsNullOrWhiteSpace(configured) && File.Exists(configured)) return configured;

        var candidates = new[]
        {
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MusicBell", "MusicBell.exe"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "MusicBell", "MusicBell.exe"),
            AppContext.BaseDirectory + "MusicBell.exe"
        };
        foreach (var c in candidates)
            if (File.Exists(c)) return c;
        return null;
    }

    static string Escape(string? value) => Uri.EscapeDataString(value ?? "");

    static string Quote(string value) => "\"" + value.Replace("\"", "\\\"") + "\"";
}
