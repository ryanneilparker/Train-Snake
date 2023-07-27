using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace TrainSnakeAPI.Models
{
	public class GameLog
	{
		public Int64 Id { get; set; }
		public int PlayerId { get; set; }
		public int Score { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
