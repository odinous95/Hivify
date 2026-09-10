namespace Hivify.AI.Services
{
    public interface IFeedGenerator
    {
        Task<GeneratedFeedDto> GenerateAsync(string instruction);
    }
}
