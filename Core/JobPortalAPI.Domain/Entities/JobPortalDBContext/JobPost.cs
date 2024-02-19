using JobPortalAPI.Domain.Entities.Common;

namespace JobPortalAPI.Domain.Entities.JobPortalDBContext
{
    public class JobPost: BaseEntity
    {
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string EmploymentType { get; set; }
        public float? MinSalary { get; set; }
        public float? MaxSalary { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public DateTime PublishedDate { get; set; }
        public Guid CompanyID { get; set; }
        public Guid LocationID { get; set; }
        public Guid? CategoryID { get; set; }
        public Category? Category { get; set; }
        public Company Company { get; set; }
        public Location Location { get; set; }
        public List<Application> Applications { get; set; }

        public ICollection<Skill>? RequiredSkills { get; set; }
    }

}
