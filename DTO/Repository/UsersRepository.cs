using AutoMapper;
using DTO.Models;
using Entitles.Models;

namespace DTO.Repository;

public class UsersRepository
{
    private readonly IMapper _mapper;

    private readonly DAL.Repository.UsersRepository _repository = new();


    /// <summary>
    /// Base constructor
    /// </summary>
    public UsersRepository
    (
        IMapper mapper
    )
    {
        _mapper = mapper;
    }

    public async Task<ApplicationUserDTO?> FindByUserNameAsync(string? userName)
    {
        var users = await _repository.FindAsync(new Func<ApplicationUser, bool>(x=>x.UserName == userName));
        return (ApplicationUserDTO?)users.Select(x => _mapper.Map<ApplicationUserDTO>(x));
    }
}
