using System.Diagnostics.CodeAnalysis;
using CreativeCoders.NukeBuild;
using CreativeCoders.NukeBuild.BuildActions;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.GitVersion;

[PublicAPI]
[CheckBuildProjectConfigurations]
[UnsetVisualStudioEnvironmentVariables]
[SuppressMessage("ReSharper", "ConvertToAutoProperty")]
// ReSharper disable once CheckNamespace
class Build : NukeBuild, IBuildInfo
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    
    [GitRepository] readonly GitRepository GitRepository;
    
    [GitVersion] readonly GitVersion GitVersion;
    
    AbsolutePath SourceDirectory => RootDirectory / "source";

    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    Target Clean => _ => _
        .Before(Restore)
        .UseBuildAction<CleanBuildAction>(this);

    Target Restore => _ => _
        .Before(Compile)
        .UseBuildAction<RestoreBuildAction>(this);

    Target Compile => _ => _
        .DependsOn(Restore)
        .ProceedAfterFailure()
        .UseBuildAction<DotNetCompileBuildAction>(this);

    Target RunTests => _ => _
        .DependsOn(Compile)
        .UseBuildAction<UnitTestAction>(this,
            x => x
                .SetUnitTestsBasePath("UnitTests")
                .SetProjectsPattern("**/*.csproj")
                .SetResultsDirectory(ArtifactsDirectory / "test_results"));
    
    Target Rebuild => _ => _
        .DependsOn(Clean)
        .DependsOn(Compile);

    Configuration IBuildInfo.Configuration => Configuration;

    Solution IBuildInfo.Solution => Solution;

    GitRepository IBuildInfo.GitRepository => GitRepository;

    IVersionInfo IBuildInfo.VersionInfo => new GitVersionWrapper(GitVersion, "0.0.0", 1);

    AbsolutePath IBuildInfo.SourceDirectory => SourceDirectory;

    AbsolutePath IBuildInfo.ArtifactsDirectory => ArtifactsDirectory;
}
