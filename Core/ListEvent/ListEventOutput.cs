using System;
using System.Collections.Generic;
using System.Text;

namespace Pari.Ics2Google.Core.ListEvent
{
    public class ListEventOutput : Output<IList<string>>
    {
        public ListEventOutput(bool executionResult, IList<string> data) : base(executionResult, data)
        {
        }
    }
}
