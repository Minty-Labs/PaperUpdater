using Newtonsoft.Json;
using PaperUpdater.Data;

namespace PaperUpdater.PaperData;

public class Application {
    [JsonProperty("name")] public string name { get; set; }
    [JsonProperty("sha256")] public string sha256 { get; set; }
}

public class Build {
    [JsonProperty("build")] public int build { get; set; }
    [JsonProperty("time")] public DateTime time { get; set; }
    [JsonProperty("channel")] public string channel { get; set; }
    [JsonProperty("promoted")] public bool promoted { get; set; }
    [JsonProperty("changes")] public List<Change> changes { get; set; }
    [JsonProperty("downloads")] public Downloads downloads { get; set; }
}

public class Change {
    [JsonProperty("commit")] public string commit { get; set; }
    [JsonProperty("summary")] public string summary { get; set; }
    [JsonProperty("message")] public string message { get; set; }
}

public class Downloads {
    [JsonProperty("application")] public Application application { get; set; }
    [JsonProperty("mojang-mappings")] public MojangMappings MojangMappings { get; set; }
}

public class MojangMappings {
    [JsonProperty("name")] public string name { get; set; }
    [JsonProperty("sha256")] public string sha256 { get; set; }
}

public class PaperBuild {
    [JsonProperty("project_id")] public string project_id { get; set; }
    [JsonProperty("project_name")] public string project_name { get; set; }
    [JsonProperty("version")] public string version { get; set; }
    [JsonProperty("builds")] public List<Build> builds { get; set; }
}

public static class PaperBuildApiJson {
    private static PaperBuild? PaperBuildJsonData { get; set; }
    private static string _originalPaperFileName;
    
    public static void LoadPaperJson(string minecraftVersion) {
        var http = new HttpClient();
        var json = http.GetStringAsync($"https://api.papermc.io/v2/projects/paper/versions/{minecraftVersion}/builds").GetAwaiter().GetResult();
        PaperBuildJsonData = JsonConvert.DeserializeObject<PaperBuild>(json) ?? throw new Exception();
    }

    private static string GetLatestPurpurUrl() {
        var version = Program.PickingLatest ? PaperProjectApi.LatestPaperProjectVersion : Self.ContainedMinecraftVersion;
        return $"https://api.purpurmc.org/v2/purpur/{version}/latest/download";
    }

    private static string GetLatestPaperUrl() {
        var version = Program.PickingLatest ? PaperProjectApi.LatestPaperProjectVersion : Self.ContainedMinecraftVersion;
        
        if (PaperBuildJsonData?.version != version) {
            Logger.Error($"Paper version is not {version}");
            return "NO DATA";
        }
        
        var lastBuild = PaperBuildJsonData?.builds.LastOrDefault();
        if (lastBuild?.channel != "default") {
            Logger.Error("Paper build channel is not default");
            return "NO DATA";
        }
        
        var buildNumber = lastBuild.build;
        var buildName = lastBuild.downloads.application.name;
        _originalPaperFileName = $"paper-{version}-{buildNumber}.jar";
        return $"https://api.papermc.io/v2/projects/paper/versions/{version}/builds/{buildNumber}/downloads/{buildName}";
    }

    public static void UpdateJarFile() {
        var url = PaperProjectApi.PaperType == "purpur" ? GetLatestPurpurUrl() : GetLatestPaperUrl();
        if (url.Equals("NO DATA") || string.IsNullOrWhiteSpace(url)) {
            Logger.Error("Data not found or incorrect.");
            return;
        }
        
        var http = new HttpClient();
        Logger.Log($"Getting latest {(PaperProjectApi.PaperType == "purpur" ? "Purpur" : "Paper")} version from {url}");
        Logger.Log($"Downloading {_originalPaperFileName}");
        var bytes = http.GetByteArrayAsync(url).GetAwaiter().GetResult();
        var targetFile = Path.Combine(Environment.CurrentDirectory, "paper.jar");
        
        if (File.Exists(targetFile)) {
            Logger.Log("Deleting old paper.jar");
            File.Delete(targetFile);
        }
        
        File.WriteAllBytes(targetFile, bytes);
        Logger.Log("Finished updating paper.jar");
    }
}