using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using MT.ReportService.API.Dtos;
using MT.ReportService.API.RabbitMQ;
using MT.ReportService.Core.Entity;
using MT.ReportService.Core.Services;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using FileStatus = MT.ReportService.Core.Entity.FileStatus;

namespace MT.ReportService.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
       
        private readonly IService<Report> _reportService;
        private readonly IMapper _mapper;
        private readonly RabbitMQPublisher _rabbitMQPublisher;

        public ReportController( IService<Report> reportService, IMapper mapper, RabbitMQPublisher rabbitMQPublisher)
        {
            
            _reportService = reportService;
            _mapper = mapper;
            _rabbitMQPublisher = rabbitMQPublisher;


        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetReport(int id) //db de Id var mı yok mu diye bir kontrol yapılmadı! Bir filter yazılabilir kod tekrarı olmaması için
        {
            var report = await _reportService.GetByIdAsync(id);
            if (report != null) { return Ok(_mapper.Map<ReportDto>(report)); }
            return BadRequest("Gönderdiğiniz id ye ait rapor bulunmuyor");

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> CreateReport(int id)
        {
            Report report = new()
            {
                UUID = id,
                ReportState = FileStatus.Creating,
                RequestDate = DateTime.Now,

            };
            var newReport = await _reportService.AddAsync(report);

            //burada RAbbitmq
            
            _rabbitMQPublisher.Publish(new CreateFileMessage
            {
                FileId = report.id,
                UserId = report.UUID
            });


        



            return Created(String.Empty, newReport);
        }





      
       
        [HttpGet]
        public async Task<IActionResult> GetAllReports()
        {
            var reports = await _reportService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<ReportDto>>(reports));
        }
    }
}
