using AutoMapper;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Tawsel.Data;
using Tawsel.Interfaces;

namespace Tawsel.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Add(T record) 
        {
            await _context.AddAsync(record);

            return "Created";
        }

        public async Task<string> Delete(T record)
        {
            _context.Remove(record);
            return "Deleted";
            
        }

        public string Edit(T newRecord)
        {
            _context.Update(newRecord);
            return "Updated";
        }

        public async Task<List<T>> GetAll() 
        {
            var table = await _context.Set<T>().AsNoTracking().ToListAsync();
            return table;
        }

        public async Task<bool> Save()
        {
            var res = await _context.SaveChangesAsync();
            return res > 0 ? true : false;  
        }

    }

}

