using Microsoft.AspNetCore.Components.RenderTree;
using System.Threading;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Classes

{
    public class TodoItem
    {
        private static int _nextId = 0;
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public bool IsCompleted { get; set; } = false; //default value
        public DateTime CurrentDate { get; private set; } = DateTime.Now; //default value
        public DateTime? TimeCompleted { get; set; }
        public string? Description { get; set; }


        public TodoItem()
        {
            Id = Interlocked.Increment(ref _nextId);

            CurrentDate = DateTime.Now;
        }
        //expression-bodied syntax for get
        public string TimeDifference => TimeCompleted != null ?
            $"{(TimeCompleted.Value - CurrentDate).Duration().Days} days {(TimeCompleted.Value - CurrentDate).Duration().Hours} hours"
         : "Not completed yet";

    }
}