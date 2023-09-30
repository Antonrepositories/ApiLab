using ApiLab.Data;
using ApiLab.Interfaces;
using ApiLab.Models;

namespace ApiLab.Repository
{
	public class AlbumRepository : IAlbumInterface
	{
		private readonly DataContext _context;
		public AlbumRepository(DataContext dataContext)
		{
			this._context = dataContext;
		}
		public bool AlbumExists(int id)
		{
			return _context.Albums.Any(a => a.Id == id);
		}

		public bool CreateAlbum(Album album)
		{
			_context.Add(album);
			return Save();
		}

		public bool DeleteAlbum(Album album)
		{
			_context.Remove(album);
			return Save();
		}

		public Album GetAlbum(int id)
		{
			return _context.Albums.Where(a => a.Id == id).FirstOrDefault();
		}

		public ICollection<Album> GetAlbums()
		{
			return _context.Albums.OrderBy(a => a.Id).ToList();
		}
		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UpdateAlbum(Album album)
		{
			_context.Update(album);
			return Save();
		}
	}
}