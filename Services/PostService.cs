using Blog.API.Dtos;
using Blog.API.Mappers;
using Blog.API.Repositories;

namespace Blog.API.Services
{
    public interface IPostService
    {
        Task Create(PostDto postDto);
        Task<IList<PostDto>> GetAll(int page, int size);
    }

    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;

        public PostService(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public async Task Create(PostDto postDto)
        {
            var post = PostMapper.ToEntity(postDto);

            postRepository.Insert(post);
        }

        public async Task<IList<PostDto>> GetAll(int page, int size)
        {
            var posts = postRepository.GetAll(page, size);

            var postDtos = posts.Select(post => post.ToDto()).ToList();

            return postDtos;
        }
    }
}
