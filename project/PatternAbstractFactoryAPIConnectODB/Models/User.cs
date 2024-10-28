﻿using System.ComponentModel.DataAnnotations;

namespace PatternAbstractFactoryAPIConnectODB.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
