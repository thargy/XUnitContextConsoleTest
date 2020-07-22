using System;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace XUnitContextConsoleTest
{
    public class ConsoleLogTest : XunitContextBase
    {
        /// <inheritdoc />
        public ConsoleLogTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void ShowIssue()
        {
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger =loggerFactory.CreateLogger<ConsoleLogTest>();
            logger.LogInformation("Log information");

            // FAILS - even though console has been logged to at this point
            // (confirmed below and via breakpoints showing)
            Assert.Equal(1, Logs.Count);
        }

        [Fact]
        public void Test1()
        {
            // Intercept Console just wraps a text writer and counts invocations of write methods.
            var interceptConsole = new InterceptConsole(Console.Out);
            Console.SetOut(interceptConsole);
            using (var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole()))
            {
                var logger = loggerFactory.CreateLogger<ConsoleLogTest>();
                logger.LogInformation("Log information");
            } // disposing logger factory actually flushes it

            Assert.Equal(2, interceptConsole.WriteCount);

            // FAILS - due to incorrect logic regarding line endings.
            Assert.Equal(1, Logs.Count);
        }
    }
}
