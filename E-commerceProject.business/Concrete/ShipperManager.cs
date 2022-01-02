using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.business.Abstract;
using E_commerceProject.data.Abstract;
using E_commerceProject.entity;

namespace E_commerceProject.business.Concrete
{
    public class ShipperManager : IShipperService
    {
        // Dependency Injection

        private IShipperRepository _shipperRepository;

        public ShipperManager(IShipperRepository shipperRepository)
        {
            _shipperRepository = shipperRepository;

        }
        public List<Shipper> GetAll()
        {
            return _shipperRepository.GetAll();
        }
    }
}