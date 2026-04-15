using AuthService.Application.DTOs;
using AuthService.Application.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthService.Infrastructure.Data;
using AuthService.Domain.Entities;

namespace AuthService.Infrastructure.Services
{
    public class TeamServices : ITeamService
    {
        private readonly AuthDBContext _context;

        public TeamServices(AuthDBContext context)
        {
            _context = context;
        }

        public Task<TeamResponse> Create(TeamRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TeamResponse>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TeamResponse> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TeamResponse> Update(int id, TeamRequest request)
        {
            throw new NotImplementedException();
        }
    }

}
