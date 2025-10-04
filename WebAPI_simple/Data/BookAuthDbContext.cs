﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAPI_simple.Data
{
    public class BookAuthDbContext : IdentityDbContext
    {
        public BookAuthDbContext(DbContextOptions<BookAuthDbContext> options)
            : base(options)
        {
        }
    }
}
