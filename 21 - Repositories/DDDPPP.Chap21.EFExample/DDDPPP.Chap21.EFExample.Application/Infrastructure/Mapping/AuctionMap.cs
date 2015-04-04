using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using DDDPPP.Chap21.EFExample.Application.Infrastructure.DataModel;

namespace DDDPPP.Chap21.EFExample.Application.Infrastructure.Mapping
{
    public class AuctionMap : EntityTypeConfiguration<AuctionDTO>
    {
        public AuctionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Auctions");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.StartingPrice).HasColumnName("StartingPrice");
            this.Property(t => t.BidderMemberId).HasColumnName("BidderMemberId");
            this.Property(t => t.TimeOfBid).HasColumnName("TimeOfBid");
            this.Property(t => t.MaximumBid).HasColumnName("MaximumBid");
            this.Property(t => t.CurrentPrice).HasColumnName("CurrentPrice");
            this.Property(t => t.AuctionEnds).HasColumnName("AuctionEnds");
            this.Property(t => t.Version).HasColumnName("Version").IsConcurrencyToken();
        }
    }
}
