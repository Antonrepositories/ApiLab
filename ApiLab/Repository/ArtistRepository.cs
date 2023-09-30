using ApiLab.Data;
using ApiLab.Interfaces;
using ApiLab.Models;

namespace ApiLab.Repository
{
	public class ArtistRepository : IArtistInterface
	{
		private readonly DataContext _context;
        public ArtistRepository(DataContext dataContext)
        {
            this._context = dataContext;
        }
        public bool ArtistExists(int id)
		{
			return _context.Artists.Any(a => a.Id == id);
		}

		public bool CreateArtist(Artist artist)
		{
			_context.Add(artist);
			return Save();
		}

		public bool DeleteArtist(Artist artist)
		{
			_context.Remove(artist);
			return Save();
		}

		public Artist GetArtist(int id)
		{
			return _context.Artists.Where(a => a.Id == id).FirstOrDefault();
		}

		public ICollection<Artist> GetArtists()
		{
			return _context.Artists.OrderBy(a => a.Id).ToList();
		}
		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UpdateArtist(Artist artist)
		{
			_context.Update(artist);
			return Save();
		}
	}
}
