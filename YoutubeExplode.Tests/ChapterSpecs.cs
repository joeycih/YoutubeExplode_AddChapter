using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;

namespace YoutubeExplode.Tests
{
    public class ChapterSpecs
    {
        [Fact]
        public async Task I_can_get_chapters_of_a_YouTube_video()
        {
            var progress = new Progress<double>(
                (p) =>
                {
                    var p1 = Convert.ToInt32(Math.Round(p, 2) * 100); //保留2位小数后*100变成整数
                    //Console.WriteLine($"p:{p}__p1:{p1}");
                    Console.WriteLine("progress:", p1);
                    if (p1 >= 100)
                    {
                        //Console.WriteLine($"下载转换{vid}完成");
                    }
                }
            );

            // Arrange
            const string videoUrl = "https://www.youtube.com/watch?v=QBEItnHiqKM";
            var youtube = new YoutubeClient();
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync("QBEItnHiqKM");
            // Act
            var video = await youtube.Videos.GetAsync(videoUrl);

            // Assert
            if (video.Chapters != null)
            {
                Console.Write(video.Chapters);
                video.Chapters.Count().Should().Be(11);
                video.Chapters![0].Title.Should().Be("Us vs Them mentality");
                video.Chapters![0].TimeRangeStart.Should().Be(0);

                video.Chapters![5].Title.Should().Be("QUICK BITS");
                video.Chapters![5].TimeRangeStart.Should().Be(237000);

                video.Chapters![10].Title.Should().Be("Run homebrew DVD-R discs on PS2");
                video.Chapters![10].TimeRangeStart.Should().Be(342000);
            }
            var stream = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
            var start = video.Chapters![5].TimeRangeStart / 1000;
            var end = video.Chapters![6].TimeRangeStart / 1000;
            await youtube.Videos.DownloadAsync(
                new IStreamInfo[] { stream },
                new ConversionRequestBuilder(
                    Path.Combine(AppContext.BaseDirectory, "down/testchapter1.mp3")
                )
                    .SetFFmpegPath(Path.Combine(AppContext.BaseDirectory, "down/ffmpeg.exe"))
                    .SetContainer("mp3")
                    //.SetTime(start, end)
                    .Build(),
                progress
            );
        }
    }
}
