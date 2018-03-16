
namespace Pari.Ics2Google.Core.Import2Google
{
    public class Import2GoogleUseCase : UseCase<Import2GoogleInput, object>
    {
        public override IOutput<object> DoExecute(Import2GoogleInput input)
        {
            return new DefaultOutput(true);
        }
    }
}
