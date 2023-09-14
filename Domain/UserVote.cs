namespace VotingService.Domain;

[Table("UserVotes", Schema = "Vote")]
public class UserVote
{
    public int VoteTopicChoiceId { get; set; }
    public int UserId { get; set; }
    
    public VoteTopicChoice VoteTopicChoice { get; set; } = null!;
}