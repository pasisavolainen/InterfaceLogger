using Microsoft.CodeAnalysis;

namespace InterfaceLogger.AOT.Utility
{
    internal static class Extensions
    {
        public static string Visibility(this ISymbol symbol)
            => symbol.DeclaredAccessibility.ToString().ToLower();
    }
}
