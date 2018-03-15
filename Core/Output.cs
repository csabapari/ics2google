
namespace Pari.Ics2Google.Core
{
    public abstract class Output<TData> : IOutput<TData>
    {
        public Output(bool executionResult, TData data)
        {
            this.ExecutionResult = executionResult;
            this.Data = data;
        }

        public bool ExecutionResult { get; }
        public TData Data { get; }
    }
}
