using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VotingService.Controllers.Models;
using VotingService.Domain;
using VotingService.Persistence;

namespace VotingService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VoteController : ControllerBase
{
    private readonly AppDbContext _context;

    public VoteController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("GetTopicResult")]
    public async Task<IActionResult> GetTopicResult(int userId, int voteTopicId)
    {
       var userVoted = _context.UsersVotes.AsNoTracking()
           .Include(uv => uv.VoteTopicChoice)
           .ThenInclude(vtc => vtc.VoteTopic)
           .Any(x => x.UserId == userId && x.VoteTopicChoice.VoteTopicId == voteTopicId);

       if (!userVoted)
              return BadRequest("You have not voted for this topic.");
       
       var votesOnTopic = await _context.UsersVotes.AsNoTracking()
           .Include(uv => uv.VoteTopicChoice)
           .ThenInclude(vtc => vtc.VoteTopic)
           .Where(x => x.VoteTopicChoice.VoteTopicId == voteTopicId)
           .GroupBy( x => x.VoteTopicChoiceId)
              .Select(x => new
              {
                VoteTopicChoiceId = x.Key,
                VoteTopicChoiceName = x.Select(y => y.VoteTopicChoice.ChoiceName).FirstOrDefault(),
                Count = x.Count()
              })
           .ToListAsync();

       return Ok(votesOnTopic);
    }

    [HttpPost("UserVote")]
    public async Task<IActionResult> UserVote(int userId, int voteTopicId, int voteTopicChoiceId)
    {
        var votingTimeExpired = await _context.VoteTopics.AsNoTracking()
            .AnyAsync(x => x.Id == voteTopicId && x.StartDate > DateTime.UtcNow && x.EndDate < DateTime.UtcNow);
        
        if (votingTimeExpired)
            return BadRequest("Voting time has expired.");
        
        var voteExists = _context.UsersVotes.AsNoTracking()
            .Include(uv => uv.VoteTopicChoice)
            .ThenInclude(vtc => vtc.VoteTopic)
            .Any(x => x.UserId == userId && x.VoteTopicChoice.VoteTopicId == voteTopicId);
        
        if (voteExists)
            return BadRequest("You have already voted for this topic.");

        var newVote = new UserVote
        {
            UserId = userId,
            VoteTopicChoiceId = voteTopicChoiceId
        };
        
        await _context.UsersVotes.AddAsync(newVote);
        await _context.SaveChangesAsync();
        
        return Ok("Vote added successfully.");
    }
    
    [HttpPost("CreateVoteTopic")]
    public async Task<IActionResult> CreateVoteTopic(VoteTopicRequest voteTopicRequest)
    {
        var newVoteTopicChoices = voteTopicRequest.Choices.Select(voteTopicChoice => new VoteTopicChoice { ChoiceName = voteTopicChoice }).ToList();

        var newVoteTopic = new VoteTopic
        {
            TopicName = voteTopicRequest.TopicName,
            TopicDescription = voteTopicRequest.TopicDescription,
            StartDate = voteTopicRequest.StartDate,
            EndDate = voteTopicRequest.EndDate,
            VoteTopicChoices = newVoteTopicChoices
        };
        
        await _context.VoteTopics.AddAsync(newVoteTopic);
        await _context.SaveChangesAsync();
        
        return Ok("Vote topic created successfully.");
    }
}