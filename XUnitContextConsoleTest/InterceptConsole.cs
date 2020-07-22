// Licensed under the Apache License, Version 2.0 (the "License").
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XUnitContextConsoleTest
{
    public class InterceptConsole : TextWriter
    {
        private readonly TextWriter _textWriterImplementation;

        public int WriteCount;

        public InterceptConsole(TextWriter textWriterImplementation) 
            => _textWriterImplementation = textWriterImplementation;

        /// <inheritdoc />
        public override Encoding Encoding => _textWriterImplementation.Encoding;

        /// <inheritdoc />
        public override void Write(char value)
        {
            Interlocked.Increment(ref WriteCount);
            _textWriterImplementation.Write(value);
        }

        public override void Write(string value)
        {
            Interlocked.Increment(ref WriteCount);
            _textWriterImplementation.Write(value);
        }

        public override void WriteLine()
        {
            Interlocked.Increment(ref WriteCount);
            _textWriterImplementation.WriteLine();
        }

        public override void WriteLine(string value)
        {
            Interlocked.Increment(ref WriteCount);
            _textWriterImplementation.WriteLine(value);
        }

        public override Task WriteAsync(char value)
        {
            Interlocked.Increment(ref WriteCount);
            return _textWriterImplementation.WriteAsync(value);
        }

        public override Task WriteAsync(string value)
        {
            Interlocked.Increment(ref WriteCount);
            return _textWriterImplementation.WriteAsync(value);
        }

        public override Task WriteLineAsync(string value)
        {
            Interlocked.Increment(ref WriteCount);
            return _textWriterImplementation.WriteAsync(value);
        }
    }
}
