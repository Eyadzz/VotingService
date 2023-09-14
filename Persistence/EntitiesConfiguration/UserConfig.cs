using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VotingService.Domain;

namespace VotingService.Persistence.EntitiesConfiguration;

public class UserConfig : IEntityTypeConfiguration<UserVote>
{
    public void Configure(EntityTypeBuilder<UserVote> builder)
    {
        builder.HasKey(dg => new { dg.UserId, dg.VoteTopicChoiceId });
    }
}