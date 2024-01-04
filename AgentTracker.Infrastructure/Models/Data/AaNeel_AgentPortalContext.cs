using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AgentTracker.Infrastructure.Models.data
{
    public partial class AaNeel_AgentPortalContext : DbContext
    {
        public AaNeel_AgentPortalContext()
        {
        }

        public AaNeel_AgentPortalContext(DbContextOptions<AaNeel_AgentPortalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AgentDetail> AgentDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=ustpavpdb03\\sql2017;database=AaNeel_AgentPortal;User ID=webuser;password=Tran@SF#2020;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AgentDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AgentId).HasColumnName("Agent ID");

                entity.Property(e => e.AgentName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Agent Name");

                entity.Property(e => e.AppNbr)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("App Nbr");

                entity.Property(e => e.ApplicationCategory)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Application Category");

                entity.Property(e => e.ApplicationDate)
                    .HasColumnType("date")
                    .HasColumnName("Application Date");

                entity.Property(e => e.ApplicationId).HasColumnName("Application ID");

                entity.Property(e => e.ApplicationStatus)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Application Status");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.County)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime)
                    .HasColumnType("date")
                    .HasColumnName("Create Time");

                entity.Property(e => e.EffectiveDate)
                    .HasColumnType("date")
                    .HasColumnName("Effective Date");

                entity.Property(e => e.ElectionType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Election Type");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("First Name");

                entity.Property(e => e.Hicn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("HICN");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Last Name");

                entity.Property(e => e.MbiHic)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MBI/HIC");

                entity.Property(e => e.MemberId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Member ID");

                entity.Property(e => e.MiddleInit)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Middle Init");

                entity.Property(e => e.PbpId).HasColumnName("PBP ID");

                entity.Property(e => e.PcpEffEndDateDate)
                    .HasColumnType("date")
                    .HasColumnName("PCP Eff End_Date date");

                entity.Property(e => e.PcpEffStartDate)
                    .HasColumnType("date")
                    .HasColumnName("PCP Eff Start Date");

                entity.Property(e => e.PcpId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PCP ID");

                entity.Property(e => e.PcpName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PCP Name");

                entity.Property(e => e.PlanId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Plan ID");

                entity.Property(e => e.ReceiptDate)
                    .HasColumnType("date")
                    .HasColumnName("Receipt Date");

                entity.Property(e => e.SepReasonCode)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("SEP Reason Code");

                entity.Property(e => e.SgnDate)
                    .HasColumnType("date")
                    .HasColumnName("Sgn Date");

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("Update Date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Zip).HasColumnName("ZIP");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
