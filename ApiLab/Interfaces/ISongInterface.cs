using ApiLab.Models;

namespace ApiLab.Interfaces
{
	public interface ISongInterface
	{
		ICollection<Song> GetSongs();
		Song GetSong(int id);
		bool SongExists(int id);
		bool UpdateSong(Song song);
		bool CreateSong(Song song);
		bool DeleteSong(Song song);
		bool Save();
	}
}
