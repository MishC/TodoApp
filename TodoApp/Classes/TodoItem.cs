﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text.Json.Serialization;



namespace TodoApp.Classes
{
    public class TodoItem
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        public bool IsCompleted { get; set; } = false;

        public DateTime CurrentDate { get; set; }
        public DateTime? TimeCompleted { get; set; }

        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must have between 10 to 500 characters.")]
        public string? Description { get; set; }

        [Display(Name = "Due Date", Prompt = "YYYY-MM-DD")] // Hint for user
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
      
        public DateTime? DueDate { get; set; }
        
        public bool Priority { get; set; }

        public TodoItem()
        {
        }

        public string TimeDifference => TimeCompleted != null ?
            $"{(TimeCompleted.Value - DateTime.Now).Duration().Days} days {(TimeCompleted.Value - DateTime.Now).Duration().Hours} hours"
            : "Not completed yet";


        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

    } //end of class

    
    
}
