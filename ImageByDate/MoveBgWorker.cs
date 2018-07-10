using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ImageByDate.Entities;

namespace ImageByDate
{
    public class MoveBgWorker : BackgroundWorker
    {
        public ProgressReport Report { get; set; }

        public MoveBgWorker()
        {
            Report = new ProgressReport();
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;

        }

        public void ReportProgress()
        {
            base.ReportProgress(0, Report);
        }

        public void ReportProgress(String message)
        {
            base.ReportProgress(0, message);
        }

        public void Init()
        {
            base.ReportProgress(99, String.Empty);
        }
        public void Reset()
        {
            Report = new ProgressReport();
        }
    }
}
