using System;
using System.IO;
using System.Threading.Tasks;
using YoutubeExplode.Converter;
using YoutubeExplode.Demo.Cli.Utils;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace YoutubeExplode.Demo.Cli;

// This demo prompts for video ID and downloads one media stream.
// It's intended to be very simple and straight to the point.
// For a more involved example - check out the WPF demo.
public static class Program
{
    public static async Task Main()
    {
        var progress = new Progress<double>(
            (p) =>
            {
                var p1 = Convert.ToInt32(Math.Round(p, 2) * 100); //保留2位小数后*100变成整数
                Console.WriteLine($"p:{p}__p1:{p1}");
                //Console.WriteLine("progress:", p1);
                if (p1 >= 100)
                {
                    Console.WriteLine($"下载转换完成");
                }
            }
        );

        const string videoUrl = "https://www.youtube.com/watch?v=t-9KpD_drdk";
        var youtube = new YoutubeClient();
        var streamManifest = await youtube.Videos.Streams.GetManifestAsync("t-9KpD_drdk");
        // Act
        var video = await youtube.Videos.GetAsync(videoUrl);

        var stream = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
        var start = video.Chapters![2].TimeRangeStart / 1000;
        var end = video.Chapters![3].TimeRangeStart / 1000;
        await youtube.Videos.DownloadAsync(
            new IStreamInfo[] { stream },
            new ConversionRequestBuilder(
                Path.Combine(AppContext.BaseDirectory, "down/02. 忘了你忘了我.mp3")
            )
                .SetFFmpegPath(Path.Combine(AppContext.BaseDirectory, "down/ffmpeg.exe"))
                .SetContainer("mp3")
                .SetTime(start, end)
                .Build(),
            progress
        );
    }
}
