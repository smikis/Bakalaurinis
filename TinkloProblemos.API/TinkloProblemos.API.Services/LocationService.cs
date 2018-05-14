using System.Collections.Generic;
using TinkloProblemos.API.Contracts.Category;
using TinkloProblemos.API.Contracts.Location;
using TinkloProblemos.API.Interfaces.Repositories;
using TinkloProblemos.API.Interfaces.Services;

namespace TinkloProblemos.API.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        public LocationService(ILocationRepository lcoationRepository)
        {
            _locationRepository = lcoationRepository;
        }

        public bool Add(WriteLocation prod)
        {
            var location = _locationRepository.GetByUserId(prod.UserId);
            if (location != null)
            {
                var result = _locationRepository.Update(new UpdateLocation
                {
                    Lat = prod.Lat,
                    Lng = prod.Lng
                }, prod.UserId);
                return result != 0;
            }
            else
            {
                var result = _locationRepository.Add(prod);
                return result != 0;
            }
        }

        public GetLocation Get(string userId)
        {
            return _locationRepository.GetByUserId(userId);
        }
    }
}
