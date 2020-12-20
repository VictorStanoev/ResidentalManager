namespace ResidentalManager.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using ResidentalManager.Data.Common.Repositories;
    using ResidentalManager.Data.Models;
    using ResidentalManager.Web.ViewModels.Administration.Dashboard;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public IEnumerable<UsersViewModel> GetUsers()
        {
            return this.usersRepository.All().Select(x => new UsersViewModel
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                PropertyId = x.PropertyId,
                RealEstateId = x.RealEstateId,
            }).ToList();
        }
    }
}
