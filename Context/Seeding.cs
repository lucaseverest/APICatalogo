using APICatalogo.DTOs;
using APICatalogo.Models;
using Microsoft.AspNetCore.Identity;

namespace APICatalogo.Context
{
    public class Seeding
    {
        private AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Seeding(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (_context.Categorias.Any() || _context.Produtos.Any()) return;

            Categoria c1 = new Categoria(1, "Carros", "Carros irados");
            Categoria c2 = new Categoria(2, "Bikes", "Bikes brabas");
            Categoria c3 = new Categoria(3, "Imoveis", "Varias Casas");

            Produto p1 = new Produto(1, "Ford", "carrão", 10, "carrao.png", 2, new DateTime(), 1);
            Produto p2 = new Produto(2, "Ford", "carrão", 10, "carrao.png", 2, new DateTime(), 1);
            Produto p3 = new Produto(3, "Ford", "carrão", 10, "carrao.png", 2, new DateTime(), 1);

            _context.Categorias.AddRange(c1, c2, c3);
            _context.Produtos.AddRange(p1, p2, p3);
            _context.SaveChanges();

            _ = await _roleManager.CreateAsync(new IdentityRole("Admin"));
            _ = await _roleManager.CreateAsync(new IdentityRole("User"));


            ApplicationUser user = new()
            {
                Email = "admin@admin.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "admin"
            };

            var admin = await _userManager.CreateAsync(user, "admin");

            if (admin != null)
            {
                _ = await _userManager.AddToRoleAsync(user, "Admin");
                return;
            }

        }
    }
}
