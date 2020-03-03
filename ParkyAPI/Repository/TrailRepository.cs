using Microsoft.EntityFrameworkCore;
using ParkyAPI.data;
using ParkyAPI.models;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository
{
    public class TrailRepository : ITrailRepsoitory
    {
        private readonly ApplicationDbContext _parkyDb;

        public TrailRepository(ApplicationDbContext parkyDb)
        {
            _parkyDb = parkyDb;
        }
        public bool CreateTrail(Trail trail)
        {
            _parkyDb.Trails.Add(trail);
            return Save();
        }

        public bool Save()
        {
            return _parkyDb.SaveChanges() > 0 ? true : false;
        }

        public bool DeleteTrail(Trail trail)
        {
            _parkyDb.Trails.Remove(trail);
            return Save();
        }

        public Trail GetTrail(int TrailId)
        {
            return _parkyDb.Trails.Include(c => c.NationalPark).FirstOrDefault(np => np.Id == TrailId);
        }

        public ICollection<Trail> GetTrails()
        {
            return _parkyDb.Trails.Include(c => c.NationalPark).ToList();
        }

        public bool TrailExists(string name)
        {
            return _parkyDb.Trails.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public bool TrailExists(int id)
        {
            return _parkyDb.Trails.Any(x => x.Id == id);
        }

        public bool UpdateTrail(Trail trail)
        {
            _parkyDb.Trails.Update(trail);
            return Save();
        }

        public ICollection<Trail> GetTrailsInNationalParkId(int npId)
        {
            return _parkyDb.Trails.Include(c => c.NationalPark).Where(c=> c.NationalParkId == npId).ToList();
        }
    }
}
