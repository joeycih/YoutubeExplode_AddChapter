using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using YoutubeExplode.Converter.Utils.Extensions;
using YoutubeExplode.Videos.Streams;

namespace YoutubeExplode.Converter;

/// <summary>
/// Builder for <see cref="ConversionRequest" />.
/// </summary>
public class ConversionRequestBuilder(string outputFilePath)
{
    private string? _ffmpegCliFilePath;
    private Container? _container;
    private ConversionPreset _preset;
    private ulong? _startTime;
    private ulong? _endTime;

    private Container GetDefaultContainer() =>
        new(Path.GetExtension(outputFilePath).TrimStart('.').NullIfWhiteSpace() ?? "mp4");

    /// <summary>
    /// Sets the path to the FFmpeg CLI.
    /// </summary>
    public ConversionRequestBuilder SetFFmpegPath(string path)
    {
        _ffmpegCliFilePath = path;
        return this;
    }

    /// <summary>
    /// Sets the output container.
    /// </summary>
    public ConversionRequestBuilder SetContainer(Container container)
    {
        _container = container;
        return this;
    }

    /// <summary>
    /// Sets the output container.
    /// </summary>
    public ConversionRequestBuilder SetContainer(string container) =>
        SetContainer(new Container(container));

    /// <summary>
    /// Sets the conversion format.
    /// </summary>
    [Obsolete("Use SetContainer instead."), ExcludeFromCodeCoverage]
    public ConversionRequestBuilder SetFormat(ConversionFormat format) =>
        SetContainer(new Container(format.Name));

    /// <summary>
    /// Sets the conversion format.
    /// </summary>
    [Obsolete("Use SetContainer instead."), ExcludeFromCodeCoverage]
    public ConversionRequestBuilder SetFormat(string format) => SetContainer(format);

    /// <summary>
    /// Sets the conversion preset.
    /// </summary>
    public ConversionRequestBuilder SetPreset(ConversionPreset preset)
    {
        _preset = preset;
        return this;
    }

    /// <summary>
    /// Sets startTime and endTime
    /// </summary>
    /// <param name="endTime"></param>
    /// <param name="startTime"></param>
    /// <returns></returns>
    public ConversionRequestBuilder SetTime(ulong startTime = 0, ulong endTime = 0)
    {
        _startTime = startTime;
        _endTime = endTime;
        return this;
    }

    /// <summary>
    /// Builds the resulting request.
    /// </summary>
    public ConversionRequest Build() =>
        new(
            _ffmpegCliFilePath ?? FFmpeg.GetFilePath(),
            outputFilePath,
            _container ?? GetDefaultContainer(),
            _preset,
            _startTime ?? 0,
            _endTime ?? 0
        );
}
