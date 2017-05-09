using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VecinosUY.Data.Entities;
using VecinosUY.Data.DataAccess;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using VecinosUY.Exceptions;
using VecinosUY.Data.Repository;
using System.Net.Http;
using VecinosUY.Logger;

namespace VecinosUY.Logic
{
    public class BuildingValidator : IBuildingValidator
    {
        private readonly IUnitOfWork unitOfWork;
        public BuildingValidator(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<Building> GetBuildings()
        {
            return unitOfWork.BuildingRepository.Get();
        }

        public Building GetBuilding(string id)
        {
            Building building = null;
            building = unitOfWork.BuildingRepository.GetByID(id);
            if (building == null)
            {
                throw new NotExistException("El edificio especificado no existe");
            }
            return building;
        }

        public Building PutBuilding(string buildingId, Building building)
        {
            building.BuildingId = buildingId;
            Building oldBuilding = GetBuilding(buildingId);
            if (oldBuilding != null)
            {
                oldBuilding.Name = building.Name;
                oldBuilding.Address = building.Address;
                oldBuilding.Admin = building.Admin;
                unitOfWork.BuildingRepository.Update(oldBuilding);
                unitOfWork.Save();
            }
            else
            {
                throw new NotExistException("El edificio especificado no existe");
            }
            return building;
        }

        public void PostBuilding(Building building)
        {
            unitOfWork.BuildingRepository.Insert(building);
            unitOfWork.Save();
        }


        public void DeleteBuilding(string buildingId)
        {
            Building building = GetBuilding(buildingId);
            if (building != null)
            {
                unitOfWork.BuildingRepository.Update(building);
                unitOfWork.Save();
                //this.PutBuilding(buildingId, building);
            }
            else
            {
                throw new NotExistException("El edificio especificado no existe");
            }

        }

        private bool BuildingExists(int id)
        {
            return unitOfWork.BuildingRepository.GetByID(id) != null; ;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public void secure(HttpRequestMessage request)
        {
            Security.Security.secure(request);
        }

        public void AtmSecure(HttpRequestMessage request)
        {
            Security.Security.AtmSecure(request);
        }
    }
}
