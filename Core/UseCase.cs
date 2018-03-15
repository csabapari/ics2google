using System;

namespace Pari.Ics2Google.Core
{
    public abstract class UseCase<TInput, TData> : IUseCase<TData> where TInput : class, IInput
    {
        public abstract IOutput<TData> DoExecute(TInput input);

        public IOutput<TData> Execute(IInput input)
        {
            if (!(input is TInput tInput))
            {
                throw new ArgumentException(string.Format("Expected type is {0}", typeof(TInput)));
            }
            return this.DoExecute(tInput);
        }
    }
}
