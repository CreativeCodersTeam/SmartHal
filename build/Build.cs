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
    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;

    [GitRepository] readonly GitRepository GitRepository;

    [GitVersion] readonly GitVersion GitVersion;

    AbsolutePath SourceDirectory => RootDirectory / "source";

    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    const string PackageProjectUrl = "https://github.com/CreativeCodersTeam/SmartHal";

    Target Clean => _ => _
        .Before(Restore)
        .UseBuildAction<CleanBuildAction>(this);

    Target Restore => _ => _
        .Before(Compile)
        .UseBuildAction<RestoreBuildAction>(this);

    Target Compile => _ => _
        .After(Clean)
        .UseBuildAction<DotNetCompileBuildAction>(this);

    Target Test => _ => _
        .After(Compile)
        .UseBuildAction<UnitTestAction>(this,
            x => x
                .SetUnitTestsBasePath("UnitTests")
                .SetProjectsPattern("**/*.csproj")
                .SetResultsDirectory(ArtifactsDirectory / "test_results"));

    Target RunBuild => _ => _
        .DependsOn(Clean)
        .DependsOn(Restore)
        .Executes(Compile);

    Target RunTest => _ => _
        .DependsOn(RunBuild)
        .Executes(Test);

    Configuration IBuildInfo.Configuration => Configuration;

    Solution IBuildInfo.Solution => Solution;

    GitRepository IBuildInfo.GitRepository => GitRepository;

    IVersionInfo IBuildInfo.VersionInfo => new GitVersionWrapper(GitVersion, "0.0.0", 1);

    AbsolutePath IBuildInfo.SourceDirectory => SourceDirectory;

    AbsolutePath IBuildInfo.ArtifactsDirectory => ArtifactsDirectory;
}
