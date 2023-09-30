using ApiLab.Dto;
using ApiLab.Interfaces;
using ApiLab.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiLab.Controllers
{
	[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
	[ApiController]
	public class SongController : Controller
	{
		private readonly ISongInterface _song;
		private readonly IAlbumInterface _album;
		private readonly IMapper _mapper;
		public SongController(ISongInterface song, IAlbumInterface album, IMapper mapper)
		{
			this._song = song;
			this._album = album;
			this._mapper = mapper;
		}
		[HttpGet]
		public IActionResult GetSongs()
		{
			var songs = _mapper.Map<List<SongDto>>(_song.GetSongs());
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(songs);
		}
		[HttpGet("{id}")]
		public IActionResult GetSong(int id)
		{
			if (!_song.SongExists(id))
			{
				return NotFound();
			}
			var song = _mapper.Map<SongDto>(_song.GetSong(id));
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			return Ok(song);
		}
		[HttpPost]
		public IActionResult CreateAlbum(int albumId, [FromBody] SongDto songCreate)
		{
			if (songCreate == null)
			{
				return BadRequest();
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var songMap = _mapper.Map<Song>(songCreate);
			songMap.Album = _album.GetAlbum(albumId);
			if (!_song.CreateSong(songMap))
			{
				ModelState.AddModelError("", "Something went while creating");
				return StatusCode(500, ModelState);
			}
			return Ok("Created!");
		}
		[HttpPut("{id}")]
		public IActionResult UpdateSong(int id, [FromBody] SongDto updatedSong)
		{
			if (updatedSong == null)
			{
				return BadRequest(ModelState);
			}
			if (id != updatedSong.Id)
			{
				return BadRequest(ModelState);
			}
			if (!_song.SongExists(id))
			{
				return NotFound();
			}
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			var songMap = _mapper.Map<Song>(updatedSong);
			if (!_song.UpdateSong(songMap))
			{
				ModelState.AddModelError("", "Error while updating");
				return StatusCode(500, ModelState);
			}
			return NoContent();
		}
		[HttpDelete("{id}")]
		public IActionResult DeleteSong(int id)
		{
			if (!_song.SongExists(id))
			{
				return NotFound();
			}
			var songDelete = _song.GetSong(id);
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if (!_song.DeleteSong(songDelete))
			{
				ModelState.AddModelError("", "Error while deleting");
			}
			return NoContent();
		}
	}
}
