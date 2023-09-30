using ApiLab.Data;
using ApiLab.Models;
using ApiLab.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLab.Test
{
	public class ArtistRepositoryTest
	{
		private async Task<DataContext> GetDataContext()
		{
			var options = new DbContextOptionsBuilder<DataContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;
			var databaseContext = new DataContext(options);
			databaseContext.Database.EnsureCreated();
			if (await databaseContext.Artists.CountAsync() <= 0)
			{
				for (int i = 0; i < 5; i++)
				{
					databaseContext.Add(
						new Artist()
						{
							Name = $"Central Cee{i}",
							Albums = new List<Album>()
						{
							new Album()
							{
								Title = $"No More Leaks{i}",
								Description = "Cenrals Album",
								Songs = new List<Song>()
								{
									new Song()
									{
										Name = $"Bumpy Jonson{i}"
									},
									new Song()
									{
										Name = $"One Up{i}"
									}
								}
							}
						}
						});
					await databaseContext.SaveChangesAsync();
				}
			}
			return databaseContext;
		}
		[Fact]
		public async void ArtistRepository_GetArtist_ReturnsArtist()
		{
			//Arrange
			var id = 1;
			var dbContext = await GetDataContext();
			var artistRepository = new ArtistRepository(dbContext);
			//Act
			var result = artistRepository.GetArtist(id);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType<Artist>();
		}
		[Fact]
		public async void AlbumRepository_ReturnsAlbum()
		{
			//Arrange
			var id = 3;
			var dbContext = await GetDataContext();
			var albumRepository = new AlbumRepository(dbContext);
			//Act
			var result = albumRepository.GetAlbum(id);
			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType<Album>();
		}
		[Fact]
		public async void SongRepository_ReturnsSong()
		{
			//Arrange
			var id = 10;
			var dbContext = await GetDataContext();
			var songRepository = new SongRepository(dbContext);
			//Act
			var result = songRepository.GetSong(id);
			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType<Song>();
		}
	}
}
