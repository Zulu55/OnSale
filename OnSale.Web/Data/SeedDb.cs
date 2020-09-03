using Microsoft.EntityFrameworkCore;
using OnSale.Common.Entities;
using OnSale.Common.Enums;
using OnSale.Web.Data.Entities;
using OnSale.Web.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnSale.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly Random _random;

        public SeedDb(DataContext context, IUserHelper userHelper, IBlobHelper blobHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Juan", "Zuluaga", "jzuluaga55@hotmail.com", "322 311 4620", "Calle Luna Calle Sol", UserType.Admin);
            await CheckCategoriesAsync();
            await CheckProductsAsync();
        }

        private async Task CheckProductsAsync()
        {
            if (!_context.Products.Any())
            {
                User user = await _userHelper.GetUserAsync("jzuluaga55@hotmail.com");
                Category mascotas = await _context.Categories.FirstOrDefaultAsync(c => c.Name == "Mascotas");
                Category ropa = await _context.Categories.FirstOrDefaultAsync(c => c.Name == "Ropa");
                Category tecnologia = await _context.Categories.FirstOrDefaultAsync(c => c.Name == "Tecnología");
                string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris gravida, nunc vel tristique cursus, velit nibh pulvinar enim, non pulvinar lorem leo eget felis. Proin suscipit dignissim nisl, at elementum justo laoreet sed. In tortor nibh, auctor quis est gravida, blandit elementum nulla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Integer placerat nisi dui, id rutrum nisi viverra at. Interdum et malesuada fames ac ante ipsum primis in faucibus. Pellentesque sodales sollicitudin tempor. Fusce volutpat, purus sit amet placerat gravida, est magna gravida risus, a ultricies augue magna vel dolor. Fusce egestas venenatis velit, a ultrices purus aliquet sed. Morbi lacinia purus sit amet nisi vulputate mollis. Praesent in volutpat tortor. Etiam ac enim id ligula rutrum semper. Sed mattis erat sed condimentum congue. Vestibulum consequat tristique consectetur. Nunc in lorem in sapien vestibulum aliquet a vel leo.";
                await AddProductAsync(mascotas, lorem, "Bulldog Frances", 2500000M, new string[] { "Bulldog1", "Bulldog2", "Bulldog3", "Bulldog4" }, user);
                await AddProductAsync(ropa, lorem, "Buso GAP Hombre", 85000M, new string[] { "BusoGAP1", "BusoGAP2" }, user);
                await AddProductAsync(tecnologia, lorem, "iPhone 11", 3500000M, new string[] { "iPhone1", "iPhone2", "iPhone3", "iPhone4", "iPhone5" }, user);
                await AddProductAsync(tecnologia, lorem, "iWatch \"42", 2100000M, new string[] { "iWatch" }, user);
                await AddProductAsync(ropa, lorem, "Tennis Adidas", 250000M, new string[] { "Adidas" }, user);
                await AddProductAsync(mascotas, lorem, "Collie", 350000M, new string[] { "Collie1", "Collie2", "Collie3", "Collie4", "Collie5" }, user);
                await AddProductAsync(tecnologia, lorem, "MacBook Pro 16\" 1TB", 12000000M, new string[] { "MacBookPro1", "MacBookPro2", "MacBookPro3", "MacBookPro4" }, user);
                await AddProductAsync(ropa, lorem, "Sudadera Mujer", 95000M, new string[] { "Sudadera1", "Sudadera2", "Sudadera3", "Sudadera4", "Sudadera5" }, user);
                await _context.SaveChangesAsync();
            }
        }

        private async Task AddProductAsync(Category category, string description, string name, decimal price, string[] images, User user)
        {
            Product product = new Product
            {
                Category = category,
                Description = description,
                IsActive = true,
                Name = name,
                Price = price,
                ProductImages = new List<ProductImage>(),
                Qualifications = GetRandomQualifications(description, user)
            };

            foreach (string image in images)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\images", $"{image}.png");
                Guid imageId = await _blobHelper.UploadBlobAsync(path, "products");
                product.ProductImages.Add(new ProductImage { ImageId = imageId });
            }

            _context.Products.Add(product);
        }

        private ICollection<Qualification> GetRandomQualifications(string description, User user)
        {
            List<Qualification> qualifications = new List<Qualification>();
            for (int i = 0; i < 10; i++)
            {
                qualifications.Add(new Qualification
                {
                    Date = DateTime.UtcNow,
                    Remarks = description,
                    Score = _random.Next(1, 5),
                    User = user
                });
            }

            return qualifications;
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                await AddCategoryAsync("Ropa");
                await AddCategoryAsync("Tecnología");
                await AddCategoryAsync("Mascotas");
                await _context.SaveChangesAsync();
            }
        }

        private async Task AddCategoryAsync(string name)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\images", $"{name}.png");
            Guid imageId = await _blobHelper.UploadBlobAsync(path, "categories");
            _context.Categories.Add(new Category { Name = name, ImageId = imageId });
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task<User> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string address,
            UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = _context.Cities.FirstOrDefault(),
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            return user;
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "Colombia",
                    Departments = new List<Department>
                    {
                        new Department
                        {
                            Name = "Antioquia",
                            Cities = new List<City>
                            {
                                new City { Name = "Medellín" },
                                new City { Name = "Envigado" },
                                new City { Name = "Itagüí" }
                            }
                        },
                        new Department
                        {
                            Name = "Bogotá",
                            Cities = new List<City>
                            {
                                new City { Name = "Bogotá" }
                            }
                        },
                        new Department
                        {
                            Name = "Valle del Cauca",
                            Cities = new List<City>
                            {
                                new City { Name = "Calí" },
                                new City { Name = "Buenaventura" },
                                new City { Name = "Palmira" }
                            }
                        }
                    }
                });
                _context.Countries.Add(new Country
                {
                    Name = "USA",
                    Departments = new List<Department>
                    {
                        new Department
                        {
                            Name = "California",
                            Cities = new List<City>
                            {
                                new City { Name = "Los Angeles" },
                                new City { Name = "San Diego" },
                                new City { Name = "San Francisco" }
                            }
                        },
                        new Department
                        {
                            Name = "Illinois",
                            Cities = new List<City>
                            {
                                new City { Name = "Chicago" },
                                new City { Name = "Springfield" }
                            }
                        }
                    }
                });
                await _context.SaveChangesAsync();
            }
        }
    }
}