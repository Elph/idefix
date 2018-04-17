using System;
using Semver;

namespace idefix
{
    public class PrefixedSemVersion : IComparable<PrefixedSemVersion>, IComparable
    {
        private readonly SemVersion _version;

        public PrefixedSemVersion(string version)
        {
            var v = version;
            if (version.Contains("/"))
            {
                var t = version.Split('/');
                Prefix = t[0];
                v = t[1];
            }

            if (v.StartsWith("v"))
                v = v.Replace("v", "");

            _version = SemVersion.Parse(v);
        }

        public PrefixedSemVersion(string prefix, SemVersion version)
        {
            Prefix = prefix;
            _version = version;
        }

        public PrefixedSemVersion(string prefix, int major, int minor, int patch)
        {
            Prefix = prefix;
            _version = new SemVersion(major, minor, patch);
        }

        public string Prefix { get; }

        public int Patch => _version.Patch;
        public int Minor => _version.Minor;
        public int Major => _version.Major;
        public string Prerelease => _version.Prerelease;
        public string Build => _version.Build;

        public int CompareTo(object obj)
        {
            if (obj is PrefixedSemVersion version)
                return CompareTo(version);
            return 1;
        }

        public int CompareTo(PrefixedSemVersion other)
        {
            return Prefix == other.Prefix
                ? _version.CompareTo(other._version)
                : string.Compare(Prefix, other.Prefix, StringComparison.Ordinal);
        }


        public PrefixedSemVersion Change(int? major = null, int? minor = null, int? patch = null,
            string prerelease = null, string build = null)
        {
            var version = _version.Change(major, minor, patch, prerelease, build);
            return new PrefixedSemVersion(Prefix, version);
        }

        public PrefixedSemVersion IncrementMajor()
            => Change(Major + 1);

        public PrefixedSemVersion IncrementMinor()
            => Change(minor: Minor + 1);

        public PrefixedSemVersion IncrementPatch()
            => Change(patch: Patch + 1);

        public override string ToString()
            => string.IsNullOrEmpty(Prefix) ? $"v{_version}" : $"{Prefix}/v{_version}";

    }
}