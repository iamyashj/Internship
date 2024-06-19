namespace ResumeManagementApp.Entities
{
	public class JobPosting
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Location { get; set; }
		public decimal Salary { get; set; }
		public DateTime PostedDate { get; set; }
		public List<Skill> RequiredSkills { get; set; }
		public List<Education> RequiredEducations { get; set; }
		public List<Experience> RequiredExperiences { get; set; }
	}


}
