using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Internal;

namespace idefix
{
    public class GitHubService
    {
        private readonly GitHubClient _client;

        public GitHubService(string token, string ua = "idefix")
        {
            _client = new GitHubClient(new ProductHeaderValue(ua),
                new InMemoryCredentialStore(new Credentials(token)));
        }

        /// <summary>
        ///     Gets the latest release. If no prefix passed it will return the latest created in the repo. If a prefix is passed,
        ///     it will return the latest tag prefixed with the striing. That means we will get all tags on the repo, and inspect
        ///     them
        /// </summary>
        /// <param name="owner">Repository Owner</param>
        /// <param name="name">Repository name</param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public async Task<Octokit.Release> GetLatest(string owner, string name, string prefix = null)
        {
            try
            {
                if (string.IsNullOrEmpty(prefix))
                    return await _client.Repository.Release.GetLatest(owner, name);

                var releases = await _client.Repository.Release.GetAll(owner, name);
                return releases
                    .Where(_ => IsPrefixed(_.TagName, prefix))
                    .OrderByDescending(_ => _.PublishedAt)
                    .FirstOrDefault();
            }
            catch (NotFoundException)
            {
                // no releases
                return null;
            }
        }

        /// <summary>
        ///     Creates a new release for the owner/repo with the passed version and a optional description
        /// </summary>
        /// <param name="owner">Repository Owner</param>
        /// <param name="repository">Repository Name</param>
        /// <param name="version">Tag name</param>
        /// <param name="description">Description for the release</param>
        /// <returns></returns>
        public async Task CreateRelease(string owner, string repository, string version, string description = "")
        {
            var release = new NewRelease(version)
            {
                Prerelease = false,
                Name = version,
                TargetCommitish = "master",
                Body = description
            };
            await _client.Repository.Release.Create(owner, repository, release);
        }


        private bool IsPrefixed(string tagName, string prefix)
        {
            return tagName.ToLowerInvariant().StartsWith($"{prefix.ToLowerInvariant()}/");
        }
    }
}