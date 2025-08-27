using Domain.Constant;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DatabaseConfig
{
    public class BidStarContext : IdentityDbContext<Account>
    {
        public BidStarContext(DbContextOptions<BidStarContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        #region config dbset
        public virtual DbSet<AuctionSession> AuctionSessions { get; set; }

        public virtual DbSet<Bid> Bids { get; set; }

        public virtual DbSet<Buyer> Buyers { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual  DbSet<Joinning> Joinnings { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Payment> Payments { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Seller> Sellers { get; set;}



        #endregion

        
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Account>().ToTable("Account");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityRole>().ToTable("Role");

            builder.Entity<Account>(entity =>
            {
                entity.HasOne(e => e.Seller)
                .WithOne(e => e.Account)
                .HasForeignKey<Seller>(e => e.AccountId);

                entity.HasOne(e => e.Buyer).WithOne(e => e.Account).HasForeignKey<Buyer>(e => e.BuyerId);
            });

            builder.Entity<AuctionSession>(entity =>
            {
                entity.Property(e => e.Id)
                .UseIdentityColumn(1);

                entity.HasOne(e => e.Winner)
                    .WithMany()
                    .HasForeignKey(e => e.WinnerId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.Product)
                .WithMany()
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(e => e.Bids)
                    .WithOne(e => e.AuctionSession)
                    .HasForeignKey(e => e.AuctionSessionId)
                    .OnDelete(DeleteBehavior.NoAction);
                entity.HasMany(e => e.Joinnings)
                    .WithOne(e => e.AuctionSession)
                    .HasForeignKey(e => e.AuctionSessionId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<Bid>(entity =>
            {
                entity.Property(e => e.Id)
                .UseIdentityColumn(1);
                entity.HasOne(e => e.Buyer)
                    .WithMany(e => e.Bids)
                    .HasForeignKey(e => e.BuyerId);
                entity.HasOne(e => e.AuctionSession)
                       .WithMany(e => e.Bids)
                       .HasForeignKey(e => e.AuctionSessionId);

            });
            builder.Entity<Buyer>(entity =>
            {
                entity.HasKey(e => e.BuyerId);
                entity.HasMany(e => e.Orders)
                       .WithOne(e => e.Buyer)
                       .HasForeignKey(e => e.BuyerId);

                entity.HasMany(e => e.Joinnings)
                      .WithOne(e => e.Buyer)
                      .HasForeignKey(e => e.BuyerId);

                entity.HasMany(e => e.Bids)
                    .WithOne(e => e.Buyer)
                    .HasForeignKey(e => e.BuyerId);

                entity.HasOne(e => e.Account)
                      .WithOne(e => e.Buyer)
                      .HasForeignKey<Buyer>(e => e.BuyerId);
            });

            builder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id)
                .UseIdentityColumn(1);
                entity.HasMany(e => e.Products)
                    .WithOne(e => e.Category)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<Joinning>(entity =>
            {
                entity.Property(e => e.Id)
                .UseIdentityColumn(1);
                entity.HasOne(e => e.AuctionSession)
                    .WithMany(e => e.Joinnings)
                    .HasForeignKey(e => e.AuctionSessionId);
                entity.HasOne(e => e.Buyer)
                    .WithMany(e => e.Joinnings)
                    .HasForeignKey(e => e.BuyerId);   
            });

            builder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id)
                .UseIdentityColumn(1);
                entity.HasOne(e => e.Seller)
                    .WithMany(e => e.Orders)
                    .HasForeignKey(e => e.SellerId)
                    .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(e => e.Buyer)
                    .WithMany(e => e.Orders)
                    .HasForeignKey(e => e.BuyerId)
                    .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(e => e.AuctionSession)
                    .WithOne()
                    .HasForeignKey<Order>(e => e.AuctionSessionId)
                    .OnDelete(DeleteBehavior.NoAction);
                entity.HasOne(e => e.Product)
                    .WithOne()
                    .HasForeignKey<Order>(e => e.ProductId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.Id)
                .UseIdentityColumn(1);
               
                entity.HasOne(e => e.Buyer)
                    .WithMany(e => e.Payments)
                    .HasForeignKey(e => e.BuyerId);

            });
            builder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id)
                .UseIdentityColumn(1);
                entity.HasOne(e => e.Seller)
                .WithMany(e => e.Products)
                .HasForeignKey(e => e.SellerId);

                entity.HasOne(e => e.Category)
                    .WithMany(e => e.Products)
                    .HasForeignKey(e => e.CategoryId);

                entity.HasMany(e => e.ImageProducts)
                    .WithOne(e => e.Product)
                    .HasForeignKey(e => e.ProductId);
            });

            builder.Entity<Seller>(entity =>
            {
                entity.HasKey(e => e.AccountId);
                entity.HasOne(e => e.Account)
                    .WithOne(e => e.Seller)
                    .HasForeignKey<Seller>(e => e.AccountId);
                entity.HasMany(e => e.AuctionSessions)
                      .WithOne(e => e.Seller)
                      .HasForeignKey(e => e.SellerId)
                      .OnDelete(DeleteBehavior.NoAction);
                entity.HasMany(e => e.Products)
                    .WithOne(e => e.Seller)
                    .HasForeignKey(e => e.SellerId);

                entity.HasMany(e => e.Orders)
                    .WithOne(e => e.Seller)
                    .HasForeignKey(e => e.SellerId);
            });
            builder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole { Id = "1", Name = UserRole.BUYER, NormalizedName = UserRole.BUYER},
                new IdentityRole { Id = "2", Name = UserRole.SELLER, NormalizedName = UserRole.SELLER},
                new IdentityRole { Id = "3", Name = UserRole.ADMIN, NormalizedName = UserRole.ADMIN}
                );
            builder.Entity<Category>(entity =>
            {
                entity.HasData(new Category {Id = 1, Name = "Electronics", Description = "Điện tử: điện thoại, laptop, v.v." });
                entity.HasData(new Category {Id = 2, Name = "Fashion", Description = "Thời trang: quần áo, phụ kiện" });
                entity.HasData(new Category {Id = 3, Name = "Home & Garden", Description = "Đồ gia dụng, nội thất, cây cảnh" });
                entity.HasData(new Category {Id = 4, Name = "Books", Description = "Sách, truyện tranh, giáo trình" });
                entity.HasData(new Category {Id = 5, Name = "Collectibles", Description = "Vật phẩm sưu tầm, đồ cổ" });
                entity.HasData(new Category {Id = 6, Name = "Jewelry & Watches", Description = "Trang sức, đồng hồ" });
                entity.HasData(new Category {Id = 7, Name = "Sports & Outdoors", Description = "Thể thao, dã ngoại" });
                entity.HasData(new Category {Id = 8, Name = "Health & Beauty", Description = "Mỹ phẩm, chăm sóc sức khỏe" });
                entity.HasData(new Category {Id = 9, Name = "Toys & Hobbies", Description = "Đồ chơi, mô hình" });
                entity.HasData(new Category {Id = 10, Name = "Vehicles", Description = "Ô tô, xe máy, xe đạp" });
            });
        }

    }
}
