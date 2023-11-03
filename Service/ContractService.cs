using LAB.IService;
using LAB.Models;
using Microsoft.EntityFrameworkCore;

namespace LAB.Service
{
	public class ContractService:IBaseService<VendorContract>
	{
		HMISContext context;
		public  ContractService(HMISContext context)
		{
			this.context = context;
		}

		public void  Add( VendorContract t)
		{
			t.LastUpdateBy = "me";
			t.LastUpdateFrom = ":::1";
			t.LastUpdateDate = DateTime.Now;
			t.BranchId = 1;
			context.Add(t);
		}

		public IEnumerable<VendorContract> GetAll()
		{
			throw new NotImplementedException();
		}

		public VendorContract GetById(int id)
		{
			return context.VendorContracts.FirstOrDefault(x => x.ContractId == id);
		}
		public VendorContract GetLastValidContractByVendorId(int vendorId)
		{
			var result = context.VendorContracts.Where(x => x.VendorId == vendorId).OrderByDescending(x => x.EndDate).FirstOrDefault();
			return result;
		}

		public VendorContract Search(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(VendorContract t)
		{
			context.Update(t);
		}
	}
}
