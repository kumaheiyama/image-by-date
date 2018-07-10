using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageByDate
{
    public class ProgressReport
    {
        public int Value { get; set; }
        public int Maximum { get; set; }
        public int Failed { get; set; }
        public int Exists { get; set; }
        public int Succeded { get; set; }

        public ProgressReport()
        {
            Value = 0;
            Maximum = 0;
            Failed = 0;
            Exists = 0;
            Succeded = 0;
        }
    }
}
