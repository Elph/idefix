using CommandLine;

namespace idefix
{
    [Verb("github", HelpText = "Perform a new release in Github")]
    public class GitHubReleaseOptions
    {
        [Option('o', "owner", Required = true,
            HelpText = "Repository Owner")]
        public string Owner { get; set; }

        [Option('r', "repository", Required = true,
            HelpText = "Repository Name")]
        public string Repository { get; set; }

        [Option('t', "token", Required = true,
            HelpText = "GitHub Personal Token")]
        public string GitHubToken { get; set; }

        [Option('b', "bump", Required = false, Default = BumpType.Patch, 
            HelpText = "Release Type (Major, minor, patch)")]
        public BumpType Bump { get; set; }

        [Option('p', "prefix", Required = false, Default = "",
            HelpText = "Prefix")]
        public string Prefix { get; set; }

        [Option('d', "description", Required = false, Default = "",
            HelpText = "Release description")]
        public string Description { get; set; }

    }
}