using ApiLab.Models;

namespace ApiLab.Interfaces
{
	public interface IArtistInterface
	{
		ICollection<Artist>GetArtists();
		Artist GetArtist(int id);
		bool ArtistExists(int id);
		bool UpdateArtist(Artist artist);
		bool CreateArtist(Artist artist);
		bool DeleteArtist(Artist artist);
		bool Save();
	}
}
