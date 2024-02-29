using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeExplode.Videos.Chapters
{
    /// <summary>
    /// Chapter
    /// </summary>
    public class Chapter
    {
        /// <summary>
        /// Chapter Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Start of the chapter in milliseconds.
        /// </summary>
        public ulong TimeRangeStart { get; set; }

        /// <summary>
        /// Initializes an instance of <see cref="Chapter"/>.
        /// </summary>
        public Chapter(string title, ulong timeRangeStart)
        {
            Title = title != null ? title : string.Empty;
            TimeRangeStart = timeRangeStart;
        }

        /// <summary>
        ///
        /// </summary>
        public Chapter()
        {
            Title = string.Empty;
            TimeRangeStart = ulong.MinValue;
        }
    }
}
