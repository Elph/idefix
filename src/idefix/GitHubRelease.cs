using System;
using System.Threading.Tasks;
using Semver;

namespace idefix
{
    public class GitHubRelease
    {
        private readonly GitHubService _gitHubService;
        private readonly GitHubReleaseOptions _options;

        public GitHubRelease(GitHubReleaseOptions options)
        {
            _options = options;
            _gitHubService = new GitHubService(_options.GitHubToken);
        }

        public async Task<int> ExecuteAsync()
        {
            var latest = await _gitHubService.GetLatest(_options.Owner, _options.Repository, _options.Prefix);
            var version = latest == null
                ? new PrefixedSemVersion(_options.Prefix, new SemVersion(0))
                : new PrefixedSemVersion(latest.TagName);

            var newVersion = Bump(version);
            await _gitHubService.CreateRelease(_options.Owner, _options.Repository, newVersion.ToString(),
                _options.Description);

            Console.WriteLine($"{newVersion}");
            return 0;
        }

        private PrefixedSemVersion Bump(PrefixedSemVersion version)
        {
            switch (_options.Bump)
            {
                case BumpType.Major:
                    return version.IncrementMajor();
                case BumpType.Minor:
                    return version.IncrementMinor();
                default:
                case BumpType.Patch:
                    return version.IncrementPatch();
            }
        }
    }
}