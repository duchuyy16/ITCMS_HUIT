using ITCMS_HUIT.DTO;
using ITCMS_HUIT.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = UserRoles.Teacher + "," + UserRoles.Admin)]
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
                List<string> backupFilePath = _coSoDuLieuService.BackupDatabase();

                var apiResponse = new ApiResponse<List<string>>
                {
                    Status = "Thành công",
                    Message = "Backup thành công.",
                    Data = backupFilePath
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Lỗi", Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("restore/{backupFileName}")] 
        public IActionResult RestoreDatabase(string backupFileName)
        {
            try
            {
                bool restoreResult = _coSoDuLieuService.RestoreDatabase(backupFileName);

                var apiResponse = new ApiResponse<bool>
                {
                    Status = restoreResult ? "Thành công" : "Lỗi",
                    Message = restoreResult ? "Restore thành công!" : "Không thể khôi phục cơ sở dữ liệu.",
                    Data = restoreResult
                };

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Lỗi", Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("backup-files")]
        public IActionResult GetBackupFiles()
        {
            try
            {
                List<string> backupFiles = _coSoDuLieuService.GetBackupFiles();

                var apiResponse = new ApiResponse<List<string>>
                {
                    Status = "Thành công",
                    Message = "Danh sách các tệp tin backup",
                    Data = backupFiles
                };

                return Ok(apiResponse);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Lỗi", Message = ex.Message });
            }
        }     
    }
}
