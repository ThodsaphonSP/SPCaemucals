
using Microsoft.EntityFrameworkCore;

using SPCaemucals.Data.Identities; // Ensure this is the correct namespace

namespace SPCaemucals.Excel;

public class SeedProvince
{
    private readonly ApplicationDbContext _context;

    public SeedProvince(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task ExecuteSqlFromFileAsync()
    {
        if (_context.Provinces.Any())
        {
            return;
        }

     

   

        // Start a new transaction
        using (var transaction = _context.Database.BeginTransaction())
        {
            await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Provinces ON");

            await _context.Database.ExecuteSqlRawAsync(Command);

            await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Provinces OFF");

            // Commit transaction
            transaction.Commit();
        }
    }

    public string Command { get; set; } = """
                                          INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (10, N'กรุงเทพมหานคร');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (11, N'สมุทรปราการ');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (12, N'นนทบุรี');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (13, N'ปทุมธานี');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (14, N'พระนครศรีอยุธยา');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (15, N'อ่างทอง');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (16, N'ลพบุรี');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (17, N'สิงห์บุรี');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (18, N'ชัยนาท');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (19, N'สระบุรี');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (20, N'ชลบุรี');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (21, N'ระยอง');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (22, N'จันทบุรี');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (23, N'ตราด');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (24, N'ฉะเชิงเทรา');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (25, N'ปราจีนบุรี');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (26, N'นครนายก');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (27, N'สระแก้ว');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (30, N'นครราชสีมา');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (31, N'บุรีรัมย์');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (32, N'สุรินทร์');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (33, N'ศรีสะเกษ');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (34, N'อุบลราชธานี');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (35, N'ยโสธร');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (36, N'ชัยภูมิ');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (37, N'อำนาจเจริญ');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (38, N'บึงกาฬ');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (39, N'หนองบัวลำภู');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (40, N'ขอนแก่น');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (41, N'อุดรธานี');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (42, N'เลย');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (43, N'หนองคาย');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (44, N'มหาสารคาม');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (45, N'ร้อยเอ็ด');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (46, N'กาฬสินธุ์');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (47, N'สกลนคร');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (48, N'นครพนม');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (49, N'มุกดาหาร');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (50, N'เชียงใหม่');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (51, N'ลำพูน');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (52, N'ลำปาง');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (53, N'อุตรดิตถ์');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (54, N'แพร่');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (55, N'น่าน');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (56, N'พะเยา');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (57, N'เชียงราย');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (58, N'แม่ฮ่องสอน');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (60, N'นครสวรรค์');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (61, N'อุทัยธานี');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (62, N'กำแพงเพชร');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (63, N'ตาก');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (64, N'สุโขทัย');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (65, N'พิษณุโลก');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (66, N'พิจิตร');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (67, N'เพชรบูรณ์');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (70, N'ราชบุรี');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (71, N'กาญจนบุรี');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (72, N'สุพรรณบุรี');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (73, N'นครปฐม');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (74, N'สมุทรสาคร');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (75, N'สมุทรสงคราม');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (76, N'เพชรบุรี');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (77, N'ประจวบคีรีขันธ์');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (80, N'นครศรีธรรมราช');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (81, N'กระบี่');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (82, N'พังงา');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (83, N'ภูเก็ต');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (84, N'สุราษฎร์ธานี');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (85, N'ระนอง');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (86, N'ชุมพร');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (90, N'สงขลา');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (91, N'สตูล');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (92, N'ตรัง');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (93, N'พัทลุง');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (94, N'ปัตตานี');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (95, N'ยะลา');
                                              INSERT INTO sw.dbo.Provinces (Id, ThaiName) VALUES (96, N'นราธิวาส');
                                              
                                          """;
}