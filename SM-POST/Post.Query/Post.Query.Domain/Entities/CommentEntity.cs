using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Post.Query.Domain.Entities;

namespace Post.Query.Domain;

[Table("Comment")]
public class CommentEntity
{
    [Key]
    public required Guid ComemmentId { get; set; }
    
    public required Guid PostId { get; set; }
    
    public required string Username { get; set; }
    
    public required string Comment { get; set; }
    
    public required bool Edited { get; set; }
    
    public required DateTime DateCreated { get; set; }
    
    public required DateTime CommentDate { get; set; }
    
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual PostEntity Post { get; set; }
    
}