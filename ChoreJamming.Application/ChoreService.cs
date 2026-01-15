using ChoreJamming.Domain;
using ChoreJamming.Domain.Models;

public class ChoreService
{
    private readonly IMusicProvider _music;
    private readonly IRepository<ChoreHistory> _repo;
    private readonly Random random = new Random();
    public List<Song>? Songs {get;set;} = new List<Song>();

    public ChoreService(IMusicProvider music, IRepository<ChoreHistory> repo)
    {
        _music = music;
        _repo = repo;
    }

    public async Task<Song?> ProcessChoreAsync(string choreName)
    {
        Songs = await _music.GetSongsAsync(choreName);
        if (Songs == null)
        {
            return null;
        }
        var song = Songs[random.Next(Songs.Count)];
        var history = new ChoreHistory 
        { 
            ChoreName = choreName, 
            SongTitle = song.Title,
            Date = DateTime.Now
        };
        await _repo.AddAsync(history);
        await _repo.SaveChangesAsync();
        return song;
    }
    
    public async Task<List<ChoreHistory>> GetHistoryAsync()
    {
        return await _repo.GetAllAsync();
    }
}