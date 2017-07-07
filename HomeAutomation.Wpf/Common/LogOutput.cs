using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace HomeAutomation.Wpf.Common
{
    public class LogOutput
    {
        private readonly TextBox _output;

        public LogOutput(TextBox output)
        {
            _output = output;
        }

        public void Log(string message)
        {
            _output.Dispatcher.Invoke(
                DispatcherPriority.Normal,
                new Action(() => _output.AppendText(message + "\r\n")));
        }
    }
}
