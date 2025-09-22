# Usage of LogClient #
## Add LogClient to project ##
1. Right-click on `solution` -> `add` -> `Reference`.
2. `Browse` on the bottom panel of `Reference manager`.
3. Select `LogClient.dll` and click `Add`.
4. Import library inside the code
```c#
using LogClient;
```
> ⚠ **Warning**: Logger class is intended for one instance per server usage. It will work with multiple instances but under the risk of **conflicts and higher resources usage**.
## Code examples ##
### Bare minimum ###
```c#
using LogClient;

// Create Logger for S01 serwer
using var logger = new Logger("S01"); 

// Add message with type: INFO, no tags and message: "message"
logger.Add("INFO", null, "message");
```

### Logger config ###
Logger constructor takes arguments:
- **serverId**
- **serverIdExt**
- **createFolder**
- **logFlushThreshold**
- **noThrow**

1. `serverId` - is mandatory as it is used to identify server, should be unique in whole project.
2. `serverIdExt` - optional additional id, used to help log filtering.
3. `createFolder` - creates folder: **log**, in the *invoker* directory. If `createFolder` is **false** and **log** folder doesn't exist, it will throw and error.
4. `logFlushThreshold` - logs are written to file every `logFlushThreshold` entries.
5. `noThrow` - Intended for **release** version, for debugging set to **false**. Doesn't throw errors when they arise, instead tries to use workarounds or ignore them.

#### Example ####
```c#
// Creates Logger instance for S01 server, which is backup server.
// It creates log folder if it doesn't exist, writes to log every 4 entries (use Add function to write new entries), and throws errors when they arise
using var logger = new Logger("S01", "Backup-server", true, 4, false);
```

### Writing to log ###
```c#
using var logger = new Logger("S01");

// This will generate entry with warning type, backup and low-space tags, and a "1024MB free space left on the disk." message.
bool result = logger.Add("WARNING", new[] {"backup", "low-space"}, "1024MB free space left on the disk.");
if (!restult)
{
    Console.Out.WriteLine("Failed to add log entry!");
}
else
{
    Console.Out.WriteLine("Successfully added log entry!");
}
```
> ℹ Available types are: **INFO, WARNING, ERROR, CRITICAL ERROR, NOTIFICATION, ATTENTION**. If `noThrow` is **true** then tags outside of this scope will be changed to **UNDEFINED**.

> ℹ You may specify **up to 5 tags**, they are used for easier filtering and are not mandatory. *You may pass null instead of an array if no tags are specified*.

> ℹ Message length must be between **1 and 256**. If `noThrow` is **true** message may be trimmed or discarded (if its empty).  


### Error handling ###
> ℹ Most errors are caught and rethrown as LoggerException

```c#
try 
{
    // Log operations
}
catch (LoggerException ex)
{
    // Catch exceptions thrown by Logger
}
catch (Exception ex)
{
    // Catch unknown exceptions (doubled security)
}
```

# Library working details #
## TODO