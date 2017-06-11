using System;
using System.Threading;

namespace DustInTheWind.FlagCalculator.Business
{
    public class StatusInfo : IDisposable
    {
        private string statusText;
        private string defaultStatusText;
        private readonly Timer timer;
        private bool disposed;

        public string StatusText
        {
            get { return statusText; }
            set
            {
                if (value == statusText)
                    return;

                statusText = value;
                timer.Change(ResetTimeout, new TimeSpan(-1));
                OnStatusTextChanged(EventArgs.Empty);
            }
        }

        public string DefaultStatusText
        {
            get { return defaultStatusText; }
            set
            {
                defaultStatusText = value;

                statusText = value;
                OnStatusTextChanged(EventArgs.Empty);
            }
        }

        public TimeSpan ResetTimeout { get; set; }

        public event EventHandler StatusTextChanged;

        protected virtual void OnStatusTextChanged(EventArgs e)
        {
            EventHandler handler = StatusTextChanged;
            handler?.Invoke(this, e);
        }

        public StatusInfo()
        {
            timer = new Timer(HandleTimerElapsed);
            defaultStatusText = string.Empty;
            statusText = string.Empty;
            ResetTimeout = TimeSpan.FromSeconds(1);
        }

        private void HandleTimerElapsed(object state)
        {
            statusText = defaultStatusText;
            OnStatusTextChanged(EventArgs.Empty);
        }

        public void SetPermanentStatusText(string text)
        {
            if (text == statusText)
                return;

            statusText = text;
            OnStatusTextChanged(EventArgs.Empty);
        }

        public void Reset()
        {
            if (statusText == defaultStatusText)
                return;

            statusText = defaultStatusText;
            OnStatusTextChanged(EventArgs.Empty);
        }

        public void Dispose()
        {
            if (disposed)
                return;

            timer.Dispose();

            disposed = true;
        }
    }
}
