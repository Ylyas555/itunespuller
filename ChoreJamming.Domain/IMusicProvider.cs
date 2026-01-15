using ChoreJamming.Domain.Models;

namespace ChoreJamming.Domain;

public interface IMusicProvider
{
    Task<List<Song>?> GetSongsAsync(string searchQuery);
    
}