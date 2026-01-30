using System.IO;
using FluentAssertions;
using Xunit;

namespace offer_manager.Tests.Controllers;

public class BackupScriptTests
{
    private static string ResolvePath(params string[] segments)
    {
        return Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", Path.Combine(segments)));
    }

    [Fact]
    public void BackupScript_ValidatesRequiredEnvVars()
    {
        var scriptPath = ResolvePath("databases", "db_backup", "backup.sh");
        var script = File.ReadAllText(scriptPath);

        script.Should().Contain("PGHOST is required");
        script.Should().Contain("PGPORT is required");
        script.Should().Contain("PGDATABASE is required");
        script.Should().Contain("PGUSER is required");
        script.Should().Contain("PGPASSWORD is required");
        script.Should().Contain("RESTIC_PASSWORD is required");
    }

    [Fact]
    public void BackupScript_UsesPgDumpAndRestic()
    {
        var scriptPath = ResolvePath("databases", "db_backup", "backup.sh");
        var script = File.ReadAllText(scriptPath);

        script.Should().Contain("pg_dump ");
        script.Should().Contain("restic backup");
        script.Should().Contain("restic forget");
    }
}
