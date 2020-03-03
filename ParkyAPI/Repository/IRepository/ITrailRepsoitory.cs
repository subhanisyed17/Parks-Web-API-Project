using ParkyAPI.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository.IRepository
{
    public interface ITrailRepsoitory
    {
        ICollection<Trail> GetTrails();

        ICollection<Trail> GetTrailsInNationalParkId(int npId);

        Trail GetTrail(int trailId);

        bool TrailExists(string name);

        bool TrailExists(int id);

        bool CreateTrail(Trail trail);

        bool UpdateTrail(Trail trail);

        bool DeleteTrail(Trail trail);

        bool Save();
    }
}
