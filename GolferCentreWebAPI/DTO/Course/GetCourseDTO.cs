namespace GolferCentreWebAPI.DTO.Course
{
    public class GetCourseDTO
    {
        public Guid CourseId { get; set; }

        public string CourseName { get; set; }

        public string Location { get; set; }

        public int Par { get; set; }
    }
}
