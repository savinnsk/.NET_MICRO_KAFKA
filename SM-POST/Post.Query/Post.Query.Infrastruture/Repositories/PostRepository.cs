using Microsoft.EntityFrameworkCore;
using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories;
using Post.Query.Infrastruture.DataAccess;

namespace Post.Query.Infrastruture.Repositories
{
    public class PostRepository : IPostRepository
    {

        private readonly DatabaseContextFactory _contextFactory;

        public PostRepository(DatabaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task CreateAsync(PostEntity post)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
                context.Posts.Add(post);
                _ = await context.SaveChangesAsync();

        }

        public async Task DeleteAsync(Guid postId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            var post = await GetByIdAsync(postId);

            if(post == null) return;

            context.Posts.Remove(post);
            _ = await context.SaveChangesAsync();
        }

        public async Task<PostEntity> GetByIdAsync(Guid postId)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();

            var post = context.Posts
            .Include(p => p.Comments)
            .FirstOrDefault(x => x.PostId == postId);

            return post;
        }

        public async Task<List<PostEntity>> ListAllAsync()
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();

            var posts = await context.Posts.
            AsNoTracking().
            Include(p => p.Comments).
            ToListAsync();

            return posts;
        }

        public async Task<List<PostEntity>> ListByAuthorAsync(string author)
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();

            var posts = await context.Posts.AsNoTracking()
            .Include(p => p.Comments)
            .Where(x => x.Author
            .Contains(author))
            .ToListAsync();

            return posts;
        }

        public async Task<List<PostEntity>> ListAllPostsWithCommentsAsync()
        {
            using DatabaseContext context = _contextFactory.CreateDbContext();
            
            var posts = await context.Posts
            .AsNoTracking()
            .Include(p => p.Comments)
            .AsNoTracking()
            .Where(x => x.Comments != null && x.Comments.Any())
            .ToListAsync();

            return posts;
        }

        public async Task<List<PostEntity>> ListAllPostsWithLikesAsync(int numberOfLikes)
        {
             using DatabaseContext context = _contextFactory.CreateDbContext();
            
            var posts = await context.Posts
            .AsNoTracking()
            .Include(p => p.Comments)
            .AsNoTracking()
            .Where(x => x.Likes >= numberOfLikes)
            .ToListAsync();

            return posts;
        }

        public async Task UpdateAsync(PostEntity post)
        {
              using DatabaseContext context = _contextFactory.CreateDbContext();
                context.Posts.Update(post);
                _ = await context.SaveChangesAsync();
        }
    }

}