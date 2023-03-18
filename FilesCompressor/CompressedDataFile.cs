using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto1Criptografia.FilesCompressor
{
    public class CompressedDataFile
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Hash { get; set; }
        public List<FileCompressed> FilesCompressed { get; set; }
    }
}
