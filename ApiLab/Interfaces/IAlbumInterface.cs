using ApiLab.Models;

namespace ApiLab.Interfaces
{
	public interface IAlbumInterface
	{
		ICollection<Album> GetAlbums();
		Album GetAlbum(int id);
		bool AlbumExists(int id);
		bool UpdateAlbum(Album album);
		bool CreateAlbum(Album album);
		bool DeleteAlbum(Album album);
		bool Save();
	}
}
