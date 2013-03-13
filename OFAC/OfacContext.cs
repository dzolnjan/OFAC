using System.Data.Entity;

namespace OFAC
{
    public class OfacContext : DbContext
    {
        public OfacContext() : base("DefaultConnection")
        {
        }

        public DbSet<sdnListM> sdnList { get; set; }
        public DbSet<sdnListPublshInformationM> sdnListPublshInformation { get; set; }
        public DbSet<sdnListSdnEntryM> sdnListSdnEntry { get; set; }
        public DbSet<sdnListSdnEntryIDM> sdnListSdnEntryID { get; set; }
        public DbSet<sdnListSdnEntryAkaM> sdnListSdnEntryAka { get; set; }
        public DbSet<sdnListSdnEntryAddressM> sdnListSdnEntryAddress { get; set; }
        public DbSet<sdnListSdnEntryNationalityM> sdnListSdnEntryNationality { get; set; }
        public DbSet<sdnListSdnEntryCitizenshipM> sdnListSdnEntryCitizenship { get; set; }
        public DbSet<sdnListSdnEntryDateOfBirthItemM> sdnListSdnEntryDateOfBirthItem { get; set; }
        public DbSet<sdnListSdnEntryPlaceOfBirthItemM> sdnListSdnEntryPlaceOfBirthItem { get; set; }
        public DbSet<sdnListSdnEntryVesselInfoM> sdnListSdnEntryVesselInfo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Use singular table names
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}

