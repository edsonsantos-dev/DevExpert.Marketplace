using DevExpert.Marketplace.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevExpert.Marketplace.Data.Mappings;

public class ImageMapping : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.ToTable("Images");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Path)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnType("varchar(200)");

        builder.Ignore(x => x.IsCover);
        
        builder.HasIndex(x => x.ProductId);
        builder.HasIndex(x => x.AddedBy);
        builder.HasIndex(x => x.AddedOn);
    }
}