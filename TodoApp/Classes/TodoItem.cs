using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace TodoApp.Classes
{
    public class TodoItem
    {
        private static int _nextId = 0;

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [IsFalse(ErrorMessage = "IsCompleted must be false during initialization.")]
        public bool IsCompleted { get; set; } = false;

        public DateTime CurrentDate { get; private set; } // Initial value set in constructor
        public DateTime? TimeCompleted { get; set; }

        [MinLength(10, ErrorMessage = "Your description is too short. Your description must have at least 10 characters.")]
        [MaxLength(500, ErrorMessage = "Too long description. Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        public TodoItem()
        {
            Id = Interlocked.Increment(ref _nextId);
            CurrentDate = DateTime.Now;
        }

        public string TimeDifference => TimeCompleted != null ?
            $"{(TimeCompleted.Value - DateTime.Now).Duration().Days} days {(TimeCompleted.Value - DateTime.Now).Duration().Hours} hours"
            : "Not completed yet";
    }

    // Custom validation attribute
    public class IsFalseAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is bool boolValue && boolValue)
            {
                return new ValidationResult(ErrorMessage ?? "The value must be false.");
            }

            return ValidationResult.Success;
        }
    }
}
