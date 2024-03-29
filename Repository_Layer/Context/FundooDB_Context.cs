﻿using Microsoft.EntityFrameworkCore;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Context
{
    public class FundooDB_Context:DbContext
    {
        public FundooDB_Context(DbContextOptions options) : base(options)
        { }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<NoteEntity> Notes { get; set; }
        public DbSet<LabelEntity> Labels { get; set; }
        public DbSet<CollaboratorEntity> Collaborators { get; set; }

    }
}
