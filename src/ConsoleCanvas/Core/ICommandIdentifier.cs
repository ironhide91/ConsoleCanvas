using System.Collections.Generic;

namespace ConsoleCanvas.Core
{
    public interface ICommandIdentifier
    {
        void BuildIdentifiers(IReadOnlySet<string> knownKeys);

        string Identify(string parameter);
    }
}