using ITCMS_HUIT.DTO;
using ITCMS_HUIT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services;
using System;
using System.Data.SqlClient;
using System.IO;

namespace ITCMS_HUIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoSoDuLieuController : ControllerBase
    {
        private readonly CoSoDuLieuService _coSoDuLieuService;
        public CoSoDuLieuController(CoSoDuLieuService coSoDuLieuService)
        {
            _coSoDuLieuService = coSoDuLieuService;
        }

        [HttpPost]
        [Route("backup")]
        public IActionResult BackupDatabase()
        {
            try
            {
                string backupFilePath = _coSoDuLieuService.BackupDatabase();

                return Ok(new { Message = "Backup thành công.", FilePath = backupFilePath });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi trong quá trình sao lưu cơ sở dữ liệu.", Error = ex.Message });
            }
        }

        [HttpPost]
        [Route("restore")]
        public IActionResult RestoreDatabase(string backupFileName)
        {
            try
            {
                _coSoDuLieuService.RestoreDatabase(backupFileName);

                return Ok(new { Status = "Success", Message = "Restore thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("backup-files")]
        public IActionResult GetBackupFiles()
        {
            try
            {
                List<string> backupFiles = _coSoDuLieuService.GetBackupFiles();

                return Ok(new { Status = "Success", Message = "Danh sách các tệp tin backup", Data = backupFiles });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = ex.Message });
            }
        }

    }
}
