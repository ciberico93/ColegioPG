using AppColegioPG.Context;
using AppColegioPG.Models;
using AppColegioPG.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace AppColegioPG.Services
{
    public class CursosServices : IMetodos<Cursos>
    {
        private readonly DbSet<Cursos> _dbSet;
        private readonly ColegioPGDbContext _context;

        public CursosServices(ColegioPGDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Cursos>();
        }

        public async Task<IEnumerable<Cursos>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
                          
        public async Task<Cursos> GetById(int id)
        {
            return await _dbSet.FirstAsync(x => x.Id_cursos == id);
        }


    }
}
