using System;
using System.Collections.Generic;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using ModuloAPI.Context;
using ModuloAPI.DTOs;
using ModuloAPI.Entities;

namespace ModuloAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AgendaContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AgendaContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Nome == dto.Nome && u.Senha == dto.Senha);
            if (usuario == null)
            {
                return Unauthorized("Credenciais inválidas");
            }

            //Claims são informações que vão dentro do token.
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Role, usuario.Id.ToString())
            };
            //gerando a chave de segurança para assinar o token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            
            //gerando as credenciais de assinatura do token
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //gerando o token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            //gerando string do token
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { Token = tokenString });
        }

        [HttpPost("register")]
        public IActionResult Register(LoginDto dto)
        {
            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Senha = dto.Senha
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return Ok("Usuário registrado com sucesso");
        }
    }
}