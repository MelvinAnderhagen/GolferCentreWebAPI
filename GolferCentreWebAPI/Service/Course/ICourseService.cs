using GolferCentreWebAPI.DTO.Course;

namespace GolferCentreWebAPI.Service.Course
{
    public interface ICourseService
    {
        ICollection<GetCourseDTO> GetAllCourses();
        GetCourseDTO GetCourse(Guid id);
    }
}
