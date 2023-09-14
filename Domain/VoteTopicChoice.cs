namespace VotingService.Domain;

[Table("VoteTopicChoices", Schema = "Vote")]
public class VoteTopicChoice
{
    public int Id { get; set; }
    public required string ChoiceName { get; set; }
    
    public int VoteTopicId { get; set; }
    public VoteTopic VoteTopic { get; set; } = null!;
    
    public ICollection<UserVote> UserVotes { get; set; } = null!;
}