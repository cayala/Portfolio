using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestThreadLibrary.Models
{
    public class Threads
    {
        public int FirstValue {get; set;}
        public int SecondValue {get;set;}
        public int ThirdValue {get;set;}

        public int Min { get; set; }
        public int Max { get; set; }
        public int Average { get; set; }

        public DateTime LastExecution { get; set; }
        public DateTime NextExecution { get {return LastExecution.AddHours(1); } }
        public string Task { get; set; }
    }
}
