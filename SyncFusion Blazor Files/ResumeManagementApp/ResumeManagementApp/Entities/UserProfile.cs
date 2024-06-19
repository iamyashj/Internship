namespace ResumeManagementApp.Entities
{
	public class UserProfile
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public string ResumeUrl { get; set; }
		public List<Skill> Skills { get; set; }
		public List<Education> Educations { get; set; }
		public List<Experience> Experiences { get; set; }
	}
}
