using AuthService.Application.DTOs;
using AuthService.Application.Interface;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly AuthDBContext _context;

        public TeamController(AuthDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(Team team)
        {
            _context.Teams.Add(team);
            _context.SaveChanges();
            return Ok(team);
        }
    }
}
