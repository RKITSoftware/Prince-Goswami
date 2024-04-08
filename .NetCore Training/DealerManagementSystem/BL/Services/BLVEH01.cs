using DealerManagementSystem.BL.Interface.Service;
using DealerManagementSystem.DAL;
using DealerManagementSystem.Models;

namespace DealerManagementSystem.BL.Services
{

    public class BLVEH01 : IBLVEH01
    {
        private readonly IVEH01_DAL _vehicleRepository;

        public BLVEH01(VEH01Repository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        ///<inheritdoc/>
        public void AddVehicle(VEH01 vehicle)
        {
            _vehicleRepository.Add(vehicle);
        }

        ///<inheritdoc/>
        public void UpdateVehicle(VEH01 vehicle)
        {
            _vehicleRepository.Update(vehicle);
        }

        ///<inheritdoc/>>
        public void RemoveVehicle(int vehicleId)
        {
            _vehicleRepository.Delete(vehicleId);
        }

        ///<inheritdoc/>
        public VEH01 GetVehicleById(int vehicleId)
        {
            return _vehicleRepository.GetByID(vehicleId);
        }

        ///<inheritdoc/>
        public List<VEH01> GetAllVehicles()
        {
            return _vehicleRepository.GetAll();
        }

        /////<inheritdoc/>
        //public  List<VEH01> SearchVehicles(string searchCriteria)
        //{
        //    return  _vehicleRepository.Search(searchCriteria);
        //}

        ///<inheritdoc/>
        public bool VehicleExists(int vehicleId)
        {
            if (vehicleId < 0) return false;
            if (_vehicleRepository.GetByID(vehicleId) != null) return true;
            return true;
        }
    }

}
