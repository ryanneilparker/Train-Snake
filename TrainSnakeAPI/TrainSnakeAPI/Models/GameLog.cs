namespace TrainSnakeAPI.Models
{
	public class GameLog
	{
		public int Id { get; set; }
		public int PlayerId { get; set; }
		public int Score { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
