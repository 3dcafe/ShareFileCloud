using AutoMapper;
using DTO.Models;
using Entitles.Models;
using Microsoft.Extensions.Logging;

namespace DTO.Repository;

public class UsersRepository
{
    private readonly IMapper _mapper;
    private readonly ILogger<UsersRepository> _logger;

    private readonly DAL.Repository.UsersRepository _usersRepository;


    /// <summary>
    /// Base constructor
    /// </summary>
    public UsersRepository
    (
        IMapper mapper,
        ILogger<UsersRepository> logger
    )
    {
        _mapper = mapper;
        _logger = logger;
        _usersRepository = new DAL.Repository.UsersRepository(logger);
    }

    public async Task<ApplicationUserDTO?> FindByUserNameAsync(string? userName)
    {
        var users = await _usersRepository.FindAsync(new Func<ApplicationUser, bool>(x=>x.UserName == userName));
        return users.Select(x => _mapper.Map<ApplicationUserDTO?>(x)).FirstOrDefault();
    }

    public async Task<ApplicationUserDTO?> CreateAsync(ApplicationUserDTO user, string userId)
    {
        var dataUser = _mapper.Map<ApplicationUser>(user);
        if (dataUser.Id == null)
            dataUser.Id = Guid.NewGuid().ToString();
        var newUser = await _usersRepository.CreateAsync(dataUser, userId);
        return _mapper.Map<ApplicationUserDTO>(newUser);
    }
}
