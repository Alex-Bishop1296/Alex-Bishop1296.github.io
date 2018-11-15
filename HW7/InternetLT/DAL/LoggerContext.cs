using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using InternetLT.Models;

namespace InternetLT.DAL
{
    public class LoggerContext : DbContext
    {
        public LoggerContext() : base("name=Logger")
        {

        }

        public virtual DbSet<Log> Log { get; set; }
    }
}