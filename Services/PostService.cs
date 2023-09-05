using Blog.API.Dtos;
using Blog.API.Mappers;
using Blog.API.Models;
using Blog.API.Repositories;

namespace Blog.API.Services
{
    public interface IPostService
    {
        Task<IList<PostDto>> GetAll(int page, int size);
        Task<IList<PostDto>> GetFromWriter(string email, int page, int size);
        Task<IList<PostDto>> SearchByStatus(PostStatusEnum postStatusEnum, int page, int size);
        Task Create(CreatePostDto createPostDto, string email);
        Task Update(long postId, UpdatePostDto updatePostDto);
        Task Submit(long postId);
        Task Review(long postId, bool approved);
        Task AddComment(string email, long postId, string content);
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

        public async Task<IList<PostDto>> GetFromWriter(string email, int page, int size)
        {
            var user = await userService.GetUserByEmail(email);

            if (user is null)
            {
                throw new Exception();
            }

            var posts = postRepository.SearchByCondition(post => post.UserId == user.Id, page, size);

            return posts.Select(post => post.ToDto()).ToList();
        }


        public async Task<IList<PostDto>> SearchByStatus(PostStatusEnum postStatusEnum, int page, int size)
        {
            var postStatus = postStatusEnum.ToString().ToUpper();

            var posts = postRepository.SearchByCondition(post => post.Status == postStatus, page, size);

            return posts.Select(post => post.ToDto()).ToList();
        }

        public async Task Create(CreatePostDto postDto, string email)
        {
            var user = await userService.GetUserByEmail(email);
            var post = PostMapper.ToEntity(postDto);

            post.UserId = user.Id;

            postRepository.Insert(post);
        }

        public async Task Update(long postId, UpdatePostDto postDto)
        {
            var existingPost = postRepository.GetById(postId);

            if (existingPost is null)
            {
                throw new Exception();
            }

            if (existingPost.PostStatusEnum == PostStatusEnum.SUBMITTED || existingPost.PostStatusEnum == PostStatusEnum.PUBLISHED)
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

            if (post.PostStatusEnum != PostStatusEnum.CREATED)
            {
                throw new Exception();
            }

            post.Status = PostStatusEnum.SUBMITTED.ToString();

            postRepository.Update(post);
        }

        public async Task Review(long postId, bool approved)
        {
            var post = postRepository.GetById(postId);

            if (post is null)
            {
                throw new Exception();
            }

            if (post.PostStatusEnum != PostStatusEnum.SUBMITTED)
            {
                throw new Exception();
            }

            post.Status = (approved ? PostStatusEnum.PUBLISHED : PostStatusEnum.REJECTED).ToString();

            postRepository.Update(post);
        }

        public async Task AddComment(string email, long postId, string content)
        {
            var commentUser = await userService.GetUserByEmail(email);

            if (commentUser == null)
            {
                throw new Exception();
            }

            var post = postRepository.GetById(postId);

            if (post == null)
            {
                throw new Exception();
            }

            post.Comments = post.Comments == null ? new List<Comment>() : post.Comments;

            post.Comments.Add(new Comment
            {
                User = commentUser,
                Content = content
            });

            postRepository.Update(post);
        }
    }
}
