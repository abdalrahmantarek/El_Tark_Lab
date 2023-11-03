namespace LAB.ViewModel
{
	public class ServiceStatusViewModel
	{
		public class ServiceViewModel
		{
			public int? BarcodeId { get; set; }
			public bool? IsCollected { get; set; }
			public bool? IsAnalisis { get; set; }
			public bool? IsCompleted { get; set; }
		}
	}
}
