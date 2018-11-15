namespace InternetLT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Log
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [StringLength(100)]
        public string Word { get; set; }

        [Required]
        [StringLength(100)]
        public string GiphyURL { get; set; }

        [Required]
        [StringLength(16)]
        public string IP { get; set; }

        [Column("Browser-Agent")]
        [Required]
        [StringLength(255)]
        public string Browser_Agent { get; set; }
    }
}
