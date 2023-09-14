namespace VotingService.Controllers.Models;

public class VoteTopicRequest
{
    public string TopicName { get; set; } = null!;
    public string TopicDescription { get; set; } = null!;
    public string[] Choices { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}