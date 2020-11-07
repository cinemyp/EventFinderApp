namespace WpfApp1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Session")]
    public partial class Session
    {
        public int ID { get; set; }

        public int EventsOrganizersId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime Date { get; set; }

        public virtual EventsOrganizers EventsOrganizers { get; set; }
    }
}
