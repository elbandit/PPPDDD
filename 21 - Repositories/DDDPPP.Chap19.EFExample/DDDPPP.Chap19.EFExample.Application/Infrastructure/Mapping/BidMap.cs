using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DDDPPP.Chap19.EFExample.Application.Infrastructure.DataModel;

namespace DDDPPP.Chap19.EFExample.Application.Infrastructure.Mapping
{
    public class BidMap : EntityTypeConfiguration<BidDTO>
    {
        public BidMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("BidHistory");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AuctionId).HasColumnName("AuctionId");
            this.Property(t => t.BidderId).HasColumnName("BidderId");
            this.Property(t => t.Bid).HasColumnName("Bid");
            this.Property(t => t.TimeOfBid).HasColumnName("TimeOfBid");
        }
    }
}
