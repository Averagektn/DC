using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using REST.Entity.Db;
using REST.Entity.DTO.RequestTO;
using REST.Entity.DTO.ResponseTO;
using REST.Service.Interface;
using REST.Storage.Common;

namespace REST.Service.Implementation
{
    public class MarkerService(DbStorage dbStorage, IMapper mapper) : IMarkerService
    {
        private readonly DbStorage _context = dbStorage;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> Add(MarkerRequestTO marker)
        {
            var m = _mapper.Map<Marker>(marker);

            if (!Validate(m))
            {
                return false;
            }

            try
            {
                _context.Markers.Add(m);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public IList<MarkerResponseTO> GetAll()
        {
            var res = new List<MarkerResponseTO>();

            foreach (var m in _context.Markers)
            {
                res.Add(_mapper.Map<MarkerResponseTO>(m));
            }

            return res;
        }

        public async Task<bool> Patch(int id, JsonPatchDocument<Marker> patch)
        {
            var target = _context.Find<Marker>(id);
            if (target is null)
            {
                return false;
            }

            try
            {
                patch.ApplyTo(target);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Remove(int id)
        {
            var target = new Marker() { Id = id };

            try
            {
                _context.Remove(target);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> Update(MarkerRequestTO marker)
        {
            var m = _mapper.Map<Marker>(marker);

            if (!Validate(m))
            {
                return false;
            }

            try
            {
                _context.Update(m);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        private static bool Validate(Marker marker)
        {
            var nameLen = marker.Name.Length;

            if (nameLen < 2 || nameLen > 32)
            {
                return false;
            }
            return true;
        }
    }
}
