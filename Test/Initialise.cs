//-----------------------------------------------------------------------------
// Filename: Initialise.cs
//
// Description: Assembly initialiser for websocket-sharp unit tests.
//
// Author(s):
// Aaron Clauson
//
// History:
// 04 Nov 2019	Aaron Clauson	Created (aaron@sipsorcery.com).
//
// License: 
// BSD 3-Clause "New" or "Revised" License, see included LICENSE.md file.
//-----------------------------------------------------------------------------

using System;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebSocketSharp.Net.UnitTests
{
    public static class UnitTestState
    {
        public static ILogger Log = null;
    }

    [TestClass]
    public class Initialize
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            Console.WriteLine("AssemblyInitialise");
            UnitTestState.Log = SimpleConsoleLogger.Instance;
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            Console.WriteLine("AssemblyCleanup");
        }
    }

    /// <summary>
    /// Getting the Microsoft console logger to work with the mstest framework was unsuccessful. Using this super
    /// simple console logger proved to be a lot easier. Can be revisited if mstest logging ever goes back to 
    /// just working OOTB.
    /// </summary>
    public class SimpleConsoleLogger : ILogger
    {
        public static SimpleConsoleLogger Instance { get; } = new SimpleConsoleLogger();

        private SimpleConsoleLogger()
        { }

        public IDisposable BeginScope<TState>(TState state)
        {
            return SimpleConsoleLoggerScope.Instance;
        }

        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss:fff")}] [{Thread.CurrentThread.ManagedThreadId}] [{logLevel}] {formatter(state, exception)}");
        }

        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            return true;
        }
    }

    public class SimpleConsoleLoggerScope : IDisposable
    {
        public static SimpleConsoleLoggerScope Instance { get; } = new SimpleConsoleLoggerScope();

        private SimpleConsoleLoggerScope()
        {}

        public void Dispose()
        {}
    }
}
