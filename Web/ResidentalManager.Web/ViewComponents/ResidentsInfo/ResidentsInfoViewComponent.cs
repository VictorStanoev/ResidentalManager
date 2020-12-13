namespace ResidentalManager.Web.ViewComponents.ResidentsInfo
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using ResidentalManager.Data.Common.Repositories;
    using ResidentalManager.Data.Models;
    using ResidentalManager.Data.Models.Enum;

    public class ResidentsInfoViewComponent : ViewComponent
    {
        private readonly IDeletableEntityRepository<Resident> residentsRepository;
        private readonly IDeletableEntityRepository<Pet> petsRepository;

        public ResidentsInfoViewComponent(
            IDeletableEntityRepository<Resident> residentsRepository,
            IDeletableEntityRepository<Pet> petsRepository)
        {
            this.residentsRepository = residentsRepository;
            this.petsRepository = petsRepository;
        }

        public IViewComponentResult Invoke(int realEstateId)
        {
            var residents = this.residentsRepository
                .All()
                .Where(x => x.Property.RealEstateId == realEstateId)
                .ToList();

            var totalPets = this.petsRepository
                .All()
                .Where(x => x.Property.RealEstateId == realEstateId)
                .Select(x => x.Id)
                .Count()
                .ToString();

            var totalResidents = residents
                .Select(x => x.Id)
                .Count()
                .ToString();

            var totalMales = residents
                .Count(x => x.Gender == Gender.Male)
                .ToString();

            var totalFemales = residents
                .Count(x => x.Gender == Gender.Female)
                .ToString();

            var below18 = residents
                .Where(x => x.DateOfBirth != null)
                .Where(x => x.DateOfBirth.Value.AddYears(18) > DateTime.UtcNow)
                .Select(x => x.Id)
                .Count()
                .ToString();

            var above65 = residents
                .Where(x => x.DateOfBirth != null)
                .Where(x => x.DateOfBirth.Value.AddYears(65) < DateTime.UtcNow)
                .Count()
                .ToString();

            var between18and65 = residents
                .Where(x => x.DateOfBirth != null)
                .Where(x => x.DateOfBirth.Value.AddYears(18) <= DateTime.UtcNow && x.DateOfBirth.Value.AddYears(65) >= DateTime.UtcNow)
                .Select(x => x.Id)
                .Count()
                .ToString();

            var model = new ResidentsInfoViewModel()
            {
                TotalResidents = totalResidents,
                TotalPets = totalPets,
                Females = totalFemales,
                Males = totalMales,
                Youth = below18,
                MiddleAged = between18and65,
                Old = above65,
            };

            return this.View(model);
        }
    }
}
