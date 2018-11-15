using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using InternetLT.Models;

namespace InternetLT.DAL
{
    /// <summary>
    /// Basic class to contain the context of the logger
    /// </summary>
    public class LoggerContext : DbContext
    {
        public LoggerContext() : base("name=Logger")
        {

        }

        public virtual DbSet<Log> Log { get; set; }
    }
}