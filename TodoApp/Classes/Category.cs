using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoApp.Classes
{
    public class Category
    {
      

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(20, ErrorMessage = "Category name cannot exceed 20 characters.")]
        public string Name {  get; set; }

        public string CategoryDescription { get; set; }= string.Empty;
        public bool Priority { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<TodoItem> Todos { get; set; } // many todos

    }
}
