using ITCMS_HUIT.DTO;
using ITCMS_HUIT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.IO;

namespace ITCMS_HUIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITCMS_HUITContext _context;

        public DatabaseController(ITCMS_HUITContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("backup")]
        public IActionResult BackupDatabase([FromBody] BackupConfiguration backupConfig)
        {
            try
            {
                // Ensure the backup directory exists
                Directory.CreateDirectory(backupConfig.BackupDirectory!);

                // Generate a unique file name based on the current timestamp
                string backupFileName = $"Backup_{DateTime.Now:yyyyMMddHHmmss}.bak";
                string backupFilePath = Path.Combine(backupConfig.BackupDirectory!, backupFileName);

                // Use EF Core to execute a raw SQL query for backup
                _context.Database.ExecuteSqlRaw($"BACKUP DATABASE {_context.Database.GetDbConnection().Database} TO DISK = '{backupFilePath}'");

                return Ok(new { Status = "Success", Message = "Backup successful!", FilePath = backupFilePath });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("restore")]
        public IActionResult RestoreDatabase([FromBody] BackupConfiguration restoreConfig)
        {
            try
            {
                _context.Database.ExecuteSqlRaw($"USE [master]; ALTER DATABASE {_context.Database.GetDbConnection().Database} SET SINGLE_USER WITH ROLLBACK IMMEDIATE; RESTORE DATABASE {_context.Database.GetDbConnection().Database} FROM DISK = '{restoreConfig.BackupDirectory}' WITH REPLACE;");

                return Ok(new { Status = "Success", Message = "Restore successful!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = ex.Message });
            }
        }

    }
}
