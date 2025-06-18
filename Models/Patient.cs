namespace PatientAPI.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public int DocumentTypeId { get; set; }
        public int GenderId { get; set; }
    }
}
