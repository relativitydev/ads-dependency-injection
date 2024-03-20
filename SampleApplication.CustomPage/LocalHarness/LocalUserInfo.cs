using Relativity.API;
using System;

namespace SampleApplication.CustomPage.LocalHarness
{
    public class LocalUserInfo : IUserInfo
    {
        public int WorkspaceUserArtifactID => 1234567890;

        public int ArtifactID => 67890;

        public string FirstName => "Local";

        public string LastName => "User";

        public string FullName => "Local User";

        public string EmailAddress => throw new NotImplementedException();

        public int AuditWorkspaceUserArtifactID => throw new NotImplementedException();

        public int AuditArtifactID => throw new NotImplementedException();
    }
}