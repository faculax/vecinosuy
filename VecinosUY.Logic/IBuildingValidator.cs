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

namespace VecinosUY.Logic
{
    public interface IBuildingValidator : IDisposable
    {

        IEnumerable<Building> GetBuildings();
        Building GetBuilding(string id);
        Building PutBuilding(string buildingId, Building building);
        void PostBuilding(Building building);
        void DeleteBuilding(string BuildingId);

        void secure(HttpRequestMessage request);

        void AtmSecure(HttpRequestMessage request);
    }
}
