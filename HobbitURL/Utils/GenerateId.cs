namespace HobbitURL.Utils;
using NUlid;
public class GenerateId
{
    public static string Id()
    {
        Ulid.NewUlid();
        return Ulid.NewUlid().ToString();
    }
}