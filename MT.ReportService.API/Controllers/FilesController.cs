using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MT.ReportService.Core.Entity;
using MT.ReportService.Core.Services;
using MT.ReportService.Data;
using System;
using System.IO;
using System.Threading.Tasks;
using FileStatus = MT.ReportService.API.Dtos.FileStatus;

namespace MT.ReportService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IService<Report> _reportService;
        public FilesController(AppDbContext appDbContext, IService<Report> reportService)
        {
            _appDbContext = appDbContext;
            _reportService = reportService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, int fileId)
        {
            if (file is not { Length: > 0 }) return BadRequest();
            
            var filePath = fileId.ToString()+"_" + DateTime.Now.Day.ToString()+ DateTime.Now.Month.ToString()+ DateTime.Now.Year.ToString() +"_"+ Path.GetExtension(file.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", filePath);
            try
            {
                using FileStream stream = new FileStream(path, FileMode.Create);

                await file.CopyToAsync(stream);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            



            var report = await _reportService.GetByIdAsync(fileId);

            report.CreatedDate = DateTime.Now;
            report.ReportState = (Core.Entity.FileStatus)FileStatus.Completed;

            var updatedReport =  _reportService.Update(report);

            return Ok();
        }
    }
}
