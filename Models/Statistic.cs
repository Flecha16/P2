﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthSystem.Models
{
    public class Statistic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Team")]
        [Required(ErrorMessage = "The Team field is required.")]
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }

        [ForeignKey("Player")]
        [Required(ErrorMessage = "The Player field is required.")]
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }

        [Required(ErrorMessage = "The Matches field is required.")]
        public int Matches { get; set; }

        [Required(ErrorMessage = "The Minutes field is required.")]
        public double Minutes { get; set; }

        [Required(ErrorMessage = "The Goals field is required.")]
        public int Goals { get; set; }

        [Required(ErrorMessage = "The Assists field is required.")]
        public int Assists { get; set; }

        [Required(ErrorMessage = "The Yellow Cards field is required.")]
        public int YellowCards { get; set; }

        [Required(ErrorMessage = "The Red Cards field is required.")]
        public int RedCards { get; set; }

        [Required(ErrorMessage = "The Average Speed field is required.")]
        public double AverageSpeed { get; set; }

        [Required(ErrorMessage = "The Average KM field is required.")]
        public double Km { get; set; }
    }

}