using Post.Query.Domain.Entities;

namespace Post.Query.Domain.Repositories;

public interface IPostRepository
{
   Task CreateAsync(PostEntity post); 
   Task UpdateAsync(PostEntity post);
   Task DeleteAsync(PostEntity post);
   Task<PostEntity> GetByIdAsync(Guid postId);
   Task<List<PostEntity>> ListAllAsync();
   Task<List<PostEntity>> ListByAuthorAsync(string author); 
   Task<List<PostEntity>> ListWithLikesAsync(int numberOfPosts);
   Task<List<PostEntity>> ListWithCommentsAsync();
}