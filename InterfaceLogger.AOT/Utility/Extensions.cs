using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace InterfaceLogger.AOT.Utility
{
    internal static class Extensions
    {
        public static string Visibility(this ISymbol symbol)
            => symbol.DeclaredAccessibility.ToString().ToLower();

        public static string JoinString<T>(this IEnumerable<T> source, string separator = ", ")
            => string.Join(separator, source);

        public static string Quoted<T>(this T item, string left="\"", string right="\"")
            => $"{left}{item}{right}";
    }
}
