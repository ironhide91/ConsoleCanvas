using ConsoleCanvas.Core;
using ConsoleCanvas.Impl.Parser;
using Pidgin;
using System.Collections.Generic;

namespace ConsoleCanvas.Impl
{
    public class CommandIdentifier : ICommandIdentifier
    {
        private readonly Dictionary<string, Parser<char, string>> identifiers =
            new Dictionary<string, Parser<char, string>>(10);

        public void BuildIdentifiers(IReadOnlySet<string> knownKeys)
        {
            foreach (var key in knownKeys)
            {
                var parser = Common.CommandCharacterParser(key);
                identifiers.Add(key, parser);
            }
        }

        public string Identify(string parameter)
        {
            foreach (var item in identifiers)
            {
                if (item.Value.Parse(parameter).Success)
                    return item.Key;
            }

            return null;
        }
    }
}