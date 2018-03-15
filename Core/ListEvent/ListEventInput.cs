
namespace Pari.Ics2Google.Core.ListEvent
{
    public class ListEventInput : IInput
    {
        public ListEventInput(string iscPath)
        {
            this.IscPath = iscPath;
        }

        public string IscPath { get; }
    }
}
