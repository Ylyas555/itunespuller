using ChoreJamming.Domain;
using ChoreJamming.Domain.Models;
using Newtonsoft.Json.Linq;

namespace ChoreJamming.Infrastructure.Services;

public class AudioDbService : IMusicProvider
{
    private static readonly Random _random = new();

    private readonly HttpClient _http;

    public AudioDbService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Song>?> GetSongsAsync(string query)
    {
        var url = $"https://itunes.apple.com/search?term={query}&entity=song&limit=10";

        try 
        {
            var response = await _http.GetAsync(url);
            if (!response.IsSuccessStatusCode) return null;

            var jsonString = await response.Content.ReadAsStringAsync();
            
            if (string.IsNullOrWhiteSpace(jsonString)) return null;

            var json = JObject.Parse(jsonString);
            var results = json["results"];

            if (results == null || !results.HasValues) return null;
// TODO
            // var item = results[0];
            var item = results[_random.Next(results.Count())];

            var songs = new List<Song>();
            foreach (var item in results)
            {
                songs.Add(new Song
                {
                    Title = item["trackName"]?.ToString() ?? "Unknown",
                    Artist = item["artistName"]?.ToString() ?? "Unknown",
                    Album = item["collectionName"]?.ToString() ?? "Single",
                    VideoUrl = item["previewUrl"]?.ToString() ?? "",
                    ThumbnailUrl = item["artworkUrl100"]?.ToString().Replace("100x100", "600x600") ?? "",
                    Genre = item["primaryGenreName"]?.ToString() ?? "Unknown",
                    ReleaseYear = item["releaseDate"] != null
                        ? DateTime.Parse(item["releaseDate"]!.ToString()).Year
                        : (int?)null
                });
            }
            return songs;
        }
        catch
        {
            return null;
        }
    }
}