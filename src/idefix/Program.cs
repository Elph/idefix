using System;
using CommandLine;

namespace idefix
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            var parser = new Parser(settings =>
            {
                settings.CaseInsensitiveEnumValues = true;
                settings.HelpWriter = Console.Out;
            });

            return parser.ParseArguments<GitHubReleaseOptions>(args)
                .MapResult(
                    o => new GitHubRelease(o).ExecuteAsync().Result,
                    errs => 1);
        }
    }
}