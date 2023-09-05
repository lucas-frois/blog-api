using Blog.API.Dtos;
using Blog.API.Mappers;
using Blog.API.Models;
using Blog.API.Repositories;

namespace Blog.API.Services
{
    public interface IPostService
    {
        Task<IList<PostDto>> GetAll(int page, int size);
        Task<IList<PostDto>> GetFromWriter(string? email, int page, int size);
        Task<IList<PostDto>> SearchByStatus(PostStatusEnum postStatusEnum, int page, int size);
        Task Create(CreatePostDto createPostDto);
        Task Update(long postId, UpdatePostDto updatePostDto);
        Task Submit(long postId);
        Task Review(long postId, bool approved);
        Task AddComment(long userId, long postId, string content);
    }

    public class PostService : IPostService
    {
        private readonly IUserService userService;
        private readonly IPostRepository postRepository;

        public PostService(IUserService userService, IPostRepository postRepository)
        {
            this.userService = userService;
            this.postRepository = postRepository;
        }

        public async Task<IList<PostDto>> GetAll(int page, int size)
        {
            var posts = postRepository.GetAll(page, size);

            var postDtos = posts.Select(post => post.ToDto()).ToList();

            return postDtos;
        }

        public async Task<IList<PostDto>> GetFromWriter(string? email, int page, int size)
        {
            throw new NotImplementedException();
        }


        public async Task<IList<PostDto>> SearchByStatus(PostStatusEnum postStatusEnum, int page, int size)
        {
            var postStatus = postStatusEnum.ToString();

            var posts = postRepository.SearchByCondition(post => post.Status == postStatus, page, size);

            return posts.Select(post => post.ToDto()).ToList();
        }

        public async Task Create(CreatePostDto postDto)
        {
            var post = PostMapper.ToEntity(postDto);

            postRepository.Insert(post);
        }

        public async Task Update(long postId, UpdatePostDto postDto)
        {
            var existingPost = postRepository.GetById(postId);

            if (existingPost is null)
            {
                throw new Exception();
            }

            if (existingPost.StatusEnum == PostStatusEnum.Submitted || existingPost.StatusEnum == PostStatusEnum.Published)
            {
                throw new Exception();
            }

            existingPost.Title = postDto.Title;
            existingPost.Content = postDto.Content;

            postRepository.Update(existingPost);
        }

        public async Task Submit(long postId)
        {
            var post = postRepository.GetById(postId);

            if (post == null)
            {
                throw new Exception();
            }

            if (post.StatusEnum != PostStatusEnum.Created)
            {
                throw new Exception();
            }

            post.Status = PostStatusEnum.Submitted.ToString();

            postRepository.Update(post);
        }

        public async Task Review(long postId, bool approved)
        {
            var post = postRepository.GetById(postId);

            if (post is null)
            {
                throw new Exception();
            }

            if (post.StatusEnum != PostStatusEnum.Submitted)
            {
                throw new Exception();
            }

            post.Status = (approved ? PostStatusEnum.Published : PostStatusEnum.Rejected).ToString();

            postRepository.Update(post);
        }

        public async Task AddComment(long userId, long postId, string content)
        {
            var commentUser = await userService.GetUser(userId);

            if (commentUser == null)
            {
                throw new Exception();
            }

            var post = postRepository.GetById(postId);

            if (post == null)
            {
                throw new Exception();
            }

            post.Comments.Add(new Comment
            {
                User = commentUser,
                Content = content
            });

            postRepository.Update(post);
        }
    }
}
