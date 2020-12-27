namespace ResidentalManager.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ResidentalManager.Common;
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

        public async Task EditRealEstate(string userId, int realEstateId)
        {
            var user = this.usersRepository.All().Where(x => x.Id == userId).ToList().FirstOrDefault();
            user.RealEstateId = realEstateId;

            this.usersRepository.Update(user);
            await this.usersRepository.SaveChangesAsync();
        }

        public UsersViewModel GetUser(string userId)
        {
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
            }).ToList().FirstOrDefault();
        }

        public UsersViewModel GetUserProperty(string userId, int realEstateId)
        {
            var properties = this.propertiesRepository.All().Where(x => x.RealEstateId == realEstateId).Select(x => new PropertyDropDown
            {
                Name = x.Number.ToString(),
                Id = x.Id,
            }).ToList();

            var realEstateName = this.realEstateRepository.All().Where(x => x.Id == realEstateId).Select(x => x.Name).FirstOrDefault().ToString();

            return this.usersRepository.All().Where(x => x.Id == userId).Select(x => new UsersViewModel
            {
                UserName = x.UserName,
                Email = x.Email,
                RealEstateId = x.RealEstateId,
                RealEstateName = realEstateName,
                PropertyId = x.PropertyId,
                Property = properties,
            }).ToList().FirstOrDefault();
        }

        public async Task EditProperty(string userId, int propertyId)
        {
            var user = this.usersRepository.All().Where(x => x.Id == userId).ToList().FirstOrDefault();
            user.PropertyId = propertyId;

            this.usersRepository.Update(user);
            await this.usersRepository.SaveChangesAsync();
        }

        public IEnumerable<UsersViewModel> GetUsers()
        {
            return this.usersRepository.All()
                .Where(x => x.UserName != GlobalConstants.AdminUserName)
                .OrderByDescending(x => x.CreatedOn)
                .ToList()
                .Select(x => new UsersViewModel
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                PropertyId = x.PropertyId,
                RealEstateId = x.RealEstateId,
                RealEstateName = this.realEstateRepository.All().Any(r => r.Id == x.RealEstateId) ? this.realEstateRepository.All().Where(r => r.Id == x.RealEstateId).ToList().ToList().Select(r => r.Name).FirstOrDefault().ToString() : "not set",
                PropertyNumber = this.propertiesRepository.All().Any(r => r.Id == x.PropertyId) ? this.propertiesRepository.All().Where(r => r.Id == x.PropertyId).ToList().ToList().Select(r => r.Number).FirstOrDefault().ToString() : "not set",
            })
                .ToList();
        }
    }
}
