using System;

namespace YoutubeExplode.Converter.Utils.Extensions;

internal static class GenericExtensions
{
    public static TOut Pipe<TIn, TOut>(this TIn input, Func<TIn, TOut> transform) =>
        transform(input);

    public static TOut Pipe<TIn, Tin1, TOut>(
        this TIn input,
        Tin1 arg,
        Func<TIn, Tin1, TOut> transform
    ) => transform(input, arg);

    public static T Clamp<T>(this T value, T min, T max)
        where T : IComparable<T> =>
        value.CompareTo(min) <= 0
            ? min
            : value.CompareTo(max) >= 0
                ? max
                : value;
}
