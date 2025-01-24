
namespace TableDriver.Models.Misc;

public class CommonImplementSample : Common
{
    public ulong ID { get; set; }
    public DateTime CreatedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public DateTime LastUpdatedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
