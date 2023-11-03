using LAB.Models;
using System.ComponentModel.DataAnnotations;
using System.Net.Cache;

namespace LAB.ViewModel
{
	public class PatientViewModel
	{
		public int PatientId { get; set; }

		public int? PatientNidType { get; set; }

		public long? PatientNid { get; set; }

		public string PatientName { get; set; } = null!; 
		
		public string? PatientAddress { get; set; }

		public string? PatientTel1 { get; set; }

		public string? PatientTel2 { get; set; }

		public string? PatientEmail { get; set; }

		public string? PatientGender { get; set; }

		public DateTime? CreationDate { get; set; }
		public DateTime? Age { get; set; }
		public bool IsDeleted { get; set; }

		public DateTime? PatientBirthdate { get; set; }

		public string? PatientCountry { get; set; }

		public string? LastUpdateBy { get; set; }

		public DateTime? LastUpdateDate { get; set; }

		public string? LastUpdateFrom { get; set; }


	}
}
