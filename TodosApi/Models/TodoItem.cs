﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;



namespace TodosApi.Models
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

    

        public string TimeDifference => TimeCompleted != null ?
            $"{(TimeCompleted.Value - DateTime.Now).Duration().Days} days {(TimeCompleted.Value - DateTime.Now).Duration().Hours} hours"
            : "Not completed yet";

        // Foreign Key: Links to the Category table

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [ValidateNever]
        [JsonIgnore]
        // Navigation property: A Todo belongs to one Category
        public Category Category { get; set; }

    } //end of class

    
    
}
