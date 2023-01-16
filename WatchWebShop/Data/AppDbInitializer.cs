using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using WatchWebShop.Models;

namespace WatchWebShop.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();

                //Categories
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(
                        new Category { Name = "Herrenuhren", TaxRate = 0.20 },
                        new Category { Name = "Damenuhren", TaxRate = 0.20 },
                        new Category { Name = "Kinderuhren", TaxRate = 0.20 }
                    );
                    context.SaveChanges();
                }

                //Manufacturers
                if (!context.Manufacturers.Any())
                {
                    context.Manufacturers.AddRange(
                        new Manufacturer { Name = "Casio", LogoPath = "~/Data/ManufacturersLogo/Casio.jpg" },
                        new Manufacturer { Name = "Certina", LogoPath = "~/Data/ManufacturersLogo/Certina.jpg" },
                        new Manufacturer { Name = "Citizen", LogoPath = "~/Data/ManufacturersLogo/Citizen.jpg" },
                        new Manufacturer { Name = "Seiko", LogoPath = "~/Data/ManufacturersLogo/Seiko.png" },
                        new Manufacturer { Name = "Tissot", LogoPath = "~/Data/ManufacturersLogo/Tissot.jpg" }
                    );
                    context.SaveChanges();
                }

                //Products
                if (!context.Products.Any())
                {
                    context.Products.AddRange(new List<Product>()
                    {
                        new Product()
                        {
                            Name = "Casio G-Shock GA-100-1A1ER",
                            Description = "Mit der markanten Optik, ihrer technischen Ausstattung und der einzigartig robusten Konstruktion ist die G-Shock der perfekte Begleiter für Männer, die kompromisslos ihren eigenen Weg gehen.",
                            UnitPriceNetto = 79.20,
                            ImagePath = "https://bilder.ottoversand.at/i/empiriecom/1df9380674054f606e12ed4537167c45?$format_dv_zoom_desk_35$",
                            CategoryId = 1,
                            ManufacturerId = 1
                        },

                        new Product()
                        {
                            Name = "Casio G-Shock GM-S5600LC-7",
                            Description = "Die G-SHOCK GM-5600 und ihr kleines Gegenstück, die GM-S5600, verfügen über eine edle Metall-Lünette und ein weißes Armband mit marineblauen Akzenten. Die Stoffband-Textur des Originals wurde in Form von Glasdruck auf den Zifferblättern wieder aufgegriffen, um ein hochwertiges Design zu schaffen.",
                            UnitPriceNetto = 127.20,
                            ImagePath = "https://www.casio.com/content/dam/casio/product-info/locales/at/de/timepiece/product/watch/G/GM/GMS/gm-s5600lc-7/assets/GM-S5600LC-7.png",
                            CategoryId = 2,
                            ManufacturerId = 1
                        },

                        new Product()
                        {
                            Name = "Citizen Rebel Pilot by Star Wars JG2108-52W",
                            Description = "May the Force be with you, with this Rebel Pilot inspired watch from Citizen celebrating the Star Wars™ galaxy. Inspired by an original Citizen analog-digital watch from the 1980s, this stainless steel rectangular case watch is packed with features, including dual time and subdials with both the Rebel Alliance logo and the X-Wing with Rebel inspired colors. It also includes an alarm, the digital time, and the temperature, making it a unique and stand-out timepiece. This watch requires a battery. Whether you are a long-time fan who still believes the original trilogy can never be topped, or you’re just being introduced to this galaxy far, far away, this is a Star Wars watch every collector will want to show off.",
                            UnitPriceNetto = 240.00,
                            ImagePath = "https://citizenwatch.widen.net/content/qigtdjwdkh/jpeg/Rebel+Pilot.jpeg?u=41zuoe&width=500&height=625&quality=80&crop=false&keep=c&color=F9F8F6",
                            CategoryId = 3,
                            ManufacturerId = 3
                        }
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
