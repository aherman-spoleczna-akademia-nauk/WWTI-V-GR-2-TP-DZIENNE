namespace DIAndSingleton;

public class Student
{
    public Guid StudentId { get; set; } = Guid.CreateVersion7();
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string AlbumNumber { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
