using Microsoft.AspNetCore.Identity;

namespace LMS.Models
{
    public class LmsUser : IdentityUser
    {
        public string? EmployeeId { get; set; }

        public string? Department { get; set; }

        public string? Location { get; set; }

        public string? Position { get; set; }
    }
}