using GolferCentreWebAPI.DTO.Course;
using GolferCentreWebAPI.Models;

namespace GolferCentreWebAPI.Service.Course
{
    public class CourseService : ICourseService
    {
        private readonly GolferGoContext _context;
        public CourseService(GolferGoContext context)
        {
            _context = context;
        }

        public ICollection<GetCourseDTO> GetAllCourses()
        {
            try
            {
                var courses = _context.Courses.Select(course => new GetCourseDTO
                {
                    CourseId = course.CourseId,
                    CourseName = course.CourseName,
                    Par = course.Par,
                    Location = course.Location
                }).ToList();

                if (courses == null)
                {
                    Console.WriteLine("Courses are null.");
                    return null;
                }

                return courses;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error message: " + ex.Message);
                return null;
            }
        }

        public GetCourseDTO GetCourse(Guid id)
        {
            try
            {
                var course = _context.Courses.Select(course => new GetCourseDTO
                {
                    CourseId = course.CourseId,
                    CourseName = course.CourseName,
                    Par = course.Par,
                    Location = course.Location
                }).FirstOrDefault(course => course.CourseId == id);

                if (course == null)
                {
                    Console.WriteLine("Course not found.");
                    return null;
                }

                return course;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error message: " + ex.Message);
                return null;
            }
        }
    }
}
