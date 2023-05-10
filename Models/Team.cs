using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthSystem.Models;

public class Team
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "The Team Name field is required.")]
    [MaxLength(50, ErrorMessage = "The maximum length of Team Name field is 50 characters.")]
    public string Name { get; set; }

    [ForeignKey("League")]
    [Required(ErrorMessage = "The League field is required.")]
    public int LeagueId { get; set; }
    public virtual League League { get; set; }

    [Required(ErrorMessage = "The Stadium field is required.")]
    [MaxLength(50, ErrorMessage = "The maximum length of Stadium field is 50 characters.")]
    public string Stadium { get; set; }

    [Required(ErrorMessage = "The City field is required.")]
    [MaxLength(50, ErrorMessage = "The maximum length of City field is 50 characters.")]
    public string City { get; set; }

    public string Country { get; set; }

    public Team()
    {
    }

    public Team(League League)
    {
        this.League = League;
        this.Country = League.Country;
    }
}
