using ITCMS_HUIT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CoSoDuLieuService
    {
        private readonly IConfiguration _configuration;
        private readonly ITCMS_HUITContext _context;

        public CoSoDuLieuService(IConfiguration configuration, ITCMS_HUITContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public string BackupDatabase()
        {
            // Ensure the backup directory exists
            Directory.CreateDirectory(_configuration["BackupConfiguration:BackupDirectory"]);

            // Generate a unique file name based on the current timestamp
            string backupFileName = $"Backup_{DateTime.Now:yyyyMMddHHmmss}.bak";
            string backupFilePath = Path.Combine(_configuration["BackupConfiguration:BackupDirectory"], backupFileName);

            // Use EF Core to execute a raw SQL query for backup
            _context.Database.ExecuteSqlRaw($"BACKUP DATABASE {_context.Database.GetDbConnection().Database} " +
                $"TO DISK = '{backupFilePath}'");

            return backupFilePath;
        }

        //public bool RestoreDatabase(IFormFile file)
        //{
        //    string backupFileName = file.FileName;
        //    string backupDirectory = _configuration["BackupConfiguration:BackupDirectory"];
        //    string filePath = Path.Combine(backupDirectory, backupFileName);

        //    // Lưu file backup vào thư mục
        //    using (var fileStream = new FileStream(filePath, FileMode.Create))
        //    {
        //        file.CopyTo(fileStream);
        //    }

        //    string restoreSql = $"USE [master]; " +
        //                        $"ALTER DATABASE {_context.Database.GetDbConnection().Database} SET SINGLE_USER WITH ROLLBACK IMMEDIATE; " +
        //                        $"RESTORE DATABASE {_context.Database.GetDbConnection().Database} FROM DISK = '{filePath}' WITH REPLACE;";

        //    _context.Database.ExecuteSqlRaw(restoreSql);

        //    System.IO.File.Delete(filePath);

        //    return true;
        //}

        public bool RestoreDatabase(string backupFileName)
        {
            string backupDirectory = _configuration["BackupConfiguration:BackupDirectory"];
            string backupFilePath = Path.Combine(backupDirectory, backupFileName);

            if (!File.Exists(backupFilePath)) return false;

            string restoreSql = $"USE [master]; " +
                                $"ALTER DATABASE {_context.Database.GetDbConnection().Database} SET SINGLE_USER WITH ROLLBACK IMMEDIATE; " +
                                $"RESTORE DATABASE {_context.Database.GetDbConnection().Database} FROM DISK = '{backupFilePath}' WITH REPLACE;";

            _context.Database.ExecuteSqlRaw(restoreSql);

            File.Delete(backupFilePath);

            return true;
        }

        public List<string> GetBackupFiles()
        {
            List<string> backupFiles = new List<string>();

            string backupDirectory = _configuration["BackupConfiguration:BackupDirectory"];

            // Kiểm tra xem thư mục backup có tồn tại không
            if (Directory.Exists(backupDirectory))
            {
                // Lấy danh sách tất cả các tệp tin trong thư mục backup
                backupFiles = Directory.GetFiles(backupDirectory).ToList();
            }
            else
            {
                // Xử lý khi thư mục không tồn tại
                Console.WriteLine($"Thư mục backup '{backupDirectory}' không tồn tại.");
            }

            return backupFiles;
        }

    }
}
