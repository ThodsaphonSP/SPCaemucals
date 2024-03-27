using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SPCaemucals.Data.Models;

namespace SPCaemucals.Data.Identities;

public class ProductSeed
{
    private readonly ApplicationDbContext _dbContext;

    public ProductSeed(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public string AdminPK { get; set; } = "1";

    public List<Category> Category { get; set; }


    public List<Vendor> Vendors { get; set; }

    public List<UnitOfMeasurement?> Measure { get; set; }

    public List<Product> Products { get; set; }

    public List<ApplicationUser> Users { get; set; }

    public List<Company> Companies { get; set; }

    public List<Title> Titles { get; set; }

    public List<Address> Addresses { get; set; }

    public List<PostalCode> PostalCodes { get; set; }

    public List<SubDistrict> SubDistrict { get; set; }

    public List<District> District { get; set; }


    public List<Province> Provinces { get; set; }

    public async Task MainSeed()
    {
        var strategy = _dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                this.Provinces = await _dbContext.Provinces.Select(x => x).Take(10).ToListAsync();

                if (!this.Provinces.Any())
                {
                    this.Provinces = GetProvinces();
                    await _dbContext.Provinces.AddRangeAsync(this.Provinces);
                    await _dbContext.SaveChangesAsync();
                }

                this.District = await _dbContext.Districts.Select(x => x).Take(10).ToListAsync();

                if (!this.District.Any())
                {
                    string districtPath = Path.Combine(Directory.GetCurrentDirectory(), "Script", "district.sql");

                    string sqlCommand = await File.ReadAllTextAsync(districtPath);


                    await _dbContext.Database.ExecuteSqlRawAsync(sqlCommand);
                }


                this.SubDistrict = await _dbContext.SubDistricts.Select(x => x).Take(10).ToListAsync();

                if (!this.SubDistrict.Any())
                {
                    string subDistrictPath =
                        Path.Combine(Directory.GetCurrentDirectory(), "Script", "SubDistricts.sql");
                    string sqlCommand = await File.ReadAllTextAsync(subDistrictPath);


                    await _dbContext.Database.ExecuteSqlRawAsync(sqlCommand);
                }


                this.PostalCodes = await _dbContext.PostalCodes.Select(x => x).Take(10).ToListAsync();

                if (!this.PostalCodes.Any())
                {
                    string pstPath = Path.Combine(Directory.GetCurrentDirectory(), "Script", "PostalCodes.sql");

                    string sqlCommand = await File.ReadAllTextAsync(pstPath);


                    await _dbContext.Database.ExecuteSqlRawAsync(sqlCommand);
                }


                this.Addresses = await _dbContext.Addresses.Select(x => x).Take(10).ToListAsync();

                if (!this.Addresses.Any())
                {
                    this.Addresses = GetAddresses();
                    await _dbContext.Addresses.AddRangeAsync(this.Addresses);
                    await _dbContext.SaveChangesAsync();
                }

                this.Titles = await _dbContext.Titles.Select(x => x).Take(10).ToListAsync();

                if (!this.Titles.Any())
                {
                    this.Titles = GetTitle();
                    await _dbContext.Titles.AddRangeAsync(this.Titles);
                    await _dbContext.SaveChangesAsync();
                }

                this.Companies = await _dbContext.Company.Select(x => x).Take(10).ToListAsync();

                if (!this.Companies.Any())
                {
                    this.Companies = GetCompanies();
                    await _dbContext.Company.AddRangeAsync(this.Companies);
                    await _dbContext.SaveChangesAsync();
                }

                this.Users = await _dbContext.Users.Select(x => x).Take(10).ToListAsync();

                if (!this.Users.Any())
                {
                    this.Users = GetAdminUser();
                    await _dbContext.Users.AddRangeAsync(this.Users);
                    await _dbContext.SaveChangesAsync();
                }

                this.Vendors = await _dbContext.Vendors.Select(x => x).Take(10).ToListAsync();
                if (!Vendors.Any())
                {
                    List<Vendor> vendors = new List<Vendor>()
                    {
                        new Vendor() { Name = "Vendor A" },
                        new Vendor() { Name = "Vendor B" },
                    };


                    _dbContext.Vendors.AddRange(vendors);

                    await _dbContext.SaveChangesAsync();

                    this.Vendors = vendors;
                }

                this.Measure = await _dbContext.UnitOfMeasurements.Select(x => x).Take(10).ToListAsync();

                if (!Measure.Any())
                {
                    var unitOfMeasurement = new UnitOfMeasurement { Name = "Pcs" };


                    _dbContext.UnitOfMeasurements.Add(unitOfMeasurement);

                    await _dbContext.SaveChangesAsync();

                    this.Measure.Add(unitOfMeasurement);
                }

                this.Category = await _dbContext.Categories.Select(x => x).Take(10).ToListAsync();
                
                if (!this.Category.Any())
                {
                    var category = new Category
                    {
                        Name = "Chemicals", CreatedById = AdminPK
                    };

                    _dbContext.Categories.Add(category);
                    this.Category.Add(category);
                }
                
                this.Products = await _dbContext.Products.Select(x => x).Take(10).ToListAsync();

                if (!this.Products.Any())
                {
                    this.Products = GetProducts();
                    await _dbContext.Products.AddRangeAsync(this.Products);
                    await _dbContext.SaveChangesAsync();
                }


                transaction.Commit();


                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw;
            }
        });
    }

    private List<Product> GetProducts()
    {
        var result = new List<Product>()
        {
            new()
            {
                Name = "product1",
                Code = "code1",
                Detail = "detail1",
                UnitOfMeasurement = this.Measure[0],
                StandardPrice = 10.00m,
                Multiplier = 1.00m,
                Quantity = 100,
                Price = 100.00m,
                Category = this.Category[0],
                Vendor = this.Vendors[0],
                CreatedById = this.Users[0].Id
            },
            new()
            {
                Name = "product2",
                Code = "code2",
                Detail = "detail2",
                UnitOfMeasurement = this.Measure[0],
                StandardPrice = 20.00m,
                Multiplier = 2.00m,
                Quantity = 200,
                Price = 200.00m,
                Category = this.Category[0],
                Vendor = this.Vendors[0],
                CreatedById = this.Users[0].Id
            },
            new()
            {
                Name = "product3",
                Code = "code3",
                Detail = "detail3",
                UnitOfMeasurement = this.Measure[0],
                StandardPrice = 20.00m,
                Multiplier = 2.00m,
                Quantity = 200,
                Price = 200.00m,
                Category = this.Category[0],
                Vendor = this.Vendors[0],
                CreatedById = this.Users[0].Id
            },
        };


        return result;
    }

    private List<ApplicationUser> GetAdminUser()
    {
        PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();

        var adminUser = new ApplicationUser
        {
            Id = "1",
            UserName = "S&P_01",
            FirstName = "John",
            LastName = "Doe",
            NormalizedUserName = "S&P_01",
            PhoneNumber = "0918131505",
            Email = "admin@sw.com",
            NormalizedEmail = "ADMIN@SW.COM",
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null, "pass@word"),
            SecurityStamp = string.Empty,
            Company = this.Companies[0]
        };

        return new List<ApplicationUser>() { adminUser };
    }

    private List<Company> GetCompanies()
    {
        return new List<Company>()
        {
            new Company()
            {
                CompanyName = "s&p",
                Address = this.Addresses[0]
            }
        };
    }


    private List<Title> GetTitle()
    {
        List<Title> titles = new List<Title>()
        {
            new Title()
            {
                Name = "นาย"
            },
            new Title()
            {
                Name = "นางสาว"
            }
        };

        return titles;
    }

    private List<Address> GetAddresses()
    {
        this.District = _dbContext.Districts.Select(x => x).ToList();
        
        if (!this.District.Any())
        {
            throw new Exception("District is empty");
        }
        
        
        this.SubDistrict = _dbContext.SubDistricts.Select(x => x).ToList();
        
        if (!this.SubDistrict.Any())
        {
            throw new Exception("subdistrict is empty");
        }
        
        this.PostalCodes = _dbContext.PostalCodes.Select(x => x).ToList();
        
        if (!this.PostalCodes.Any())
        {
            throw new Exception("postal is empty");
        }
        
        List<Address> addresses = new List<Address>()
        {
            new Address()
            {
                AddressDetail = "111 หมู่7 ", Province = this.Provinces[0],
                District = this.District[0],
                SubDistrict = this.SubDistrict[0],
                PostalCode = this.PostalCodes[0]
            }
        };
        return addresses;
    }

    private List<Province> GetProvinces()
    {
        List<Province> provinces = new List<Province>()
        {
            new Province { Id = 10, ThaiName = "กรุงเทพมหานคร" },
            new Province { Id = 11, ThaiName = "สมุทรปราการ" },
            new Province { Id = 12, ThaiName = "นนทบุรี" },
            new Province { Id = 13, ThaiName = "ปทุมธานี" },
            new Province { Id = 14, ThaiName = "พระนครศรีอยุธยา" },
            new Province { Id = 15, ThaiName = "อ่างทอง" },
            new Province { Id = 16, ThaiName = "ลพบุรี" },
            new Province { Id = 17, ThaiName = "สิงห์บุรี" },
            new Province { Id = 18, ThaiName = "ชัยนาท" },
            new Province { Id = 19, ThaiName = "สระบุรี" },
            new Province { Id = 20, ThaiName = "ชลบุรี" },
            new Province { Id = 21, ThaiName = "ระยอง" },
            new Province { Id = 22, ThaiName = "จันทบุรี" },
            new Province { Id = 23, ThaiName = "ตราด" },
            new Province { Id = 24, ThaiName = "ฉะเชิงเทรา" },
            new Province { Id = 25, ThaiName = "ปราจีนบุรี" },
            new Province { Id = 26, ThaiName = "นครนายก" },
            new Province { Id = 27, ThaiName = "สระแก้ว" },
            new Province { Id = 30, ThaiName = "นครราชสีมา" },
            new Province { Id = 31, ThaiName = "บุรีรัมย์" },
            new Province { Id = 32, ThaiName = "สุรินทร์" },
            new Province { Id = 33, ThaiName = "ศรีสะเกษ" },
            new Province { Id = 34, ThaiName = "อุบลราชธานี" },
            new Province { Id = 35, ThaiName = "ยโสธร" },
            new Province { Id = 36, ThaiName = "ชัยภูมิ" },
            new Province { Id = 37, ThaiName = "อำนาจเจริญ" },
            new Province { Id = 38, ThaiName = "บึงกาฬ" },
            new Province { Id = 39, ThaiName = "หนองบัวลำภู" },
            new Province { Id = 40, ThaiName = "ขอนแก่น" },
            new Province { Id = 41, ThaiName = "อุดรธานี" },
            new Province { Id = 42, ThaiName = "เลย" },
            new Province { Id = 43, ThaiName = "หนองคาย" },
            new Province { Id = 44, ThaiName = "มหาสารคาม" },
            new Province { Id = 45, ThaiName = "ร้อยเอ็ด" },
            new Province { Id = 46, ThaiName = "กาฬสินธุ์" },
            new Province { Id = 47, ThaiName = "สกลนคร" },
            new Province { Id = 48, ThaiName = "นครพนม" },
            new Province { Id = 49, ThaiName = "มุกดาหาร" },
            new Province { Id = 50, ThaiName = "เชียงใหม่" },
            new Province { Id = 51, ThaiName = "ลำพูน" },
            new Province { Id = 52, ThaiName = "ลำปาง" },
            new Province { Id = 53, ThaiName = "อุตรดิตถ์" },
            new Province { Id = 54, ThaiName = "แพร่" },
            new Province { Id = 55, ThaiName = "น่าน" },
            new Province { Id = 56, ThaiName = "พะเยา" },
            new Province { Id = 57, ThaiName = "เชียงราย" },
            new Province { Id = 58, ThaiName = "แม่ฮ่องสอน" },
            new Province { Id = 60, ThaiName = "นครสวรรค์" },
            new Province { Id = 61, ThaiName = "อุทัยธานี" },
            new Province { Id = 62, ThaiName = "กำแพงเพชร" },
            new Province { Id = 63, ThaiName = "ตาก" },
            new Province { Id = 64, ThaiName = "สุโขทัย" },
            new Province { Id = 65, ThaiName = "พิษณุโลก" },
            new Province { Id = 66, ThaiName = "พิจิตร" },
            new Province { Id = 67, ThaiName = "เพชรบูรณ์" },
            new Province { Id = 70, ThaiName = "ราชบุรี" },
            new Province { Id = 71, ThaiName = "กาญจนบุรี" },
            new Province { Id = 72, ThaiName = "สุพรรณบุรี" },
            new Province { Id = 73, ThaiName = "นครปฐม" },
            new Province { Id = 74, ThaiName = "สมุทรสาคร" },
            new Province { Id = 75, ThaiName = "สมุทรสงคราม" },
            new Province { Id = 76, ThaiName = "เพชรบุรี" },
            new Province { Id = 77, ThaiName = "ประจวบคีรีขันธ์" },
            new Province { Id = 80, ThaiName = "นครศรีธรรมราช" },
            new Province { Id = 81, ThaiName = "กระบี่" },
            new Province { Id = 82, ThaiName = "พังงา" },
            new Province { Id = 83, ThaiName = "ภูเก็ต" },
            new Province { Id = 84, ThaiName = "สุราษฎร์ธานี" },
            new Province { Id = 85, ThaiName = "ระนอง" },
            new Province { Id = 86, ThaiName = "ชุมพร" },
            new Province { Id = 90, ThaiName = "สงขลา" },
            new Province { Id = 91, ThaiName = "สตูล" },
            new Province { Id = 92, ThaiName = "ตรัง" },
            new Province { Id = 93, ThaiName = "พัทลุง" },
            new Province { Id = 94, ThaiName = "ปัตตานี" },
            new Province { Id = 95, ThaiName = "ยะลา" },
            new Province { Id = 96, ThaiName = "นราธิวาส" }
        };
        return provinces;
    }
}