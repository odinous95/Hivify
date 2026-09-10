namespace Hivify.AI.Services.FeedGenerator
{
    public interface IFeedGenerator
    {
        Task<GeneratedFeedDto> GenerateAsync(string instruction);
    }
}
