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

        public async Task<MarkerResponseTO> Add(MarkerRequestTO marker)
        {
            var m = _mapper.Map<Marker>(marker);

            if (!Validate(m))
            {
                throw new InvalidDataException("MARKER is not valid");
            }

            _context.Markers.Add(m);
            await _context.SaveChangesAsync();

            return _mapper.Map<MarkerResponseTO>(m);
        }

        public IList<MarkerResponseTO> GetAll()
        {
            return _context.Markers.Select(_mapper.Map<MarkerResponseTO>).ToList();
        }

        public async Task<MarkerResponseTO> Patch(int id, JsonPatchDocument<Marker> patch)
        {
            var target = await _context.FindAsync<Marker>(id)
                ?? throw new InvalidDataException($"MARKER {id} not found at PATCH {patch}");

            patch.ApplyTo(target);
            await _context.SaveChangesAsync();

            return _mapper.Map<MarkerResponseTO>(target);
        }

        public async Task<bool> Remove(int id)
        {
            var target = new Marker() { Id = id };

            _context.Remove(target);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<MarkerResponseTO> Update(MarkerRequestTO marker)
        {
            var m = _mapper.Map<Marker>(marker);

            if (!Validate(m))
            {
                throw new InvalidDataException($"UPDATE invalid data: {marker}");
            }

            _context.Update(m);
            await _context.SaveChangesAsync();

            return _mapper.Map<MarkerResponseTO>(m);
        }

        public async Task<MarkerResponseTO> GetByID(int id)
        {
            var a = await _context.Markers.FindAsync(id);

            return a is not null ? _mapper.Map<MarkerResponseTO>(a)
                : throw new ArgumentNullException($"Not found MARKER {id}");
        }

        public Task<IList<MarkerResponseTO>> GetByTweetID(int tweetId)
        {
            throw new NotImplementedException();
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
