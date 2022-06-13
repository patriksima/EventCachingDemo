namespace EventCachingDemo.Server.Helpers;

public static class CollectionExtensions
{
    private static readonly Random Random = new();
     
    public static T? GetRandomElement<T>(this ICollection<T> list)
    {
        return !list.Any() ? default : list.ElementAt(Random.Next(list.Count));
    }
}