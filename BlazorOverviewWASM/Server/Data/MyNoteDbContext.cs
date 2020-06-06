using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorOverviewWASM.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorOverviewWASM.Server.Data
{
    public class MyNoteDbContext:DbContext
    {
        public MyNoteDbContext(DbContextOptions<MyNoteDbContext> options): base(options)
        {
        }
        public DbSet<MyNote> MyNotes { get; set; }
    }
}
