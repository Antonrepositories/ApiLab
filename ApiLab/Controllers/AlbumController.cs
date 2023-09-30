using ApiLab.Dto;
using ApiLab.Interfaces;
using ApiLab.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;


namespace ApiLab.Controllers
{
	[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
	[ApiController]
	public class AlbumController : Controller
	{
		private readonly IArtistInterface _artist;
		private readonly IAlbumInterface _album;
		private readonly IMapper _mapper;
		public AlbumController(IArtistInterface artist, IAlbumInterface album, IMapper mapper)
		{
			this._artist = artist;
			this._album = album;
			this._mapper = mapper;
		}
		[HttpGet]
		public IActionResult GetArtists()
		{
			var albums = _mapper.Map<List<AlbumDto>>(_album.GetAlbums());
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(albums);
		}
		[HttpGet("{id}")]
		public IActionResult GetAlbum(int id)
		{
			if (!_album.AlbumExists(id))
			{
				return NotFound();
			}
			var album = _mapper.Map<AlbumDto>(_album.GetAlbum(id));
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			return Ok(album);
		}
		[HttpPost]
		public IActionResult CreateAlbum(int artisdId, [FromBody] AlbumDto albumCreate)
		{
			if (albumCreate == null)
			{
				return BadRequest();
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var albumMap = _mapper.Map<Album>(albumCreate);
			albumMap.Artist = _artist.GetArtist(artisdId);
			if (!_album.CreateAlbum(albumMap))
			{
				ModelState.AddModelError("", "Something went while creating");
				return StatusCode(500, ModelState);
			}
			return Ok("Created!");
		}
		[HttpPut("{id}")]
		public IActionResult UpdateAlbum(int id, [FromBody] AlbumDto updatedAlbum)
		{
			if (updatedAlbum == null)
			{
				return BadRequest(ModelState);
			}
			if (id != updatedAlbum.Id)
			{
				return BadRequest(ModelState);
			}
			if (!_album.AlbumExists(id))
			{
				return NotFound();
			}
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			var albumMap = _mapper.Map<Album>(updatedAlbum);
			if (!_album.UpdateAlbum(albumMap))
			{
				ModelState.AddModelError("", "Error while updating");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
		[HttpDelete("{id}")]
		public IActionResult DeleteArtist(int id)
		{
			if (!_album.AlbumExists(id))
			{
				return NotFound();
			}
			var albumDelete = _album.GetAlbum(id);
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if (!_album.DeleteAlbum(albumDelete))
			{
				ModelState.AddModelError("", "Error while deleting");
			}
			return NoContent();
		}
	}
}
