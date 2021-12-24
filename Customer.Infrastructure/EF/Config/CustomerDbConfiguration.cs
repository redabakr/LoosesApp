using Customer.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.Infrastructure.EF.Config;

internal sealed class CustomerDbConfiguration : IEntityTypeConfiguration<Domain.Entities.Customer>, 
    IEntityTypeConfiguration<ShippingAddress>

{
    public void Configure(EntityTypeBuilder<Domain.Entities.Customer> builder)
    {
        builder.ToTable("Customers");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(v => v.Value, v=> new CustomerId(v))
            .HasColumnName("Id");
        
        builder.Property(x => x.Email)
            .HasConversion(v => v.Value, v=> new CustomerEmail(v))
            .HasColumnName("Email");
        
        builder.Property(x => x.Phone)
            .HasConversion(v => v.Value, v=> new CustomerPhone(v))
            .HasColumnName("Phone");

        builder.Property(x => x.Country)
            .HasConversion(v => v.Value, v=> new CustomerCountry(v))
            .HasColumnName("Country");

        builder.Property(x => x.City)
            .HasConversion(v => v.Value, v=> new CustomerCity(v))
            .HasColumnName("City");
        
        builder.Property(x => x.Gender)
            .HasColumnName("Gender");
        
        builder.Property(x => x.FirstName)
            .HasConversion(v => v.Value, v=> new CustomerFirstName(v))
            .HasColumnName("FirstName");
        
        builder.Property(x => x.LastName)
            .HasConversion(v => v.Value, v=> new CustomerLastName(v))
            .HasColumnName("LastName");
        
        builder.Property(x => x.Age)
            .HasConversion(v => v.Value, v=> new CustomerAge(v))
            .HasColumnName("Age");

        var navigation = builder.Metadata.FindNavigation(nameof(Domain.Entities.Customer.ShippingAddresses));
        //EF access the OrderItem collection property through its backing field
        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        
        //builder.HasMany(typeof(ShippingAddress), "_shippingAddresses");
    }

    public void Configure(EntityTypeBuilder<ShippingAddress> builder)
    {
        builder.ToTable("ShippingAddresses");
        builder.HasKey("Id");
        builder.Property(x => x.Name);
        builder.Property(x => x.Street);
        builder.Property(x => x.City);
        builder.Property(x => x.Description);
        builder.Property(x => x.IsDefault);
    }
}