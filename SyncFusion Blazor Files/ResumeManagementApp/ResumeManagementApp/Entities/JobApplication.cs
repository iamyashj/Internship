namespace ResumeManagementApp.Entities
{
	public class JobApplication
	{
		public int Id { get; set; }
		public int JobPostingId { get; set; }
		public int UserProfileId { get; set; }
		public string CoverLetter { get; set; }
		public DateTime AppliedDate { get; set; }
		public string ApplicationStatus { get; set; }
	}
}
