using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LAB.Models;

public partial class HMISContext : DbContext
{
    public HMISContext()
    {
    }

    public HMISContext(DbContextOptions<HMISContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ContactsOccupation> ContactsOccupations { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<DaysOfTheWeek> DaysOfTheWeeks { get; set; }

    public virtual DbSet<DoctorPayingRule> DoctorPayingRules { get; set; }

    public virtual DbSet<DurationUnit> DurationUnits { get; set; }

    public virtual DbSet<EmployeeShift> EmployeeShifts { get; set; }

    public virtual DbSet<Employess> Employesses { get; set; }

    public virtual DbSet<EmployessContract> EmployessContracts { get; set; }

    public virtual DbSet<EmployessContractType> EmployessContractTypes { get; set; }

    public virtual DbSet<InvoicesPaying> InvoicesPayings { get; set; }

    public virtual DbSet<NormalRange> NormalRanges { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientInvoice> PatientInvoices { get; set; }

    public virtual DbSet<PatientInvoiceDetail> PatientInvoiceDetails { get; set; }

    public virtual DbSet<RefferedDoctor> RefferedDoctors { get; set; }

    public virtual DbSet<RoportResult> RoportResults { get; set; }

    public virtual DbSet<SampleLog> SampleLogs { get; set; }

    public virtual DbSet<SampleStatus> SampleStatuses { get; set; }

    public virtual DbSet<ServiceBarcode> ServiceBarcodes { get; set; }

    public virtual DbSet<ServicesCategory> ServicesCategories { get; set; }

    public virtual DbSet<ServicesPricelist> ServicesPricelists { get; set; }

    public virtual DbSet<StandardService> StandardServices { get; set; }

    public virtual DbSet<Syndicate> Syndicates { get; set; }

    public virtual DbSet<SyndicateService> SyndicateServices { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    public virtual DbSet<VendorCategory> VendorCategories { get; set; }

    public virtual DbSet<VendorContact> VendorContacts { get; set; }

    public virtual DbSet<VendorContract> VendorContracts { get; set; }

    public virtual DbSet<VendorContractType> VendorContractTypes { get; set; }

    public virtual DbSet<Visit> Visits { get; set; }

    public virtual DbSet<VisitService> VisitServices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=LAB;Integrated Security=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactsOccupation>(entity =>
        {
            entity.HasKey(e => e.OccupationId);

            entity.ToTable("contacts_occupation");

            entity.Property(e => e.OccupationId).HasColumnName("occupation_id");
            entity.Property(e => e.OccupationName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_100_CI_AI")
                .HasColumnName("occupation_name");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.CId);

            entity.Property(e => e.CId).HasColumnName("c_id");
            entity.Property(e => e.CurrencyName)
                .HasMaxLength(50)
                .HasColumnName("currency_name");
        });

        modelBuilder.Entity<DaysOfTheWeek>(entity =>
        {
            entity.HasKey(e => e.DayId);

            entity.ToTable("days_of_the_week");

            entity.Property(e => e.DayId)
                .ValueGeneratedNever()
                .HasColumnName("day_id");
            entity.Property(e => e.DayName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("day_name");
            entity.Property(e => e.DayNameAr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .UseCollation("Arabic_100_CI_AI")
                .HasColumnName("day_name_ar");
        });

        modelBuilder.Entity<DoctorPayingRule>(entity =>
        {
            entity.HasKey(e => e.RuleId);

            entity.ToTable("Doctor_Paying_Rule");

            entity.Property(e => e.RuleId).HasColumnName("rule_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.DocContractId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("doc_contract_id");
            entity.Property(e => e.DocId).HasColumnName("doc_id");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(50)
                .HasColumnName("last_update_by");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("last_update_date");
            entity.Property(e => e.LastUpdateFrom)
                .HasMaxLength(50)
                .HasColumnName("last_update_from");
            entity.Property(e => e.Range).HasColumnName("range");
            entity.Property(e => e.ReferredDoc).HasColumnName("referred_doc");
            entity.Property(e => e.Repeatation).HasColumnName("repeatation");
            entity.Property(e => e.RuleStatus)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("rule_status");
            entity.Property(e => e.Serial).HasColumnName("serial");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<DurationUnit>(entity =>
        {
            entity.HasKey(e => e.DurationId);

            entity.ToTable("Duration_Units");

            entity.Property(e => e.DurationId).HasColumnName("duration_id");
            entity.Property(e => e.Duration)
                .HasMaxLength(50)
                .HasColumnName("duration");
            entity.Property(e => e.DurationAr)
                .HasMaxLength(50)
                .HasColumnName("duration_ar");
            entity.Property(e => e.DurationDays).HasColumnName("duration_days");
        });

        modelBuilder.Entity<EmployeeShift>(entity =>
        {
            entity.HasKey(e => e.ShiftId);

            entity.ToTable("Employee_shifts");

            entity.Property(e => e.ShiftId).HasColumnName("shift_id");
            entity.Property(e => e.DepId).HasColumnName("dep_id");
            entity.Property(e => e.EmpId).HasColumnName("emp_id");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("end_date");
            entity.Property(e => e.Floor).HasColumnName("floor");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_update_by");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("last_update_date");
            entity.Property(e => e.LastUpdateFrom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_update_from");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.ShiftDay).HasColumnName("shift_day");
            entity.Property(e => e.ShiftEnd).HasColumnName("shift_end");
            entity.Property(e => e.ShiftStart).HasColumnName("shift_start");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("start_date");
        });

        modelBuilder.Entity<Employess>(entity =>
        {
            entity.HasKey(e => e.EmployeeId);

            entity.ToTable("Employess");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.DepartId).HasColumnName("depart_id");
            entity.Property(e => e.EmployeeAddress)
                .HasMaxLength(150)
                .HasColumnName("employee_address");
            entity.Property(e => e.EmployeeEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("employee_email");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(150)
                .HasColumnName("employee_name");
            entity.Property(e => e.EmployeeNid)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("employee_NID");
            entity.Property(e => e.EmployeeTel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("employee_tel");
            entity.Property(e => e.EmployeeTel2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("employee_tel2");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("gender");
            entity.Property(e => e.HiringDate)
                .HasColumnType("datetime")
                .HasColumnName("hiring_date");
        });

        modelBuilder.Entity<EmployessContract>(entity =>
        {
            entity.HasKey(e => e.ContractId);

            entity.ToTable("Employess_Contracts");

            entity.Property(e => e.ContractId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contract_id");
            entity.Property(e => e.ContractTypeId).HasColumnName("contract_type_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.JobCat).HasColumnName("job_cat");
            entity.Property(e => e.JobId).HasColumnName("job_id");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_update_by");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("last_update_date");
            entity.Property(e => e.LastUpdateFrom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_update_from");
            entity.Property(e => e.Note)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("note");
            entity.Property(e => e.SecializationId).HasColumnName("secialization_id");
            entity.Property(e => e.SpecializationCat).HasColumnName("specialization_cat");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
        });

        modelBuilder.Entity<EmployessContractType>(entity =>
        {
            entity.HasKey(e => e.ContractId);

            entity.ToTable("Employess_contract_types");

            entity.Property(e => e.ContractId)
                .ValueGeneratedNever()
                .HasColumnName("contract_id");
            entity.Property(e => e.ContractName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_100_CI_AI")
                .HasColumnName("contract_name");
        });

        modelBuilder.Entity<InvoicesPaying>(entity =>
        {
            entity.HasKey(e => e.PayingId);

            entity.ToTable("Invoices_Paying");

            entity.Property(e => e.PayingId).HasColumnName("paying_id");
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_update_by");
            entity.Property(e => e.LastUpdateFrom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_update_from");
            entity.Property(e => e.PayingDate)
                .HasColumnType("datetime")
                .HasColumnName("paying_date");
            entity.Property(e => e.PayingValue).HasColumnName("paying_value");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoicesPayings)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoices_Paying_Patient_invoices");
        });

        modelBuilder.Entity<NormalRange>(entity =>
        {
            entity.ToTable("Normal_Range");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AgeRange)
                .HasMaxLength(50)
                .HasColumnName("age_range");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("gender");
            entity.Property(e => e.NormalRange1)
                .HasMaxLength(50)
                .HasColumnName("normal_range");
            entity.Property(e => e.Standref).HasColumnName("standref");

            entity.HasOne(d => d.StandrefNavigation).WithMany(p => p.NormalRanges)
                .HasForeignKey(d => d.Standref)
                .HasConstraintName("FK_Normal_Range_Standard_Services");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("creation_date");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_update_by");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("last_update_date");
            entity.Property(e => e.LastUpdateFrom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_update_from");
            entity.Property(e => e.PatientAddress)
                .HasMaxLength(250)
                .HasColumnName("patient_address");
            entity.Property(e => e.PatientBirthdate)
                .HasColumnType("date")
                .HasColumnName("patient_birthdate");
            entity.Property(e => e.PatientCountry)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("patient_country");
            entity.Property(e => e.PatientEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("patient_email");
            entity.Property(e => e.PatientGender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("patient_gender");
            entity.Property(e => e.PatientName)
                .HasMaxLength(100)
                .HasColumnName("patient_name");
            entity.Property(e => e.PatientNid).HasColumnName("patient_NID");
            entity.Property(e => e.PatientNidType).HasColumnName("patient_NID_type");
            entity.Property(e => e.PatientTel1)
                .HasMaxLength(50)
                .HasColumnName("patient_tel1");
            entity.Property(e => e.PatientTel2)
                .HasMaxLength(50)
                .HasColumnName("patient_tel2");
        });

        modelBuilder.Entity<PatientInvoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId);

            entity.ToTable("Patient_invoices");

            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("creation_date");
            entity.Property(e => e.Currency)
                .HasMaxLength(50)
                .HasColumnName("currency");
            entity.Property(e => e.InvoiceCost).HasColumnName("invoice_cost");
            entity.Property(e => e.InvoiceNotes)
                .HasMaxLength(250)
                .HasColumnName("invoice_notes");
            entity.Property(e => e.InvoiceStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("invoice_status");
            entity.Property(e => e.IsSupply).HasColumnName("is_supply");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(50)
                .HasColumnName("last_update_by");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("last_update_date");
            entity.Property(e => e.LastUpdateFrom)
                .HasMaxLength(50)
                .HasColumnName("last_update_from");
            entity.Property(e => e.PaidValue).HasColumnName("paid_value");
            entity.Property(e => e.PaymentType)
                .HasMaxLength(50)
                .HasColumnName("payment_type");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");

            entity.HasOne(d => d.Visit).WithMany(p => p.PatientInvoices)
                .HasForeignKey(d => d.VisitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patient_invoices_Visits");
        });

        modelBuilder.Entity<PatientInvoiceDetail>(entity =>
        {
            entity.ToTable("Patient_Invoice_Details");

            entity.Property(e => e.PatientInvoiceDetailId).HasColumnName("patient_invoice_detail_id");
            entity.Property(e => e.InvId).HasColumnName("inv_id");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(50)
                .HasColumnName("last_update_by");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("last_update_date");
            entity.Property(e => e.LastUpdateFrom)
                .HasMaxLength(50)
                .HasColumnName("last_update_from");
            entity.Property(e => e.Serial).HasColumnName("serial");
            entity.Property(e => e.ServiceCost).HasColumnName("service_cost");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");

            entity.HasOne(d => d.Inv).WithMany(p => p.PatientInvoiceDetails)
                .HasForeignKey(d => d.InvId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patient_Invoice_Details_Patient_invoices");

            entity.HasOne(d => d.Service).WithMany(p => p.PatientInvoiceDetails)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patient_Invoice_Details_Services_Pricelist");

            entity.HasOne(d => d.Visit).WithMany(p => p.PatientInvoiceDetails)
                .HasForeignKey(d => d.VisitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patient_Invoice_Details_Visits");
        });

        modelBuilder.Entity<RefferedDoctor>(entity =>
        {
            entity.HasKey(e => e.DrId);

            entity.ToTable("Reffered_Doctors");

            entity.Property(e => e.DrId).HasColumnName("dr_id");
            entity.Property(e => e.ClinicAddress)
                .HasMaxLength(100)
                .HasColumnName("clinic_address");
            entity.Property(e => e.DoctorName)
                .HasMaxLength(50)
                .HasColumnName("doctor_name");
            entity.Property(e => e.DoctorRules).HasColumnName("doctor_rules");
            entity.Property(e => e.DoctorSpecialization).HasColumnName("doctor_specialization");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_update_by");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("last_update_date");
            entity.Property(e => e.LastUpdateFrom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_update_from");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<RoportResult>(entity =>
        {
            entity.ToTable("Roport_result");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BarcodeId).HasColumnName("barcode_id");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(50)
                .HasColumnName("last_update_by");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("last_update_date");
            entity.Property(e => e.LastUpdateFrom)
                .HasMaxLength(50)
                .HasColumnName("last_update_from");
            entity.Property(e => e.NormalRangeId).HasColumnName("normal_range_id");
            entity.Property(e => e.Result).HasColumnName("result");

            entity.HasOne(d => d.Barcode).WithMany(p => p.RoportResults)
                .HasForeignKey(d => d.BarcodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Roport_result_Service_barcode");

            entity.HasOne(d => d.NormalRange).WithMany(p => p.RoportResults)
                .HasForeignKey(d => d.NormalRangeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Roport_result_Normal_Range");
        });

        modelBuilder.Entity<SampleLog>(entity =>
        {
            entity.HasKey(e => e.LogId);

            entity.ToTable("sample_log");

            entity.Property(e => e.LogId).HasColumnName("log_id");
            entity.Property(e => e.BarcodeId).HasColumnName("barcode_id");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(50)
                .HasColumnName("last_update_by");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("last_update_date");
            entity.Property(e => e.LastUpdateFrom)
                .HasMaxLength(50)
                .HasColumnName("last_update_from");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.Barcode).WithMany(p => p.SampleLogs)
                .HasForeignKey(d => d.BarcodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sample_log_Service_barcode");

            entity.HasOne(d => d.Status).WithMany(p => p.SampleLogs)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sample_log_sample_status");
        });

        modelBuilder.Entity<SampleStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId);

            entity.ToTable("sample_status");

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<ServiceBarcode>(entity =>
        {
            entity.HasKey(e => e.BarcodeId).HasName("PK_Service_qrcode");

            entity.ToTable("Service_barcode");

            entity.Property(e => e.BarcodeId).HasColumnName("barcode_id");
            entity.Property(e => e.Barcode)
                .HasMaxLength(250)
                .HasColumnName("barcode");
            entity.Property(e => e.PackageId).HasColumnName("package_id");
            entity.Property(e => e.SampleStatus).HasColumnName("sample_status");
            entity.Property(e => e.ServicesId).HasColumnName("services_id");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");

            entity.HasOne(d => d.Package).WithMany(p => p.ServiceBarcodes)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("FK_Service_qrcode_Standard_Services");

            entity.HasOne(d => d.SampleStatusNavigation).WithMany(p => p.ServiceBarcodes)
                .HasForeignKey(d => d.SampleStatus)
                .HasConstraintName("FK_Service_barcode_sample_status");

            entity.HasOne(d => d.Services).WithMany(p => p.ServiceBarcodes)
                .HasForeignKey(d => d.ServicesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Service_qrcode_Services_Pricelist");

            entity.HasOne(d => d.Visit).WithMany(p => p.ServiceBarcodes)
                .HasForeignKey(d => d.VisitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Service_qrcode_Visits");
        });

        modelBuilder.Entity<ServicesCategory>(entity =>
        {
            entity.HasKey(e => e.ServiceCatId);

            entity.ToTable("Services_Categories");

            entity.Property(e => e.ServiceCatId).HasColumnName("service_cat_id");
            entity.Property(e => e.AlphaCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("alpha_code");
            entity.Property(e => e.ServiceCatName)
                .HasMaxLength(50)
                .HasColumnName("service_cat_name");
            entity.Property(e => e.ServiceCatNameAr)
                .HasMaxLength(50)
                .HasColumnName("service_cat_name_ar");
        });

        modelBuilder.Entity<ServicesPricelist>(entity =>
        {
            entity.HasKey(e => e.ServiceId);

            entity.ToTable("Services_Pricelist");

            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_update_by");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("date")
                .HasColumnName("last_update_date");
            entity.Property(e => e.LastUpdateFrom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_update_from");
            entity.Property(e => e.ServiceCat).HasColumnName("service_cat");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(150)
                .HasColumnName("service_name");
            entity.Property(e => e.ServiceNameAr)
                .HasMaxLength(150)
                .HasColumnName("service_name_ar");
            entity.Property(e => e.ServicePrice).HasColumnName("service_price");
            entity.Property(e => e.StandRef).HasColumnName("stand_ref");

            entity.HasOne(d => d.Contract).WithMany(p => p.ServicesPricelists)
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Services_Pricelist_Vendor_Contracts");

            entity.HasOne(d => d.ServiceCatNavigation).WithMany(p => p.ServicesPricelists)
                .HasForeignKey(d => d.ServiceCat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Services_Pricelist_Services_Categories");

            entity.HasOne(d => d.StandRefNavigation).WithMany(p => p.ServicesPricelists)
                .HasForeignKey(d => d.StandRef)
                .HasConstraintName("FK_Services_Pricelist_Standard_Services");

            entity.HasMany(d => d.Services).WithMany(p => p.Packages)
                .UsingEntity<Dictionary<string, object>>(
                    "PackageService",
                    r => r.HasOne<StandardService>().WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Package_Services_Standard_Services"),
                    l => l.HasOne<ServicesPricelist>().WithMany()
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Package_Services_Services_Pricelist"),
                    j =>
                    {
                        j.HasKey("PackageId", "ServiceId");
                        j.ToTable("Package_Services");
                        j.IndexerProperty<int>("PackageId").HasColumnName("package_id");
                        j.IndexerProperty<int>("ServiceId").HasColumnName("service_id");
                    });
        });

        modelBuilder.Entity<StandardService>(entity =>
        {
            entity.HasKey(e => e.ServiceId);

            entity.ToTable("Standard_Services");

            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.IsPackage).HasColumnName("is_package");
            entity.Property(e => e.ServiceAlphaCode).HasColumnName("service_alpha_code");
            entity.Property(e => e.ServiceAvailability)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("service_availability");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(150)
                .HasColumnName("service_name");
            entity.Property(e => e.ServiceUnit)
                .HasMaxLength(50)
                .HasColumnName("service_unit");

            entity.HasOne(d => d.ServiceAlphaCodeNavigation).WithMany(p => p.StandardServices)
                .HasForeignKey(d => d.ServiceAlphaCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Standard_Services_Services_Categories");
        });

        modelBuilder.Entity<Syndicate>(entity =>
        {
            entity.HasKey(e => e.SyndId);

            entity.Property(e => e.SyndId).HasColumnName("synd_id");
            entity.Property(e => e.SyndName)
                .HasMaxLength(100)
                .HasColumnName("synd_name");
        });

        modelBuilder.Entity<SyndicateService>(entity =>
        {
            entity.HasKey(e => e.ServiceId);

            entity.ToTable("Syndicate_Services");

            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.ServiceCat).HasColumnName("service_cat");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(100)
                .HasColumnName("service_name");
            entity.Property(e => e.ServiceNameAr)
                .HasMaxLength(100)
                .HasColumnName("service_name_ar");
            entity.Property(e => e.ServicePrice).HasColumnName("service_price");
            entity.Property(e => e.StandRef).HasColumnName("stand_ref");
            entity.Property(e => e.SyndicateId).HasColumnName("syndicate_id");

            entity.HasOne(d => d.ServiceCatNavigation).WithMany(p => p.SyndicateServices)
                .HasForeignKey(d => d.ServiceCat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Syndicate_Services_Services_Categories");

            entity.HasOne(d => d.StandRefNavigation).WithMany(p => p.SyndicateServices)
                .HasForeignKey(d => d.StandRef)
                .HasConstraintName("FK_Syndicate_Services_Standard_Services");

            entity.HasOne(d => d.Syndicate).WithMany(p => p.SyndicateServices)
                .HasForeignKey(d => d.SyndicateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Syndicate_Services_Syndicates");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.Property(e => e.VendorId).HasColumnName("vendor_id");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("creation_date");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_update_by");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("last_update_date");
            entity.Property(e => e.LastUpdateFrom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_update_from");
            entity.Property(e => e.OfficialEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("official_email");
            entity.Property(e => e.VendorAddress)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("vendor_address");
            entity.Property(e => e.VendorCat).HasColumnName("vendor_cat");
            entity.Property(e => e.VendorName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("vendor_name");
            entity.Property(e => e.VendorTel).HasColumnName("vendor_tel");
            entity.Property(e => e.VendorTel2).HasColumnName("vendor_tel2");
            entity.Property(e => e.VendorWebsite)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("vendor_website");

            entity.HasOne(d => d.VendorCatNavigation).WithMany(p => p.Vendors)
                .HasForeignKey(d => d.VendorCat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vendors_Vendor_Categories");
        });

        modelBuilder.Entity<VendorCategory>(entity =>
        {
            entity.HasKey(e => e.CatId);

            entity.ToTable("Vendor_Categories");

            entity.Property(e => e.CatId).HasColumnName("cat_id");
            entity.Property(e => e.AlphaCode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("alpha_code");
            entity.Property(e => e.CatName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cat_name");
            entity.Property(e => e.CatNameAr)
                .HasMaxLength(50)
                .HasColumnName("cat_name_ar");
        });

        modelBuilder.Entity<VendorContact>(entity =>
        {
            entity.HasKey(e => e.ContactId);

            entity.ToTable("Vendor_Contacts");

            entity.Property(e => e.ContactId)
                .ValueGeneratedNever()
                .HasColumnName("contact_id");
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("contact_email");
            entity.Property(e => e.ContactName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("contact_name");
            entity.Property(e => e.ContactNotes)
                .HasMaxLength(500)
                .HasColumnName("contact_notes");
            entity.Property(e => e.ContactTel)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("contact_tel");
            entity.Property(e => e.ContactTel2)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("contact_tel2");
            entity.Property(e => e.ContactVendorId).HasColumnName("contact_vendor_id");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_update_by");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("last_update_date");
            entity.Property(e => e.LastUpdateFrom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_update_from");
            entity.Property(e => e.Occuption).HasColumnName("occuption");

            entity.HasOne(d => d.ContactVendor).WithMany(p => p.VendorContacts)
                .HasForeignKey(d => d.ContactVendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vendor_Contacts_Vendors");

            entity.HasOne(d => d.OccuptionNavigation).WithMany(p => p.VendorContacts)
                .HasForeignKey(d => d.Occuption)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vendor_Contacts_contacts_occupation");
        });

        modelBuilder.Entity<VendorContract>(entity =>
        {
            entity.HasKey(e => e.ContractId);

            entity.ToTable("Vendor_Contracts");

            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.ContractName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("contract_name");
            entity.Property(e => e.ContractType).HasColumnName("contract_type");
            entity.Property(e => e.ContractValue).HasColumnName("contract_value");
            entity.Property(e => e.CurrencyId).HasColumnName("currency_id");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.ExcutedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("excuted_by");
            entity.Property(e => e.IncreasePer).HasColumnName("increase_per");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_update_by");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("date")
                .HasColumnName("last_update_date");
            entity.Property(e => e.LastUpdateFrom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_update_from");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.VendorId).HasColumnName("vendor_id");

            entity.HasOne(d => d.ContractTypeNavigation).WithMany(p => p.VendorContracts)
                .HasForeignKey(d => d.ContractType)
                .HasConstraintName("FK_Vendor_Contracts_Vendor_Contract_Types");

            entity.HasOne(d => d.Currency).WithMany(p => p.VendorContracts)
                .HasForeignKey(d => d.CurrencyId)
                .HasConstraintName("FK_Vendor_Contracts_Currencies");

            entity.HasOne(d => d.Vendor).WithMany(p => p.VendorContracts)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vendor_Contracts_Vendors");
        });

        modelBuilder.Entity<VendorContractType>(entity =>
        {
            entity.HasKey(e => e.TypeId);

            entity.ToTable("Vendor_Contract_Types");

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.Parent).HasColumnName("parent");
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .HasColumnName("type_name");
            entity.Property(e => e.TypeNameAr)
                .HasMaxLength(50)
                .HasColumnName("type_name_ar");
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.Property(e => e.VisitId).HasColumnName("visit_id");
            entity.Property(e => e.ApprovalMaxLimit).HasColumnName("approval_max_limit");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.Copayment).HasColumnName("copayment");
            entity.Property(e => e.DiscountCard).HasColumnName("discount_card");
            entity.Property(e => e.InssuredExpireDate)
                .HasColumnType("datetime")
                .HasColumnName("inssured_expire_date");
            entity.Property(e => e.InvestigationNotes)
                .HasMaxLength(250)
                .HasColumnName("investigation_notes");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_update_by");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("last_update_date");
            entity.Property(e => e.LastUpdateFrom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_update_from");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PatientApprovalNum)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("patient_approval_num");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.PatientInssurredId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("patient_inssurred_id");
            entity.Property(e => e.RefererDoctor).HasColumnName("referer_doctor");
            entity.Property(e => e.RelatedVisit).HasColumnName("related_visit");
            entity.Property(e => e.RelativeId).HasColumnName("relative_id");
            entity.Property(e => e.VisitEndDate)
                .HasColumnType("datetime")
                .HasColumnName("visit_end_date");
            entity.Property(e => e.VisitInsurred).HasColumnName("visit_insurred");
            entity.Property(e => e.VisitInsurredCompanyId).HasColumnName("visit_insurred_company_id");
            entity.Property(e => e.VisitNote)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("visit_note");
            entity.Property(e => e.VisitSource).HasColumnName("visit_source");
            entity.Property(e => e.VisitStartDate)
                .HasColumnType("datetime")
                .HasColumnName("visit_start_date");
            entity.Property(e => e.VisitStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("visit_status");
            entity.Property(e => e.VisitType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("visit_type");

            entity.HasOne(d => d.Patient).WithMany(p => p.Visits)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_Visits_Patients");

            entity.HasOne(d => d.RefererDoctorNavigation).WithMany(p => p.Visits)
                .HasForeignKey(d => d.RefererDoctor)
                .HasConstraintName("FK_Visits_Reffered_Doctors");
        });

        modelBuilder.Entity<VisitService>(entity =>
        {
            entity.HasKey(e => new { e.ServiceId, e.VisitId, e.ServiceSerial });

            entity.ToTable("visit_services");

            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.VisitId).HasColumnName("visit_id");
            entity.Property(e => e.ServiceSerial).HasColumnName("service_serial");
            entity.Property(e => e.ApprovalDate)
                .HasColumnType("datetime")
                .HasColumnName("approval_date");
            entity.Property(e => e.ApprovalId)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("approval_id");
            entity.Property(e => e.Lab2lab).HasColumnName("lab2lab");
            entity.Property(e => e.RequestedDepartment)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("requested_department");
            entity.Property(e => e.RequestedDoctor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("requested_doctor");
            entity.Property(e => e.ServiceBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("service_by");
            entity.Property(e => e.ServiceDate)
                .HasColumnType("datetime")
                .HasColumnName("service_date");
            entity.Property(e => e.ServiceNotes)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("service_notes");

            entity.HasOne(d => d.Lab2labNavigation).WithMany(p => p.VisitServices)
                .HasForeignKey(d => d.Lab2lab)
                .HasConstraintName("FK_visit_services_Vendors");

            entity.HasOne(d => d.Service).WithMany(p => p.VisitServices)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_visit_services_Services_Pricelist");

            entity.HasOne(d => d.Visit).WithMany(p => p.VisitServices)
                .HasForeignKey(d => d.VisitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_visit_services_Visits");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
