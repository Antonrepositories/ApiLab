using ApiLab.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLab.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{

		}
		public DbSet<Artist> Artists { get; set; }
		public DbSet<Album> Albums { get; set; }
		public DbSet<Song> Songs { get; set; }
	}
}
