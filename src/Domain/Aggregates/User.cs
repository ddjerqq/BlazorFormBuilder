namespace Domain.Aggregates;

public sealed class User // : Entity
{
 public Guid Id { get; init; } 
 public string Name { get; set; } = string.Empty;
 public string Email { get; set; } = string.Empty;

    private User()
    {
    }
 public User(string name , string email)
 {
     Name = name;
     Email = email;
 }
}