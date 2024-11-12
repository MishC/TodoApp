using Microsoft.AspNetCore.Components.RenderTree;
using System.Threading;
namespace TodoApp.Classes
{
    public class TodoItem
    {
        private static int _nextId = 1;
        public int Id { get; set; }
        private string? _title;
        public string Title
        {
            get => _title ?? throw new NullReferenceException();
            set => _title = value?.Trim() ?? throw new ArgumentNullException(nameof(value));

        }

        public bool isCompleted { get; set; } = false;
        public DateTime CurrentDate { get; private set; } = DateTime.Now;
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