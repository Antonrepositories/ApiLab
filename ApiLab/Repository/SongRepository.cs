using ApiLab.Data;
using ApiLab.Interfaces;
using ApiLab.Models;

namespace ApiLab.Repository
{
	public class SongRepository : ISongInterface
	{
		private readonly DataContext _context;
		public SongRepository(DataContext dataContext)
		{
			this._context = dataContext;
		}
		public bool SongExists(int id)
		{
			return _context.Songs.Any(a => a.Id == id);
		}

		public bool CreateSong(Song song)
		{
			_context.Add(song);
			return Save();
		}

		public bool DeleteSong(Song song)
		{
			_context.Remove(song);
			return Save();
		}

		public Song GetSong(int id)
		{
			return _context.Songs.Where(a => a.Id == id).FirstOrDefault();
		}

		public ICollection<Song> GetSongs()
		{
			return _context.Songs.OrderBy(a => a.Id).ToList();
		}
		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UpdateSong(Song song)
		{
			_context.Update(song);
			return Save();
		}
	}
}
