using SpottedCotuca.API.Models;
using System.Collections.Generic;

namespace SpottedCotuca.API.Repositories
{
    public interface ISpotsRepository
    {
        Spot Read(long id);
        List<Spot> Read(Status status, int offset, int limit);
        void Create(Spot spot);
        void Update(Spot spot);
        void Delete(long id);
    }
}
