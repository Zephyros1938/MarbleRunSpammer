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
    static MarbleRunContents.Trackdata trackdata = new MarbleRunContents.Trackdata(duration: 10000, trackname: "Test");
    public static async Task Main(String[] args)
    {
        if (args.Length <= 1) throw new ArgumentException("Must specify repetition count.");
        int MaxDegreeOfParallelism = 2;
        if (args.Length == 2) StringToInt(args[1], out MaxDegreeOfParallelism);

        StringToInt(args[0], out int repetitions);
        Console.WriteLine(repetitions);

        using HttpClient client = new HttpClient();
        MultipartFormDataContent requestContent = [];
        requestContent.Add(new StringContent(trackdata.duration.ToString(), Encoding.UTF8, "application/json"), "track[duration]");
        requestContent.Add(new StringContent(trackdata.length.ToString(), Encoding.UTF8, "application/json"), "track[length]");
        requestContent.Add(new StringContent(trackdata.username.ToString(), Encoding.UTF8, "application/json"), "track[username]");
        requestContent.Add(new StringContent(trackdata.trackname.ToString(), Encoding.UTF8, "application/json"), "track[trackname]");
        requestContent.Add(new StringContent(trackdata.imagedata.ToString(), Encoding.UTF8, "application/json"), "track[imagedata]");
        requestContent.Add(new StringContent(trackdata.json.ToString(), Encoding.UTF8, "application/json"), "track[json]");

        await Parallel.ForAsync(0, repetitions, new ParallelOptions { MaxDegreeOfParallelism = MaxDegreeOfParallelism }, async (i, ct) =>
        {
            Console.WriteLine($"Sending request No.{i}");
            var r = await SendRequest(client, requestContent);
            Console.WriteLine($"Request No.{i} sent, status code is {r.StatusCode}");
            if (r.StatusCode == HttpStatusCode.InternalServerError)
            {
                Console.WriteLine(r.ReasonPhrase);
            }
        });
    }

    public static async Task<HttpResponseMessage> SendRequest(HttpClient client, HttpContent? requestContent)
    {
        HttpRequestMessage request = new(HttpMethod.Post, "https://www.marblerun.at/tracks");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        foreach (var h in RequestData.Headers.RequestHeaders)
        {
            request.Headers.Add(h.Key, h.Value);
        }

        request.Content = requestContent;
        return await client.SendAsync(request);
    }

    public static void StringToInt(String InString, out Int32 OutInt)
    {
        bool result = Int32.TryParse(InString, out Int32 number);
        if (result) { OutInt = number; return; };
        throw new InvalidCastException($"Could not cast {InString} To Int");
    }

    public static async Task<MultipartFormDataContent> JsonToFormData(String fileLocation)
    {
        string location = Path.Combine("Assets", "Samples", fileLocation);
        MultipartFormDataContent formData = [];
        string jsonContent = await File.ReadAllTextAsync(location);
        var jsonObject = JsonNode.Parse(jsonContent);
        foreach (var item in jsonObject.AsObject())
        {
            formData.Add(new StringContent(item.Value.ToString(), Encoding.UTF8, "application/json"), item.Key);
        }
        return formData;
    }
}