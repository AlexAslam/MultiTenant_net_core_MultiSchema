﻿using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MultiTenant_net_core_MultiSchema.DataModels.Entities;
using MultiTenant_net_core_MultiSchema.DataModels.Repository;
using MultiTenant_net_core_MultiSchema.ViewModels;

namespace MultiTenant_net_core_MultiSchema.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TenantController : Controller
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IMapper _mapper;
        public TenantController(ITenantRepository tenantRepository,IMapper mapper)
        {
            _mapper = mapper;
            _tenantRepository = tenantRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var results = _tenantRepository.getAllTenants();

            return Ok(_mapper.Map<IEnumerable<Tenant>, IEnumerable<TenantViewModel>>(results));
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var get_tenant = _tenantRepository.getTenantById(id);
                if (get_tenant != null)
                {
                    return Ok(_mapper.Map<Tenant, TenantViewModel>(get_tenant));
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]TenantViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newTenant = _mapper.Map<TenantViewModel, Tenant>(model);
                    if (_tenantRepository.addEntity(newTenant))
                    {
                        return Created($"api/Tenant/{newTenant.Id}", _mapper.Map<Tenant, TenantViewModel>(newTenant));
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
                
            }
            catch(Exception ex)
            {
                return BadRequest($"There is a issue : {ex}");
            }
        }
    }
}