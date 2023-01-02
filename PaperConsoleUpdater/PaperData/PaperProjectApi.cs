using Newtonsoft.Json;
using PaperUpdater.Data;

namespace PaperUpdater.PaperData; 

public class PaperProject {
    [JsonProperty("project_id")] public string project_id { get; set; }
    [JsonProperty("project_name")] public string project_name { get; set; }
    [JsonProperty("version_groups")] public List<string> version_groups { get; set; }
    [JsonProperty("versions")] public List<string> versions { get; set; }
}

public static class PaperProjectApi {
    private static PaperProject? PaperProjectJsonData { get; set; }
    public static string? LatestPaperProjectVersion, PaperType;

    public static void LoadProjectData() {
        var http = new HttpClient();
        var json = http.GetStringAsync("https://papermc.io/api/v2/projects/paper").GetAwaiter().GetResult();
        PaperProjectJsonData = JsonConvert.DeserializeObject<PaperProject>(json) ?? throw new Exception();
    }
    
    public static void GetLatestPaperProjectVersion() {
        if (PaperProjectJsonData == null) 
            LoadProjectData();
        if (PaperType == "paper" && PaperProjectJsonData!.project_id != "paper")
            throw new Exception("Invalid project id: paper");
        LatestPaperProjectVersion = PaperProjectJsonData!.versions.Last();
    }
}