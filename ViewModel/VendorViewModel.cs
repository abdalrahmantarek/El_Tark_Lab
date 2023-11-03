using LAB.Models;
using System.ComponentModel.DataAnnotations;

namespace LAB.ViewModel
{
    public class VendorViewModel
    {
        public int VendorId { get; set; }
        [Required]
        public string VendorName { get; set; } = null!;
		[Required]
		public string VendorAddress { get; set; } = null!;
		[Required]
        [DataType(dataType:DataType.Text)]
		public int VendorTel { get; set; }
		[DataType(dataType: DataType.Text)]
		public int? VendorTel2 { get; set; }

        public string? VendorWebsite { get; set; }

        public string? OfficialEmail { get; set; }

        public DateTime? CreationDate { get; set; }

        public int VendorCat { get; set; }

        public string? LastUpdateBy { get; set; }

        public string? LastUpdateFrom { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public List<VendorCategory>? VendorCategories {get; set;} = new List<VendorCategory>();
    }
}
