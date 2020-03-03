using ParkyAPI.data;
using ParkyAPI.models;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {

        private readonly ApplicationDbContext _parkyDb;

        public NationalParkRepository(ApplicationDbContext parkyDb)
        {
            _parkyDb = parkyDb;
        }
        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _parkyDb.NationalParks.Add(nationalPark);
            return Save();
        }

        public bool Save()
        {
            return _parkyDb.SaveChanges() > 0? true: false;
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _parkyDb.NationalParks.Remove(nationalPark);
            return Save();
        }

        public NationalPark GetNationalPark(int nationalParkId)
        {
            return _parkyDb.NationalParks.FirstOrDefault(x => x.Id == nationalParkId);
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return _parkyDb.NationalParks.ToList();
        }

        public bool NationalParkExists(string name)
        {
            return _parkyDb.NationalParks.Any(x=>x.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public bool NationalParkExists(int id)
        {
            return _parkyDb.NationalParks.Any(x => x.Id == id);
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            _parkyDb.NationalParks.Update(nationalPark);
            return Save();
        }
    }
}
