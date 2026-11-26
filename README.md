# MusicBell 点歌铃 · ClassIsland 插件

给 [MusicBell 点歌铃](https://github.com/ChenyangLT/MusicBell) 写的 ClassIsland 桥接插件：把「在课表里放一首歌」变成一个可以拖进自动化的**行动**。

| 行动 | 做什么 |
| --- | --- |
| **MusicBell 放一首歌** | 填上「歌手 歌名」，由 MusicBell 打开网易云、在搜索框里搜出来播放。 |
| **MusicBell 放下一首排歌** | 不填歌名，让 MusicBell 按自己的排歌表放下一首。 |
| **MusicBell 播放 / 暂停** | 内置播放器在放歌时暂停，再执行一次继续。 |
| **MusicBell 停止播放** | 只停内置播放器，网易云客户端不动。 |
| **MusicBell 关闭网易云音乐** | 结束 cloudmusic.exe（内置播放器一起停），上课打铃收尾很合适。 |
| **MusicBell 打开网易云音乐** | 把客户端叫出来放前台，不搜索也不放歌。 |

插件本身不碰音频：它只是把歌名通过 `musicbell://` 协议（或命令行）交给 MusicBell，由 MusicBell 完成「叫出网易云 → 点搜索框 → 粘贴 → 回车 → 播放」。所以 MusicBell 需要装着并且正在运行。

## 安装

**从插件包安装（推荐）**
1. 到 [Releases](https://github.com/ChenyangLT/MusicBell.ClassIsland/releases) 下载 `MusicBell.ClassIsland.cipx`（或从 MusicBell 的「设置 → 联动 → 释放插件」拿到）；
2. ClassIsland →「设置 → 插件」→ 从本地安装 → 选它；
3. 在插件页确认「MusicBell 点歌铃」是「已启用」。

**手动拷贝**

`text
ClassIsland\data\Plugins\MusicBell.ClassIsland\
`

把 `manifest.yml`、`MusicBell.ClassIsland.dll`、`icon.png`、`README.md` 放进去即可。

> ClassIsland 可能提示「该插件需要 2.1.x」——正常，插件就是按 2.1 的 API 编的。

## 用法

1. ClassIsland →「设置 → 自动化」→ 打开最上面的**「启用自动化」总开关**（默认关着）。
2. 「添加自动化」→ 选一个触发（上课 / 下课 / 定时 / 当前时间状态变化…）。
3. 「触发行动 → 添加行动」→ 分组「音乐铃」→「MusicBell 放一首歌」→ 填歌名。
4. 设置是即时保存的，回到自动化列表确认它是启用状态即可。

「外部逻辑路径」可以留空：插件会依次找 `%LocalAppData%\MusicBell\MusicBell.exe`、`Program Files\MusicBell\MusicBell.exe` 和它自己所在目录。

## 从源码构建

~~~powershell
dotnet publish -c Release -o out
~~~

输出目录里 `manifest.yml` 必须在根，把它整个压成 zip 就是 `.cipx`：

~~~powershell
Compress-Archive -Path out\* -DestinationPath MusicBell.ClassIsland.cipx
~~~

或者用 `dotnet publish -p:CreateCipx=true` 让 SDK 自动打包。

## 工程结构

~~~text
Plugin.cs                       插件入口（[PluginEntrance]）
PlaySongAction.cs               「放一首歌」行动
PlayNextAction.cs               「放下一首排歌」行动
SimpleActions.cs                暂停 / 停止 / 关客户端 / 开客户端 四个行动
SimpleActionSettings.cs         这四个行动共用的设置（可选 exe 路径）
*SettingsControl.cs             行动设置面板（纯 C# 搭的 Avalonia 界面）
MusicBellLauncher.cs            调起 MusicBell：先协议，失败退回命令行
Settings.cs                     两个行动各自的可序列化设置
manifest.yml                    插件清单
~~~

## 许可

[MIT](LICENSE)
