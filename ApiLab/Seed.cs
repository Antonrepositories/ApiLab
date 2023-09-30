using ApiLab.Data;
using ApiLab.Models;
using System.Reflection;

namespace ApiLab
{
	public class Seed
	{
		private readonly DataContext _context;
		public Seed(DataContext context)
		{
			this._context = context;
		}
		public void SeedDataContext()
		{
			if (!_context.Artists.Any())
			{
				var artists = new List<Artist>()
				{
					new Artist()
					{
						Name = "Drake",
						Albums = new List<Album>()
						{
							new Album()
							{
								Title = "Her Loss",
								Description = "Drakes album",
								Songs = new List<Song>()
								{
									new Song()
									{
										Name = "Broke Boys"
									},
									new Song()
									{
										Name = "Jumbotron Shit Poppin"
									}
								}
							},
							new Album()
							{
								Title = "Certified Lover Boy",
								Description = "Drakes Album",
								Songs = new List<Song>()
								{
									new Song()
									{
										Name = "Girls Want Girls"
									},
									new Song()
									{
										Name = "In the Bible"
									}
								}
							}
						}
					},
					new Artist()
					{
						Name = "Lil Uzi Vert",
						Albums = new List<Album>()
						{
							new Album()
							{
								Title = "Pink Tape",
								Description = "Uzis Album",
								Songs = new List<Song>()
								{
									new Song()
									{
										Name = "X2"
									},
									new Song()
									{
										Name = "Zoom"
									}
								}
							}
						}
					},
					new Artist()
					{
						Name = "Central Cee",
						Albums = new List<Album>()
						{
							new Album()
							{
								Title = "No More Leaks",
								Description = "Cenrals Album",
								Songs = new List<Song>()
								{
									new Song()
									{
										Name = "Bumpy Jonson"
									},
									new Song()
									{
										Name = "One Up"
									}
								}
							}
						}
					}
				};
				_context.Artists.AddRange(artists);
				_context.SaveChanges();
			}
		}
	}
}
