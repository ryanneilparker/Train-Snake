using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using TrainSnakeAPI.Models;

namespace TrainSnakeAPI.Data
{
	public class TrainSnakeDbContext : DbContext
	{
		public TrainSnakeDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Player> player { get; set; }
	}
}
