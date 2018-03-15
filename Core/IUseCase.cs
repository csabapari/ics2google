
namespace Pari.Ics2Google.Core
{
    public interface IUseCase<out TData>
    {
        IOutput<TData> Execute(IInput input);
    }
}
