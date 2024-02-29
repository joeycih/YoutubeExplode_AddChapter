using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using YoutubeExplode.Common;
using YoutubeExplode.Videos.Chapters;

namespace YoutubeExplode.Videos;

/// <summary>
/// Metadata associated with a YouTube video.
/// </summary>
public class Video(
    VideoId id,
    string title,
    Author author,
    DateTimeOffset uploadDate,
    string description,
    TimeSpan? duration,
    IReadOnlyList<Thumbnail> thumbnails,
    IReadOnlyList<string> keywords,
    Engagement engagement,
    IReadOnlyList<Chapter>? chapters = null
) : IVideo
{
    /// <inheritdoc />
    public VideoId Id { get; } = id;

    /// <inheritdoc />
    public string Url => $"https://www.youtube.com/watch?v={Id}";

    /// <inheritdoc />
    public string Title { get; } = title;

    /// <inheritdoc />
    public Author Author { get; } = author;

    /// <summary>
    /// Video upload date.
    /// </summary>
    public DateTimeOffset UploadDate { get; } = uploadDate;

    /// <summary>
    /// Video description.
    /// </summary>
    public string Description { get; } = description;

    /// <inheritdoc />
    public TimeSpan? Duration { get; } = duration;

    /// <inheritdoc />
    public IReadOnlyList<Thumbnail> Thumbnails { get; } = thumbnails;

    /// <summary>
    /// Available search keywords for the video.
    /// </summary>
    public IReadOnlyList<string> Keywords { get; } = keywords;

    /// <summary>
    /// Engagement statistics for the video.
    /// </summary>
    public Engagement Engagement { get; } = engagement;

    /// <summary>
    /// Chapters for this video.
    /// Will be null for playlists and searches.
    /// </summary>
    public IReadOnlyList<Chapter>? Chapters { get; } = chapters;

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override string ToString() => $"Video ({Title})";
}
