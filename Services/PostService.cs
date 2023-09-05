using Blog.API.Dtos;
using Blog.API.Mappers;
using Blog.API.Models;
using Blog.API.Repositories;

namespace Blog.API.Services
{
    public interface IPostService
    {
        Task AddComment(long userId, long postId, string content);
        Task Create(CreatePostDto createPostDto);
        Task<IList<PostDto>> GetAll(int page, int size);
        Task Review(long postId, bool approved);
        Task Update(long postId, UpdatePostDto updatePostDto);
    }

    public class PostService : IPostService
    {
        private readonly IUserService userService;
        private readonly IPostRepository postRepository;

        public PostService(IUserService userService,IPostRepository postRepository)
        {
            this.userService = userService;
            this.postRepository = postRepository;
        }

        public async Task AddComment(long userId, long postId, string content)
        {
            var commentAuthor = await userService.GetUser(userId);

            if(commentAuthor == null)
            {
                throw new Exception();
            }

            var post = postRepository.GetById(postId);

            if(post == null)
            {
                throw new Exception();
            }

            post.Comments.Add(new Comment 
            { 
                Author = commentAuthor,
                Content = content
            });

            postRepository.Update(post);
        }

        public async Task Create(CreatePostDto postDto)
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

        public async Task Review(long postId, bool approved)
        {
            var post = postRepository.GetById(postId);

            if(post is null || post.Status != PostStatusEnum.Submitted)
            {
                throw new Exception();
            }

            post.Status = approved ? PostStatusEnum.Published : PostStatusEnum.Rejected;

            postRepository.Update(post);
        }

        public async Task Update(long postId, UpdatePostDto postDto)
        {
            var existingPost = postRepository.GetById(postId);

            if(existingPost is null)
            {
                throw new Exception();
            }

            if(existingPost.Status == PostStatusEnum.Submitted || existingPost.Status == PostStatusEnum.Published)
            {
                throw new Exception();
            }

            existingPost.Title = postDto.Title;
            existingPost.Content = postDto.Content;

            postRepository.Update(existingPost);
        }
    }
}
