using ApiLab.Dto;
using ApiLab.Models;
using AutoMapper;

namespace ApiLab.Map
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<Artist, ArtistDto>();
            CreateMap<Album, AlbumDto>();
            CreateMap<ArtistDto, Artist>();
            CreateMap<AlbumDto, Album>();
            CreateMap<Song, SongDto>();
            CreateMap<SongDto, Song>();
        }
    }
}
