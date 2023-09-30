using ApiLab.Dto;
using ApiLab.Interfaces;
using ApiLab.Models;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace ApiLab.Controllers
{
	[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
	[ApiController]
	public class ArtistController : Controller
	{
		private readonly IArtistInterface _artist;
		private readonly IAlbumInterface _album;
		private readonly IMapper _mapper;
        public ArtistController(IArtistInterface artist, IAlbumInterface album, IMapper mapper)
        {
            this._artist = artist;
			this._album = album;
			this._mapper = mapper;
        }
		[HttpGet]
		public IActionResult GetArtists()
		{
			var artists = _mapper.Map<List<ArtistDto>>(_artist.GetArtists());
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(artists);
		}
		[HttpGet("{id}")]
		public IActionResult GetArtist(int id)
		{
			if (!_artist.ArtistExists(id))
			{
				return NotFound();
			}
			var artist = _mapper.Map<ArtistDto>(_artist.GetArtist(id));
            if (!ModelState.IsValid)
            {
				return BadRequest();
            }
			return Ok(artist);
        }
		[HttpPost]
		public IActionResult CreateArtist([FromBody] ArtistDto artistCreate)
		{
			if(artistCreate == null)
			{
				return BadRequest();
			}
			var artist = _artist.GetArtists().Where(a => a.Name.Trim().ToUpper() == artistCreate.Name.Trim().ToUpper()).FirstOrDefault();
			if(artist != null)
			{
				ModelState.AddModelError("", "Artist already exists");
				return StatusCode(422, ModelState);
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var artistMap = _mapper.Map<Artist>(artistCreate);
			if (!_artist.CreateArtist(artistMap))
			{
				ModelState.AddModelError("", "Something went while creating");
				return StatusCode(500, ModelState);
			}
			return Ok("Created!");
		}
		[HttpPut("{id}")]
		public IActionResult UpdateArtist(int id, [FromBody]ArtistDto updatedArtist)
		{
			if (updatedArtist == null)
			{
				return BadRequest(ModelState);
			}
			if(id != updatedArtist.Id)
			{
				return BadRequest(ModelState);
			}
			if (!_artist.ArtistExists(id))
			{
				return NotFound();
			}
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			var artisnMap = _mapper.Map<Artist>(updatedArtist);
			if (!_artist.UpdateArtist(artisnMap))
			{
				ModelState.AddModelError("", "Error while updating");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
		[HttpDelete("{id}")]
		public IActionResult DeleteArtist(int id)
		{
			if (!_artist.ArtistExists(id))
			{
				return NotFound();
			}
			var artistDelete = _artist.GetArtist(id);
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if (!_artist.DeleteArtist(artistDelete))
			{
				ModelState.AddModelError("", "Error while deleting");
			}
			return NoContent();
		}
    }
}
