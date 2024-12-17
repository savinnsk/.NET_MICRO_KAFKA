using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Post.Query.Domain.Entities;

namespace Post.Query.Domain;

[Table("Comment")]
public class CommentEntity
{
    [Key]
    public Guid ComemmentId { get; set; }
    
    public Guid PostId { get; set; }
    
    public string Username { get; set; }
    
    public string Comment { get; set; }
    
    public bool Edited { get; set; }
    
    public DateTime DateCreated { get; set; }
    
    public DateTime CommentDate { get; set; }
    
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual PostEntity Post { get; set; }
    
}