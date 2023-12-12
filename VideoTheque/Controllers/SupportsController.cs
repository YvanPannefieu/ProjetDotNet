﻿using Microsoft.AspNetCore.Mvc;
using VideoTheque.Businesses.Supports;
using VideoTheque.DTOs;
using VideoTheque.ViewModels;
using Mapster;

namespace VideoTheque.Controllers
{
    [ApiController]
    [Route("supports")]
    public class SupportsController : ControllerBase
    {
        private readonly ISupportsBusiness _supportsBusiness;
        protected readonly ILogger<SupportsController> _logger;

        public SupportsController(ILogger<SupportsController> logger, ISupportsBusiness supportsBusiness)
        {
            _logger = logger;
            _supportsBusiness = supportsBusiness;
        }

        [HttpGet]
        public async Task<List<SupportViewModel>> GetSupports() => (await _supportsBusiness.GetSupports()).Adapt<List<SupportViewModel>>();

        [HttpGet("{id}")]
        public async Task<SupportViewModel> GetSupport([FromRoute] int id) => _supportsBusiness.GetSupport(id).Adapt<SupportViewModel>();
    }
}