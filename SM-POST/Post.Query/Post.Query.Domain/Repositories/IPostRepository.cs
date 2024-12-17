using Post.Query.Domain.Entities;

namespace Post.Query.Domain.Repositories;

public interface IPostRepository
{
   Task CreateAsync(PostEntity post); 
   Task UpdateAsync(PostEntity post);
   Task DeleteAsync(PostEntity post);
   Task<PostEntity> GetByIdAsync(Guid postId);
   Task<List<PostEntyty>> ListAllAsync();> 
   Task<List<PostEntyty>> ListByAuthorAsync(string author);> 
   Task<List<PostEntity>> ListWithLikesAsync(int numberOfPosts);
   Task<List<PostEntity>> ListWithCommentsAsync();
}