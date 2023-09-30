namespace ApiLab.Models
{
	public class Album
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public ICollection<Song> Songs { get; set; }
		public virtual Artist Artist { get; set; }

	}
}
