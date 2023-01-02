using Newtonsoft.Json;
using PaperUpdater.PaperData;

namespace PaperUpdater.Data;

public class SelfData {
    [JsonProperty("Remembered Minecraft Version")]
    public string? RememberedMinecraftVersion { get; set; }
    [JsonProperty("Remembered Paper Type")]
    public string? RememberedPaperType { get; set; }
}

public static class Self {
    private static bool IsWindows => Environment.OSVersion.ToString().ToLower().Contains("windows");
    private static string _path = IsWindows ? 
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Minty Labs", "PaperUpdater") : 
        Environment.CurrentDirectory;
    public static SelfData? Data { get; set; }
    public static string? ContainedMinecraftVersion { get; set; }

    public static void Init() {
        if (IsWindows) {
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Minty Labs")))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Minty Labs"));
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Minty Labs", "PaperUpdater")))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Minty Labs", "PaperUpdater"));
        }
        if (!File.Exists($"{_path}{Path.DirectorySeparatorChar}SelfData.json")) {
            var data = new SelfData {
                RememberedMinecraftVersion = PaperProjectApi.LatestPaperProjectVersion,
                RememberedPaperType = "paper"
            };
            File.WriteAllText($"{_path}{Path.DirectorySeparatorChar}SelfData.json", JsonConvert.SerializeObject(data));
        }
        
        Data = JsonConvert.DeserializeObject<SelfData>(File.ReadAllText($"{_path}{Path.DirectorySeparatorChar}SelfData.json")) ?? throw new Exception();
        ContainedMinecraftVersion = Data.RememberedMinecraftVersion;
        PaperProjectApi.PaperType = GetPaperType();
    }

    private static string GetPaperType() => Data!.RememberedPaperType ??= "paper";

    public static void Save() => File.WriteAllText($"{_path}{Path.DirectorySeparatorChar}SelfData.json", JsonConvert.SerializeObject(Data));
}

/*public class ServerData {
    [JsonProperty("Latest PaperMC Version")]
    public string? LatestPaperMcVersion { get; set; }
}

public static class Server {
    public static string? LatestPaperMcVersion { get; set; }
    
    public static void Init() {
        var http = new HttpClient();
        var data = Program.IsRunningDebug ? File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "ServerData.json")) : 
            http.GetStringAsync("https://raw.githubusercontent.com/Minty-Labs/PaperUpdater/master/Resources/ServerData.json").GetAwaiter().GetResult();
        
        var d = JsonConvert.DeserializeObject<ServerData>(data) ?? throw new Exception();
        LatestPaperMcVersion = d.LatestPaperMcVersion;
    }
}*/