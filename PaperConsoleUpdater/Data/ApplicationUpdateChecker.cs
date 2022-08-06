namespace PaperUpdater.Data; 

public static class ApplicationUpdateChecker {
    private static string[] _ignoredVersionTags = { "1.0.0", "1.1.0", "1.2.0" };
    private static readonly string LazyTag = "\"tag_name\": \"" + "1.3.0" + "\"";
    private const string GitHub = "https://api.github.com/repos/Minty-Labs/PaperUpdater/releases";

    private static async Task<string> GetString() {
        var web = new HttpClient();
        web.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:87.0) Gecko/20100101 Firefox/87.0"); // Adds a "fake" client request
        //web.DefaultRequestHeaders.Add("Content-Type", "application/json"); // Looks for JSON format
        return await web.GetStringAsync(GitHub);
    }

    public static void CheckForUpdates() {
        try {
            var s = GetString().GetAwaiter().GetResult();
            
            // Console.WriteLine(s);
            
            s = _ignoredVersionTags.Aggregate(s, (current, t) => current.Replace("\"tag_name\": \"" + t + "\"", "\"tag_name\": \"old\""));

            // Console.WriteLine("======== NEW ========");
            // Console.WriteLine(s);
            
            if (s.Contains(LazyTag)) {
                if (Program.IsRunningDebug)
                    Logger.Log("Versions are matching");
            }
            else
                Logger.SendUpdateNotice();
        }
        catch { Logger.Error("Could not check for update. Please make sure you are connected to your network."); }
    }
}