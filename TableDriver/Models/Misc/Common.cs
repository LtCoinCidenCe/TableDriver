namespace TableDriver.Models.Misc;

// this becomes a requirement and doesn't implicitly does anything
public interface Common
{
    public long ID { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }
}
