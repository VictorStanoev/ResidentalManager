namespace ResidentalManager.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ResidentalManager.Data.Common.Repositories;
    using ResidentalManager.Data.Models;
    using ResidentalManager.Web.ViewModels.Fees;
    using ResidentalManager.Web.ViewModels.Residents;

    public class ResidentsService : IResidentsService
    {
        private readonly IDeletableEntityRepository<Resident> resRepository;
        private readonly IDeletableEntityRepository<Fee> feeRepository;

        public ResidentsService(
            IDeletableEntityRepository<Resident> resRepository,
            IDeletableEntityRepository<Fee> feeRepository)
        {
            this.resRepository = resRepository;
            this.feeRepository = feeRepository;
        }

        public CreateResidentsInputModel AddFee()
        {
            var model = new CreateResidentsInputModel();
            var feeCollection = this.feeRepository.AllAsNoTracking()
                .Select(x => new FeeDropDown
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();
            model.Fee = feeCollection;

            return model;
        }

        public async Task CreateAsync(CreateResidentsInputModel inputModel)
        {
            var resident = new Resident()
            {
               FirstName = inputModel.FirstName,
               MiddleName = inputModel.MiddleName,
               LastName = inputModel.LastName,
               Telephone = inputModel.Telephone,
               Email = inputModel.Email,
               DateOfBirth = inputModel.DateOfBirth,
               FeeId = inputModel.FeeId,
               Gender = inputModel.Gender,
               PropertyId = inputModel.PropertyId,
               ResidentType = inputModel.ResidentType,
            };

            await this.resRepository.AddAsync(resident);
            await this.resRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var property = this.resRepository.All().Where(x => x.Id == id).FirstOrDefault();
            this.resRepository.Delete(property);
            await this.resRepository.SaveChangesAsync();
        }

        public AllResidentsViewModel Get(string residentId)
        {
            var fees = this.feeRepository.AllAsNoTracking().Select(x => new FeeDropDown
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            return this.resRepository.All()
                 .Where(x => x.Id == residentId)
                 .Select(x => new AllResidentsViewModel
                 {
                     Id = x.Id,
                     FirstName = x.FirstName,
                     MiddleName = x.MiddleName,
                     LastName = x.LastName,
                     DateOfBirth = x.DateOfBirth,
                     Email = x.Email,
                     Fee = fees,
                     Gender = x.Gender,
                     ResidentType = x.ResidentType,
                     Telephone = x.Telephone,
                     FeeId = x.FeeId,
                     PropertyId = x.PropertyId,
                 }).FirstOrDefault();
        }

        public IEnumerable<AllResidentsViewModel> GetAll(int propertyId)
        {
            var fees = this.feeRepository.AllAsNoTracking().Select(x => new FeeDropDown
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            var residents = this.resRepository.AllAsNoTracking()
                .Where(x => x.PropertyId == propertyId)
                .Select(x => new AllResidentsViewModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    DateOfBirth = x.DateOfBirth,
                    Email = x.Email,
                    Fee = fees,
                    Gender = x.Gender,
                    ResidentType = x.ResidentType,
                    Telephone = x.Telephone,
                    FeeId = x.FeeId,
                    PropertyId = x.PropertyId,
                }).ToList().AsEnumerable();

            return residents;
        }

        public async Task Update(string residentId, CreateResidentsInputModel inputModel)
        {
            var resident = this.resRepository.All().Where(x => x.Id == residentId).FirstOrDefault();
            resident.FirstName = inputModel.FirstName;
            resident.MiddleName = inputModel.MiddleName;
            resident.LastName = inputModel.LastName;
            resident.Telephone = inputModel.Telephone;
            resident.Email = inputModel.Email;
            resident.Gender = inputModel.Gender;
            resident.FeeId = inputModel.FeeId;
            resident.PropertyId = inputModel.PropertyId;
            resident.ResidentType = inputModel.ResidentType;
            this.resRepository.Update(resident);
            await this.resRepository.SaveChangesAsync();
        }
    }
}
