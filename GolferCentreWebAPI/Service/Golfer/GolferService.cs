using GolferCentreWebAPI.DTO.Golfer;
using GolferCentreWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GolferCentreWebAPI.Service.Golfer
{

    public class GolferService : IGolferService
    {
        private readonly GolferGoContext _context;
        public GolferService(GolferGoContext context)
        {
            _context = context;
        }

        public ICollection<GetGolferDTO> GetAllGolfers()
        {
            var golfers = _context.Golfers.Select(g => new GetGolferDTO
            {
                GolferId = g.GolferId,
                Firstname = g.FirstName,
                Lastname = g.LastName,
                Handicap = g.Handicap,
                Country = g.Country
            }).ToList();

            if (golfers == null)
            {
                return null;
            }

            return golfers;
        }

        public GetGolferDTO GetGolfer(Guid id)
        {
            var golfer = _context.Golfers.Select(g => new GetGolferDTO
            {
                GolferId = g.GolferId,
                Firstname = g.FirstName,
                Lastname = g.LastName,
                Handicap = g.Handicap,
                Country = g.Country
            }).FirstOrDefault(golfer => golfer.GolferId == id);

            if (golfer == null)
            {
                return null;
            }

            return golfer;
        }

        public bool CreateGolfer(CreateGolferDTO createGolferDTO)
        {
            // Check if a golfer with the same first and last name already exists
            var existingGolfer = _context.Golfers
                .FirstOrDefault(g => g.FirstName == createGolferDTO.FirstName && g.LastName == createGolferDTO.LastName);

            if (existingGolfer != null)
            {
                // Return false if the golfer already exists
                return false;
            }

            // Create a new Golfer instance
            var newGolfer = new Models.Golfer
            {
                FirstName = createGolferDTO.FirstName,
                LastName = createGolferDTO.LastName,
                Handicap = createGolferDTO.Handicap,
                Country = createGolferDTO.Country
                // GolferId will be set automatically via Guid.NewGuid()
            };

            // Add the new golfer to the context
            _context.Golfers.Add(newGolfer);

            // Save changes to the database
            var result = _context.SaveChanges();

            // Return true if at least one row was affected
            return result > 0;
        }

        public bool DeleteGolfer(Guid id)
        {
            try
            {
                var golfer = _context.Golfers
                    .Include(g => g.Scores) 
                    .FirstOrDefault(g => g.GolferId == id);

                // Check if golfer exists
                if (golfer == null)
                {
                    Console.WriteLine($"Golfer with ID {id} not found.");
                    return false;
                }

                if (golfer.Scores.Any())
                {
                    Console.WriteLine($"Deleting {golfer.Scores.Count} related scores for golfer with ID {id}");
                    _context.Scores.RemoveRange(golfer.Scores);
                }

                // Remove the golfer from the context
                _context.Golfers.Remove(golfer);

                // Save changes to the database
                int changes = _context.SaveChanges();

                Console.WriteLine($"Deleted golfer with ID {id}. Changes made: {changes}");
                return changes > 0;
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Database error occurred while deleting golfer: {dbEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting golfer: {ex.Message}");
                return false;
            }
        }

        public bool UpdateGolfer(UpdateGolferDTO updateGolferDTO)
        {
            throw new NotImplementedException();
        }
    }
}
