namespace VotingService.Domain;

[Table("VoteTopics", Schema = "Vote")]
public class VoteTopic
{
    public int Id { get; set; }
    public required string TopicName { get; set; }
    public required string TopicDescription { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    
    public ICollection<VoteTopicChoice> VoteTopicChoices { get; set; } = null!;
}