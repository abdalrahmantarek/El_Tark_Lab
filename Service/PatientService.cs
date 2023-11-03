using LAB.IService;
using LAB.Models;
using LAB.Models;
using LAB.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace LAB.Service
{
    public class PatientService : IBaseService<Patient>
    {
        #region Field 


        HMISContext context;

        #endregion

        public PatientService(HMISContext _context)
        {
            context = _context;
        }

        public IEnumerable<Patient> GetAll()
        {
            return context.Set<Patient>().ToList();// context.Patients.ToList();
		}


        public Patient GetById(int id)
		{
            
			return context.Set<Patient>().Find(id);
		}


		public void Add(Patient patient)
		{
			context.Add(patient);
		}
		public void Update(Patient t)
		{
			context.Patients.Update(t);
			context.SaveChanges();
		}
		//public List<Patient> GlobalSearch(string name)
		//{
		//	var patients = context.Patients
		//		.Where(p => p.PatientName.Contains(name) || p.PatientEmail.Contains(name))
		//		.ToList();
		//	return patients;
		//}

		internal void Update(PatientViewModel v)
		{
			throw new NotImplementedException();
		}

		public Patient Search(int id)
		{
			throw new NotImplementedException();
		}
	}
}
