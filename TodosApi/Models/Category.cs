using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodosApi.Models
{
    public class Category
    {
      

        [Key]
        public int Id { get; set; }

        //[Required(ErrorMessage = "Category name is required.")]
        [StringLength(30, ErrorMessage = "Category name cannot exceed 20 characters.")]
        public string Name {  get; set; }

        public string CategoryDescription { get; set; }= string.Empty;
        public bool Priority { get; set; } 
        [JsonIgnore]
        public ICollection<TodoItem> Todos { get; set; } // many todos

    }
}
