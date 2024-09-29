namespace Ordering.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CustomerId);

        builder.HasIndex(x => x.OrderName)
               .IsUnique();

        builder.Property(x => x.OrderName)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasMany(x => x.Items)
               .WithOne()
               .HasForeignKey(x => x.OrderId);

        builder.ComplexProperty(x => x.ShippingAddress, addressBuilder =>
        {
            addressBuilder.Property(x => x.FirstName)
                          .HasMaxLength(50)
                          .IsRequired();

            addressBuilder.Property(x => x.LastName)
                          .HasMaxLength(50)
                          .IsRequired();

            addressBuilder.Property(x => x.EmailAddress)
                          .HasMaxLength(50)
                          .IsRequired();

            addressBuilder.Property(x => x.AddressLine)
                          .HasMaxLength(180)
                          .IsRequired();

            addressBuilder.Property(x => x.Country)
                          .HasMaxLength(50);

            addressBuilder.Property(x => x.State)
                          .HasMaxLength(50);

            addressBuilder.Property(x => x.ZipCode)
                          .HasMaxLength(5)
                          .IsRequired();
        });

        builder.ComplexProperty(x => x.BillingAddress, addressBuilder =>
        {
            addressBuilder.Property(x => x.FirstName)
                          .HasMaxLength(50)
                          .IsRequired();

            addressBuilder.Property(x => x.LastName)
                          .HasMaxLength(50)
                          .IsRequired();

            addressBuilder.Property(x => x.EmailAddress)
                          .HasMaxLength(50)
                          .IsRequired();

            addressBuilder.Property(x => x.AddressLine)
                          .HasMaxLength(180)
                          .IsRequired();

            addressBuilder.Property(x => x.Country)
                          .HasMaxLength(50);

            addressBuilder.Property(x => x.State)
                          .HasMaxLength(50);

            addressBuilder.Property(x => x.ZipCode)
                          .HasMaxLength(5)
                          .IsRequired();
        });

        builder.ComplexProperty(x => x.Payment, paymentBuilder =>
        {
            paymentBuilder.Property(x => x.CardName)
                          .HasMaxLength(50);

            paymentBuilder.Property(x => x.CardNumber)
                         .HasMaxLength(24)
                         .IsRequired();

            paymentBuilder.Property(x => x.Expiration)
                         .HasMaxLength(10);

            paymentBuilder.Property(x => x.CVV)
                         .HasMaxLength(3);

            paymentBuilder.Property(x => x.PaymentMethod);
        });
    }
}
