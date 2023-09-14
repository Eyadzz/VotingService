using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VotingService.Domain;

namespace VotingService.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    public DbSet<UserVote> UsersVotes { get; set; } = null!;
    public DbSet<VoteTopic> VoteTopics { get; set; } = null!;
    public DbSet<VoteTopicChoice> VoteTopicChoices { get; set; } = null!;
}