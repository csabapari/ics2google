
namespace Pari.Ics2Google.Console
{
    public class Argument
    {
        public Argument(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        public string Name { get; }

        public string Description { get; }
    }
}
