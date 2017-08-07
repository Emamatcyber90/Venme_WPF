namespace VenmeWPF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Transaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int amount { get; set; }

        public int toUserId { get; set; }

        public int FromUserId { get; set; }

        public virtual User User { get; set; }
    }
}
