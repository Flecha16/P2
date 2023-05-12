using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthSystem.Models
{
    public class Player : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "The First Name field is required.")]
        [MaxLength(50, ErrorMessage = "The maximum length of First Name field is 20 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The Last Name field is required.")]
        [MaxLength(50, ErrorMessage = "The maximum length of Last Name field is 20 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The Date Of Birth field is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [CustomValidation(typeof(Player), "ValidateDateOfBirth", ErrorMessage = "Date of Birth can't be greater than current date.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "The Nationality field is required.")]
        public Nationality Nationality { get; set; }

        [Required(ErrorMessage = "The Position field is required.")]
        public Position Position { get; set; }

        [Required(ErrorMessage = "The Valoration field is required.")]
        public int Valoration { get; set; }

        [ForeignKey("League")]
        [Required(ErrorMessage = "The League field is required.")]
        public int LeagueId { get; set; }
        public virtual League League { get; set; }

        [ForeignKey("Team")]
        [Required(ErrorMessage = "The Team field is required.")]
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }

        public static ValidationResult ValidateDateOfBirth(DateTime dateOfBirth, ValidationContext context)
        {
            if (dateOfBirth > DateTime.Now)
            {
                return new ValidationResult("Date of Birth can't be greater than current date.");
            }
            return ValidationResult.Success;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            ValidationResult dateOfBirthValidationResult = ValidateDateOfBirth(DateOfBirth, validationContext);
            if (dateOfBirthValidationResult != ValidationResult.Success)
            {
                results.Add(dateOfBirthValidationResult);
            }

            return results;
        }
    }

    public enum Nationality
    {
        Argentina,
        Brazil,
        USA,
        France,
        Italy
    }

    public enum Position
    {
        CF, LW, RW, ST,
        CDM, LM, RM, CM, CAM,
        RB, LB, CB, SW,
        GK
    }
}