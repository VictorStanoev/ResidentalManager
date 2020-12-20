namespace ResidentalManager.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ResidentalManager.Data.Common.Repositories;
    using ResidentalManager.Data.Models;
    using ResidentalManager.Web.ViewModels.Administration.Dashboard;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Property> propertiesRepository;
        private readonly IRepository<RealEstate> realEstateRepository;

        public UsersService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Property> propertiesRepository,
            IRepository<RealEstate> realEstateRepository)
        {
            this.usersRepository = usersRepository;
            this.propertiesRepository = propertiesRepository;
            this.realEstateRepository = realEstateRepository;
        }

        public async Task Delete(string userId)
        {
            var user = this.usersRepository
                .All()
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            if (user != null)
            {
                this.usersRepository.Delete(user);
                await this.usersRepository.SaveChangesAsync();
            }
        }

        public async Task Edit(string userId, UsersInputModel model)
        {
            var user = this.usersRepository.All().Where(x => x.Id == userId).ToList().FirstOrDefault();
            user.PropertyId = model.PropertyId;
            user.RealEstateId = model.RealEstateId;

            this.usersRepository.Update(user);
            await this.usersRepository.SaveChangesAsync();
        }

        public UsersViewModel GetUser(string userId)
        {
            var properties = this.propertiesRepository.All().Select(x => new PropertyDropDown
            {
                Name = x.Number.ToString(),
                Id = x.Id,
            }).ToList();

            var realEstates = this.realEstateRepository.All().Select(x => new RealEstateDropDown
            {
                Name = x.Name,
                Id = x.Id,
            }).ToList();

            return this.usersRepository.All().Where(x => x.Id == userId).Select(x => new UsersViewModel
            {
                UserName = x.UserName,
                Email = x.Email,
                RealEstate = realEstates,
                RealEstateId = x.RealEstateId,
                PropertyId = x.PropertyId,
                Property = properties,
            }).ToList().FirstOrDefault();
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
