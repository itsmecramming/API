namespace Student.Web.Api.Models
{
    public class Grade
    {
        // Primary Key
        public int Id { get; set; }

        // Foreign Key for PupilTable
        // If PupilId is required, keep it non-nullable and ensure it's set when creating a Grade object.
        // If it can be absent initially, make it nullable: public string? PupilId { get; set; }
        public string PupilId { get; set; } 

        // Foreign Key for Subject Table
        public int SubjectId { get; set; }

        // These properties can be nullable if they're not required initially
        public string? MidTerm { get; set; }
        public string? FinalTerm { get; set; }
        public string? FinalGrade { get; set; }
        public string? Remarks { get; set; }

        // Navigation Properties
        // If a Grade can exist without a linked Pupil or Subject initially, make these nullable.
        public Pupil? Pupil { get; set; }
        public Subject? Subject { get; set; }

        public Grade()
        {
            // Initialize string properties with default values if they are required
            PupilId = string.Empty; // or null if it's made nullable
            MidTerm = string.Empty;
            FinalTerm = string.Empty;
            FinalGrade = string.Empty;
            Remarks = string.Empty;
        }
    }
}
