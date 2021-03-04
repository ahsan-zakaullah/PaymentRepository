using System.ComponentModel.DataAnnotations;

namespace Payment.Models.Common
{
    // Define the enum to set the status for payment
    public enum StatusEnum
    {
        [Display(Name = "Pending")]
        Pending = 1,
        [Display(Name = "Processed")]
        Processed = 2,
        [Display(Name = "Failed")]
        Failed = 3
    }
}
