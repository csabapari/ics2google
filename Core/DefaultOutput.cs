using System;
using System.Collections.Generic;
using System.Text;

namespace Pari.Ics2Google.Core
{
    public class DefaultOutput : Output<object>
    {
        public DefaultOutput(bool executionResult, object data) : base(executionResult, data)
        {
        }

        public DefaultOutput(bool executionResult) : this(executionResult, null)
        {
        }
    }
}
