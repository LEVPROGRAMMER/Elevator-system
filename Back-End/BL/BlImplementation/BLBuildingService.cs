using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BlApi;
using BL.Bo;
using Dal;
using Dal.DalApi;
using Dal.Do;

namespace BL.BlImplementation
{
    
    
        public class BLBuildingService : IBLBuilding
        {
            DalManager Dal;
            public BLBuildingService(DalManager manager)
            {
                this.Dal = manager;
            }

        public List<BLBuilding> ReadAll() =>
                CastListToBl(Dal.Building.GetAll());

        public BLBuilding CastingToBl(Building dalbuilding)
            {
            BLBuilding b = new BLBuilding()
                {
                    Id = dalbuilding.Id,
                    UserId = dalbuilding.UserId,
                    Name = dalbuilding.Name,
                    NumberOfFloors = dalbuilding.NumberOfFloors,
                };
                return b;
            }

            public List<BLBuilding> CastListToBl(List<Building> list)
            {
                List<BLBuilding> lst = new List<BLBuilding>();
                list.ForEach(x => lst.Add(CastingToBl(x)));
                return lst;
            }

            public Building CastingToDal(BLBuilding blbuilding)
            {
            Building b = new Building()
                {
                    Id = blbuilding.Id,
                    UserId= blbuilding.UserId,
                    Name = blbuilding.Name,
                    NumberOfFloors = blbuilding.NumberOfFloors
                };
            return b;
            }

            public bool Create(BLBuilding blbuilding)
            {
                return Dal.Building.Create(CastingToDal(blbuilding));
            }

            public bool Delete(int blbuildingId)
            {
                return Dal.Building.Delete(Dal.Building.GetAll().Find(x => x.Id == blbuildingId));
            }

            public bool Update(BLBuilding blbuilding)
            {
                return Dal.Building.Update(CastingToDal(blbuilding));
            }

            public BLBuilding Read(int filter)
            {
                Building temp = Dal.Building.GetAll().Find(x => x.Id == filter);
                if (temp == null)
                    return null;
                return CastingToBl(temp);
            }

        }
 }

