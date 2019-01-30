using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrequencyAnalysisTools.Data
{
    class LetterContext: PageData
    {
        public string Letter { get; set; } = "A";
        public string Mapping { get; set; } = "";
        public int Count { get; set; } = 24;
    }
}
