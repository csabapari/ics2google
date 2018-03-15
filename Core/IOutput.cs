
namespace Pari.Ics2Google.Core
{
    public interface IOutput<out TData>
    {
        /// <summary>
        /// If the execution successful then the value should be true otherwise false.
        /// </summary>
        bool ExecutionResult { get; }

        /// <summary>
        /// The resulting data of the use-case execution. It can be null.
        /// </summary>
        TData Data { get; }
    }
}
