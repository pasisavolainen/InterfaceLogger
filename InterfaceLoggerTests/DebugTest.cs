using System;
using System.Diagnostics;
using System.Text.Json;

namespace InterfaceLoggerTests
{
    [DebuggerDisplay("{DebuggerString}")]
    class DebugTest
    {
        string Name { get; set; }
        int Age { get; set; }

        string DebuggerString => $"{Name} {Age}";
        string DebuggerJsonString => new { Name, Age }.Dump();

    }
    public static class DumpExtensions
    {
        public static string Dump<T>(this T o)  => JsonSerializer.Serialize(o);
    }

}