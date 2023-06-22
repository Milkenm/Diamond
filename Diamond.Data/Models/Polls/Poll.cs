using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diamond.Data.Models.Polls
{
	[Table("Poll")]
	public class DbPoll
	{
		public long Id { get; set; }
		public required ulong DiscordMessageId { get; set; }
		public required ulong DiscordUserId { get; set; }
		public required string Title { get; set; }
		public required string Description { get; set; }
		public required string ImageUrl { get; set; }
		public required string ThumbnailUrl { get; set; }
		[DefaultValue(false)] public bool IsPublished { get; set; }
		public required long CreatedAt { get; set; }
		public required long UpdatedAt { get; set; }
	}
}