using System.IO;
using System.Text;
using System.Windows.Controls;

namespace ConsoleEmulation
{
    /// <summary>
    /// A custom TextWriter that redirects console output to a TextBlock control in a WPF application.
    /// Useful to redirect errors and messages, since WPF does not support a console natively.
    /// </summary>
    public class ConsoleTextToTextBlock : TextWriter
    {
        private TextBlock _textBlock;
        private ScrollViewer _scrollViewer;
        private Queue<string> _lines = new Queue<string>();
        private readonly int _maxLines = 100;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleTextToTextBlock"/> class.
        /// </summary>
        /// <param name="textBlock">The TextBlock control to which console output will be redirected.</param>
        /// <param name="scrollViewer">The ScrollViewer control used to enable scrolling on the TextBlock.</param>
        public ConsoleTextToTextBlock(TextBlock textBlock, ScrollViewer scrollViewer)
        {
            _textBlock = textBlock;
            _scrollViewer = scrollViewer;
            textBlock.Text = string.Empty;
        }

        /// <summary>
        /// Gets the encoding used by the TextWriter. This implementation uses UTF-8 encoding.
        /// </summary>
        public override Encoding Encoding => Encoding.UTF8;

        /// <summary>
        /// Writes a single character to the TextBlock.
        /// </summary>
        /// <param name="value">The character to write.</param>
        public override void Write(char value)
        {
            Write(value.ToString());
        }

        /// <summary>
        /// Writes a string followed by a line terminator to the TextBlock.
        /// If the maximum number of lines is exceeded, the oldest line is removed.
        /// This method also ensures that the ScrollViewer scrolls to show the latest output.
        /// </summary>
        /// <param name="value">The string to write.</param>
        public override void WriteLine(string value)
        {
            _textBlock.Dispatcher.Invoke(() =>
            {
                if (_lines.Count >= _maxLines)
                {
                    _lines.Dequeue();
                }

                _lines.Enqueue(value);
                _textBlock.Text = string.Join("\n", _lines);

                // Ensure the ScrollViewer scrolls to the bottom
                _scrollViewer.ScrollToEnd();
            });
        }
    }
}