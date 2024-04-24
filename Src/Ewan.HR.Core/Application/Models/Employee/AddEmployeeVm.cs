using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Ewan.HR.Core.Application.Models
{
    public class AddEmployeeVm
    {
        [Required]
        public string EmployeeNumber { get; set; }

        [Required]
        public string NationalId { get; set; } = "0";

        public string Position { get; set; } = "0";

        public string Department { get; set; } = "0";
        public string DirectManagerId { get; set; }
        public string Location { get; set; } = "0";

        [Required]
        [DataType(DataType.Date)]
        // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ResumptionDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ContractStartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ContractEndDate { get; set; }

        public string Nationality { get; set; } = "0";

        public string Gender { get; set; } = "0";

        [Required]
        public string FristName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string EmployeeId { get; set; }

        public string Photo { get; set; }

        public IFormFile image { get; set; }



    }
}
