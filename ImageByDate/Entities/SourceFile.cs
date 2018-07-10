using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ImageByDate.Entities
{
    public class SourceFile
    {
        public SourceFile()
        {

        }
        public SourceFile(string filePath) : this()
        {
            this.FilenameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            this.Filename = Path.GetFileName(filePath);
            this.FullPath = filePath;
        }

        public string Filename { get; set; }
        public string FullPath { get; set; }
        public string FilenameWithoutExtension { get; set; }
    }
}
