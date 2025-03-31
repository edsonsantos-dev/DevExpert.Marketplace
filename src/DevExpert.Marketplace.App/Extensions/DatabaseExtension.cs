using DevExpert.Marketplace.Business.Models;
using DevExpert.Marketplace.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DevExpert.Marketplace.App.Extensions;

public static class DatabaseExtension
{
    public static void RegisterDatabaseServices(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddDbContext<MarketplaceContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString(nameof(MarketplaceContext))));
            builder.Services.AddDbContext<IdentityContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString(nameof(IdentityContext))));
        }
        else
        {
            builder.Services.AddDbContext<MarketplaceContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(MarketplaceContext))));
            builder.Services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(IdentityContext))));
        }
    }

    public static async Task EnsureSeedData(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var env = scopedProvider.GetRequiredService<IHostEnvironment>();
        var marketplaceContext = scopedProvider.GetRequiredService<MarketplaceContext>();
        var identityContext = scopedProvider.GetRequiredService<IdentityContext>();

        if (env.IsDevelopment() || env.IsEnvironment("Docker"))
        {
            await identityContext.Database.MigrateAsync();
            await marketplaceContext.Database.MigrateAsync();
            
            await EnsureSeedEntities(marketplaceContext, identityContext);
        }
    }

    private static async Task EnsureSeedEntities(
        MarketplaceContext marketplaceContext,
        IdentityContext identityContext)
    {
        if (identityContext.Users.Any())
            return;

        await identityContext.Users.AddAsync(new IdentityUser
        {
            Id = "b0be86e1-037b-4f2c-84f3-6f456762ca8e",
            UserName = "teste@teste.com",
            NormalizedUserName = "TESTE@TESTE.COM",
            Email = "teste@teste.com",
            NormalizedEmail = "TESTE@TESTE.COM",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEKfhxyJVWW/9lY8ItKciNHuVpkSvaHqzyHA4+1SfGgJHzC52/qXU8kUj06DrSRvgeA==",
            SecurityStamp = "YQFPMKDORLEKVUE5SILAJNEQZE5GL22Q",
            ConcurrencyStamp = "6b31294c-790c-4a42-8a84-7e7b26fc6378",
            PhoneNumber = null,
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnd = null,
            LockoutEnabled = true
        });

        await identityContext.SaveChangesAsync();

        if (marketplaceContext.Sellers.Any())
            return;

        await marketplaceContext.Sellers.AddAsync(new Seller
        {
            Id = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E"),
            FullName = "teste@teste.com",
            Email = "teste@teste.com",
            PhoneNumber = null,
            AddedOn = DateTime.Now,
            AddedBy = Guid.Empty
        });

        await marketplaceContext.SaveChangesAsync();

        if (marketplaceContext.Categories.Any())
            return;
        
        await marketplaceContext.Categories.AddRangeAsync(CreateDefaultCategories());
        await marketplaceContext.SaveChangesAsync();
        
        if (marketplaceContext.Products.Any())
            return;
        
        await marketplaceContext.Products.AddRangeAsync(CreateDefaultProducts());
        await marketplaceContext.SaveChangesAsync();
        
        if (marketplaceContext.Images.Any())
            return;

        await marketplaceContext.Images.AddRangeAsync(CreateDefaultImages());
        await marketplaceContext.SaveChangesAsync();
    }

    private static List<Category> CreateDefaultCategories()
    {
        return
        [
            new Category
            {
                Id = Guid.Parse("B5902737-178B-4FCE-BB76-91166D90215F"),
                Name = "Casual",
                Description =
                    "Estilo descontraído e confortável, ideal para o dia a dia, com peças práticas e leves como camisetas, jeans e tênis.",
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Category
            {
                Id = Guid.Parse("E96D52D4-B433-4B4F-8F1F-E232282E0C3E"),
                Name = "Esporte Fino",
                Description =
                    "Visual elegante e moderno, combinando peças sociais e casuais para ocasiões que exigem sofisticação sem formalidade excessiva.",
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Category
            {
                Id = Guid.Parse("C7FA8657-7973-4457-9CC6-88552620BCCC"),
                Name = "Infantil",
                Description =
                    "Roupas sofisticadas e confortáveis para crianças, com cortes refinados e detalhes delicados, ideais para eventos especiais.",
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Category
            {
                Id = Guid.Parse("FA7052ED-DB1C-4829-8B1E-B4BAA5779067"),
                Name = "Perfume",
                Description =
                    "Fragrância que expressa personalidade e estilo, trazendo frescor, elegância ou sensualidade ao visual.",
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Category
            {
                Id = Guid.Parse("5F13EBC9-4639-4F6F-9719-3AE3E6F38CEB"),
                Name = "Relógio",
                Description =
                    "Acessório sofisticado que une estilo e funcionalidade, agregando elegância e personalidade ao look.",
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Category
            {
                Id = Guid.Parse("CEE3B4D3-E59F-415D-8CC2-C79583211F8E"),
                Name = "Sapato",
                Description =
                    "Calçado que combina estilo e conforto, complementando o visual com elegância e versatilidade.",
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Category
            {
                Id = Guid.Parse("AFB9839F-DBCB-4D3D-80E6-AF1165DDCD73"),
                Name = "Terno",
                Description =
                    "Conjunto clássico e elegante, composto por paletó, calça e, opcionalmente, colete, ideal para ocasiões formais.",
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Category
            {
                Id = Guid.Parse("461B90B8-CBBA-45E6-96D4-88D193D2669C"),
                Name = "Óculos",
                Description =
                    "Acessório versátil que combina estilo e funcionalidade, protegendo os olhos e complementando o visual.",
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            }
        ];
    }

    private static List<Product> CreateDefaultProducts()
    {
        return
        [
            new Product
            {
                Id = Guid.Parse("CF1A52B7-8C13-459F-8D1B-54F4F42C8D36"),
                Name = "Camisa",
                Description =
                    "Peça versátil e confortável, ideal para looks descontraídos, com tecidos leves e estilos modernos para o dia a dia.",
                Price = 150.99M,
                Stock = 26,
                CategoryId = Guid.Parse("B5902737-178B-4FCE-BB76-91166D90215F"),
                SellerId = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Product
            {
                Id = Guid.Parse("9CA0DC1E-668A-4E4C-9A7B-532E9ABC7D4D"),
                Name = "Camiseta Polo",
                Description =
                    "Clássica e elegante, com gola dobrável e botões, perfeita para um visual casual sofisticado e confortável.",
                Price = 189.9M,
                Stock = 12,
                CategoryId = Guid.Parse("B5902737-178B-4FCE-BB76-91166D90215F"),
                SellerId = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Product
            {
                Id = Guid.Parse("39146688-1CAF-4C6F-85E0-DE06D69F5C91"),
                Name = "Join Elegancy",
                Description =
                    "Peça refinada com corte moderno, ideal para ocasiões sem formalidade extrema, combinando elegância e conforto.",
                Price = 347.9M,
                Stock = 12,
                CategoryId = Guid.Parse("E96D52D4-B433-4B4F-8F1F-E232282E0C3E"),
                SellerId = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Product
            {
                Id = Guid.Parse("33C7CAD4-DC9F-4AE7-ACE0-566F3364147B"),
                Name = "Jhon Forest",
                Description =
                    "Peça refinada com corte moderno, ideal para ocasiões sem formalidade extrema, combinando elegância e conforto.",
                Price = 215.49M,
                Stock = 7,
                CategoryId = Guid.Parse("E96D52D4-B433-4B4F-8F1F-E232282E0C3E"),
                SellerId = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Product
            {
                Id = Guid.Parse("8FB0644A-E27A-4D4E-B108-72254314168B"),
                Name = "Join Like",
                Description =
                    "Peça refinada com corte moderno, ideal para ocasiões sem formalidade extrema, combinando elegância e conforto.",
                Price = 475.49M,
                Stock = 9,
                CategoryId = Guid.Parse("E96D52D4-B433-4B4F-8F1F-E232282E0C3E"),
                SellerId = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Product
            {
                Id = Guid.Parse("6D2CE8E1-7D2C-4507-82DE-0ACE8C95A84E"),
                Name = "Litle Boy",
                Description =
                    "Roupas elegantes e confortáveis para meninos, combinando estilo e praticidade com detalhes sofisticados para ocasiões especiais.",
                Price = 775.49M,
                Stock = 9,
                CategoryId = Guid.Parse("C7FA8657-7973-4457-9CC6-88552620BCCC"),
                SellerId = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Product
            {
                Id = Guid.Parse("190E6ABE-202C-4881-B01E-9A6DB2B46D7D"),
                Name = "Classic LB",
                Description =
                    "Roupas elegantes e confortáveis para meninos, combinando estilo e praticidade com detalhes sofisticados para ocasiões especiais.",
                Price = 895.49M,
                Stock = 5,
                CategoryId = Guid.Parse("C7FA8657-7973-4457-9CC6-88552620BCCC"),
                SellerId = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Product
            {
                Id = Guid.Parse("CF8319D0-9DBF-414B-BCC8-082969710CAE"),
                Name = "Sunset 99",
                Description =
                    "Óculos de sol moderno e estiloso, com lentes escuras que oferecem proteção e um visual sofisticado para ocasiões casuais e elegantes.",
                Price = 545.49M,
                Stock = 5,
                CategoryId = Guid.Parse("461B90B8-CBBA-45E6-96D4-88D193D2669C"),
                SellerId = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Product
            {
                Id = Guid.Parse("4172894C-AA31-4A88-BCD6-D9B64A94D53D"),
                Name = "Sunset 718",
                Description =
                    "Óculos de sol moderno e estiloso, com lentes escuras que oferecem proteção e um visual sofisticado para ocasiões casuais e elegantes.",
                Price = 415.49M,
                Stock = 5,
                CategoryId = Guid.Parse("461B90B8-CBBA-45E6-96D4-88D193D2669C"),
                SellerId = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Product
            {
                Id = Guid.Parse("1CF044B2-3CEE-435C-BC4A-8996A06667F1"),
                Name = "Sunset 918",
                Description =
                    "Óculos de sol moderno e estiloso, com lentes escuras que oferecem proteção e um visual sofisticado para ocasiões casuais e elegantes.",
                Price = 515.49M,
                Stock = 5,
                CategoryId = Guid.Parse("461B90B8-CBBA-45E6-96D4-88D193D2669C"),
                SellerId = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Product
            {
                Id = Guid.Parse("8C692DEE-6855-4C9F-8004-AEBBBC0987FD"),
                Name = "Sunset 818",
                Description =
                    "Óculos de sol moderno e estiloso, com lentes escuras que oferecem proteção e um visual sofisticado para ocasiões casuais e elegantes.",
                Price = 345.49M,
                Stock = 5,
                CategoryId = Guid.Parse("461B90B8-CBBA-45E6-96D4-88D193D2669C"),
                SellerId = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Product
            {
                Id = Guid.Parse("6D2B80F7-A8A5-4A10-9C18-A8E498B11B71"),
                Name = "Sunset 998",
                Description =
                    "Óculos de sol moderno e estiloso, com lentes escuras que oferecem proteção e um visual sofisticado para ocasiões casuais e elegantes.",
                Price = 245.49M,
                Stock = 5,
                CategoryId = Guid.Parse("461B90B8-CBBA-45E6-96D4-88D193D2669C"),
                SellerId = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Product
            {
                Id = Guid.Parse("72C2F548-720D-4D43-B622-4A1DADB1F721"),
                Name = "PUB",
                Description =
                    "Fragrância marcante e sofisticada, ideal para noites e ocasiões especiais, transmitindo personalidade e estilo com notas intensas e sedutoras.",
                Price = 725.47M,
                Stock = 3,
                CategoryId = Guid.Parse("FA7052ED-DB1C-4829-8B1E-B4BAA5779067"),
                SellerId = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Product
            {
                Id = Guid.Parse("F19CB8D5-1BAF-4612-B09E-6CC9E99FD504"),
                Name = "Breitling",
                Description =
                    "Relógio de luxo suíço, conhecido por sua precisão, design sofisticado e durabilidade, ideal para quem aprecia elegância e desempenho.",
                Price = 2725.47M,
                Stock = 2,
                CategoryId = Guid.Parse("5F13EBC9-4639-4F6F-9719-3AE3E6F38CEB"),
                SellerId = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Product
            {
                Id = Guid.Parse("227DB142-E978-42CA-9969-220CE790EDA3"),
                Name = "TIME",
                Description =
                    "Relógio de luxo suíço, conhecido por sua precisão, design sofisticado e durabilidade, ideal para quem aprecia elegância e desempenho.",
                Price = 1937M,
                Stock = 3,
                CategoryId = Guid.Parse("5F13EBC9-4639-4F6F-9719-3AE3E6F38CEB"),
                SellerId = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Product
            {
                Id = Guid.Parse("FBF934E5-35FF-425E-9A29-A5CE4D9E26F8"),
                Name = "Shoe",
                Description =
                    "Calçado clássico e versátil, combinando conforto e estilo para ocasiões formais e casuais elegantes.",
                Price = 937M,
                Stock = 3,
                CategoryId = Guid.Parse("CEE3B4D3-E59F-415D-8CC2-C79583211F8E"),
                SellerId = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Product
            {
                Id = Guid.Parse("5CCDA279-7A1A-4469-959A-C4872D03655D"),
                Name = "Groom",
                Description =
                    "Terno sofisticado e impecável, projetado especialmente para noivos, unindo elegância, conforto e estilo para o grande dia.",
                Price = 3457.88M,
                Stock = 1,
                CategoryId = Guid.Parse("AFB9839F-DBCB-4D3D-80E6-AF1165DDCD73"),
                SellerId = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Product
            {
                Id = Guid.Parse("B6900844-DEE4-4156-908D-297504360CB8"),
                Name = "MEN",
                Description =
                    "Conjunto clássico e versátil para homens, oferecendo elegância e estilo em eventos formais ou ocasiões especiais.",
                Price = 3100.99M,
                Stock = 4,
                CategoryId = Guid.Parse("AFB9839F-DBCB-4D3D-80E6-AF1165DDCD73"),
                SellerId = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Product
            {
                Id = Guid.Parse("FFC8DB59-54B6-4C37-8819-179B1F62A9B1"),
                Name = "Portrait",
                Description =
                    "Terno elegante e bem ajustado, ideal para sessões fotográficas e eventos especiais, destacando sofisticação e estilo.",
                Price = 3347.99M,
                Stock = 4,
                CategoryId = Guid.Parse("AFB9839F-DBCB-4D3D-80E6-AF1165DDCD73"),
                SellerId = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Product
            {
                Id = Guid.Parse("66DE29D8-E24E-4035-92C9-565C7C2FC1CB"),
                Name = "Young",
                Description =
                    "Terno moderno e estiloso, com cortes ajustados e detalhes contemporâneos, ideal para jovens que buscam elegância com um toque de descontração.",
                Price = 3347.99M,
                Stock = 4,
                CategoryId = Guid.Parse("AFB9839F-DBCB-4D3D-80E6-AF1165DDCD73"),
                SellerId = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            }
        ];
    }

    private static List<Image> CreateDefaultImages()
    {
        return
        [
            new Image
            {
                Id = Guid.Parse("0839737D-BD3A-40AB-B6C7-5BDADEE9E792"),
                DisplayPosition = 2,
                Name = "0839737d-bd3a-40ab-b6c7-5bdadee9e792.jpg",
                ProductId = Guid.Parse("CF1A52B7-8C13-459F-8D1B-54F4F42C8D36"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("CCF07E7A-3EE2-49F4-9C6C-03DCF6DF59E6"),
                DisplayPosition = 1,
                Name = "ccf07e7a-3ee2-49f4-9c6c-03dcf6df59e6.jpg",
                ProductId = Guid.Parse("CF1A52B7-8C13-459F-8D1B-54F4F42C8D36"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("D9EF862B-C3F1-4129-8FA2-F4FFA6E8E1E9"),
                DisplayPosition = 3,
                Name = "d9ef862b-c3f1-4129-8fa2-f4ffa6e8e1e9.jpg",
                ProductId = Guid.Parse("CF1A52B7-8C13-459F-8D1B-54F4F42C8D36"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("28F3E503-30AB-453E-9D53-BFDE33F64C4E"),
                DisplayPosition = 1,
                Name = "28f3e503-30ab-453e-9d53-bfde33f64c4e.jpg",
                ProductId = Guid.Parse("9CA0DC1E-668A-4E4C-9A7B-532E9ABC7D4D"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("0FC1A57E-1D4C-4F4C-910C-E59A0989C0A8"),
                DisplayPosition = 3,
                Name = "0fc1a57e-1d4c-4f4c-910c-e59a0989c0a8.jpg",
                ProductId = Guid.Parse("39146688-1CAF-4C6F-85E0-DE06D69F5C91"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("2905EBD7-302C-4200-946A-5986DFD6BDEF"),
                DisplayPosition = 1,
                Name = "2905ebd7-302c-4200-946a-5986dfd6bdef.jpg",
                ProductId = Guid.Parse("39146688-1CAF-4C6F-85E0-DE06D69F5C91"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("771A3A41-9DFA-489B-8326-FE33F7FB5076"),
                DisplayPosition = 4,
                Name = "771a3a41-9dfa-489b-8326-fe33f7fb5076.jpg",
                ProductId = Guid.Parse("39146688-1CAF-4C6F-85E0-DE06D69F5C91"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("DC64CBA3-E294-40CA-BCA8-84BCF196DB0B"),
                DisplayPosition = 2,
                Name = "dc64cba3-e294-40ca-bca8-84bcf196db0b.jpg",
                ProductId = Guid.Parse("39146688-1CAF-4C6F-85E0-DE06D69F5C91"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("3F634AB4-C671-44A6-8187-2A75D2381398"),
                DisplayPosition = 1,
                Name = "3f634ab4-c671-44a6-8187-2a75d2381398.jpg",
                ProductId = Guid.Parse("33C7CAD4-DC9F-4AE7-ACE0-566F3364147B"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("2FA31857-F872-42FB-BE1D-87153212E909"),
                DisplayPosition = 1,
                Name = "2fa31857-f872-42fb-be1d-87153212e909.jpg",
                ProductId = Guid.Parse("8FB0644A-E27A-4D4E-B108-72254314168B"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("56EDA72B-9054-4C72-8FEE-100D02616501"),
                DisplayPosition = 1,
                Name = "56eda72b-9054-4c72-8fee-100d02616501.jpg",
                ProductId = Guid.Parse("6D2CE8E1-7D2C-4507-82DE-0ACE8C95A84E"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("E33F21A4-2699-4F7B-8F32-7AEF0C967F4E"),
                DisplayPosition = 2,
                Name = "e33f21a4-2699-4f7b-8f32-7aef0c967f4e.jpg",
                ProductId = Guid.Parse("6D2CE8E1-7D2C-4507-82DE-0ACE8C95A84E"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("556A3748-EA9F-4B11-8641-8C52465D4536"),
                DisplayPosition = 1,
                Name = "556a3748-ea9f-4b11-8641-8c52465d4536.jpg",
                ProductId = Guid.Parse("190E6ABE-202C-4881-B01E-9A6DB2B46D7D"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("61DE1D06-A494-47F7-A19F-20AECE88B471"),
                DisplayPosition = 1,
                Name = "61de1d06-a494-47f7-a19f-20aece88b471.jpg",
                ProductId = Guid.Parse("CF8319D0-9DBF-414B-BCC8-082969710CAE"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("E84978AF-9C36-4E38-8C50-28006FB61A41"),
                DisplayPosition = 1,
                Name = "e84978af-9c36-4e38-8c50-28006fb61a41.jpg",
                ProductId = Guid.Parse("4172894C-AA31-4A88-BCD6-D9B64A94D53D"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("2F9A836A-05F6-4952-A8A4-68C772E363F9"),
                DisplayPosition = 1,
                Name = "2f9a836a-05f6-4952-a8a4-68c772e363f9.jpg",
                ProductId = Guid.Parse("1CF044B2-3CEE-435C-BC4A-8996A06667F1"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("502DCDA9-AF46-42B3-A577-27AD62E2D118"),
                DisplayPosition = 1,
                Name = "502dcda9-af46-42b3-a577-27ad62e2d118.jpg",
                ProductId = Guid.Parse("8C692DEE-6855-4C9F-8004-AEBBBC0987FD"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("3222A439-D9A0-4D52-B274-11444F6502DC"),
                DisplayPosition = 1,
                Name = "3222a439-d9a0-4d52-b274-11444f6502dc.jpg",
                ProductId = Guid.Parse("6D2B80F7-A8A5-4A10-9C18-A8E498B11B71"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("3EACC364-7913-4E23-9E42-7911BFED3914"),
                DisplayPosition = 1,
                Name = "3eacc364-7913-4e23-9e42-7911bfed3914.jpg",
                ProductId = Guid.Parse("72C2F548-720D-4D43-B622-4A1DADB1F721"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("44D9873E-81FF-4BF7-9FD6-B3629B976054"),
                DisplayPosition = 1,
                Name = "44d9873e-81ff-4bf7-9fd6-b3629b976054.jpg",
                ProductId = Guid.Parse("F19CB8D5-1BAF-4612-B09E-6CC9E99FD504"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("56A9ADFE-E8BB-44B4-90AE-BA58E6F84B70"),
                DisplayPosition = 1,
                Name = "56a9adfe-e8bb-44b4-90ae-ba58e6f84b70.jpg",
                ProductId = Guid.Parse("227DB142-E978-42CA-9969-220CE790EDA3"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("E88C6113-5B2A-445F-8667-C3C8F3A68C28"),
                DisplayPosition = 1,
                Name = "e88c6113-5b2a-445f-8667-c3c8f3a68c28.jpg",
                ProductId = Guid.Parse("FBF934E5-35FF-425E-9A29-A5CE4D9E26F8"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("D2C20AEF-1BEF-426B-8E0F-01B886AB9CD4"),
                DisplayPosition = 1,
                Name = "d2c20aef-1bef-426b-8e0f-01b886ab9cd4.jpg",
                ProductId = Guid.Parse("5CCDA279-7A1A-4469-959A-C4872D03655D"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("388F80F0-0D07-42AD-A7B8-73CAFB1F042D"),
                DisplayPosition = 1,
                Name = "388f80f0-0d07-42ad-a7b8-73cafb1f042d.jpg",
                ProductId = Guid.Parse("B6900844-DEE4-4156-908D-297504360CB8"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("1C2A891D-EE06-4E99-B0FA-04BC6AD7B0E5"),
                DisplayPosition = 1,
                Name = "1c2a891d-ee06-4e99-b0fa-04bc6ad7b0e5.jpg",
                ProductId = Guid.Parse("FFC8DB59-54B6-4C37-8819-179B1F62A9B1"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            },

            new Image
            {
                Id = Guid.Parse("13E66701-4991-4EB2-B007-0BCD5202B639"),
                DisplayPosition = 1,
                Name = "13e66701-4991-4eb2-b007-0bcd5202b639.jpg",
                ProductId = Guid.Parse("66DE29D8-E24E-4035-92C9-565C7C2FC1CB"),
                AddedOn = DateTime.Now,
                AddedBy = Guid.Parse("B0BE86E1-037B-4F2C-84F3-6F456762CA8E")
            }
        ];
    }
}