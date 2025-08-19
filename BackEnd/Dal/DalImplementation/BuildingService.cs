using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Dal.DalApi;
using Dal.Do;

namespace Dal.DalImplementation
{
    public class BuildingService : IBuilding
    {
        private readonly IDbConnection _db;

        public BuildingService(IDbConnection db)
        {
            _db = db;
        }

        public bool Create(Building item)
        {
            const string sql = @"INSERT INTO Buildings (Name, NumberOfFloors, UserId) 
                                 VALUES (@Name, @NumberOfFloors, @UserId)";
            try
            {
                int rows = _db.Execute(sql, item);
                return rows > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Create] Error: {ex.Message}");
                return false;
            }
        }

        public bool Delete(Building item)
        {
            const string sql = @"DELETE FROM Building WHERE Id = @Id";
            try
            {
                int rows = _db.Execute(sql, new { item.Id });
                return rows > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Delete] Error: {ex.Message}");
                return false;
            }
        }

        public List<Building> GetAll()
        {
            const string sql = @"SELECT * FROM Building";
            return _db.Query<Building>(sql).ToList();
        }

        public List<Building> Read(Predicate<Building> filter)
        {
            const string sql = @"SELECT * FROM Building";
            var buildings = _db.Query<Building>(sql).ToList();
            return buildings.FindAll(x => filter(x));
        }

        public bool Update(Building item)
        {
            const string sql = @"UPDATE Building
                                 SET Name = @Name, 
                                     NumberOfFloors = @NumberOfFloors, 
                                     UserId = @UserId
                                 WHERE Id = @Id";
            try
            {
                int rows = _db.Execute(sql, item);
                return rows > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Update] Error: {ex.Message}");
                return false;
            }
        }
    }
}
