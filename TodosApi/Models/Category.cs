using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TodosApi.Models
{
    public class Category
    {
      

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(30, ErrorMessage = "Category name cannot exceed 20 characters.")]
        public required string Name { get; set; }
       
        public string CategoryDescription { get; set; }= string.Empty;

       
        // many todos
        // Navigation property: A Category can have many Todos
        public ICollection<TodoItem> Todos { get; set; }

    }
}
