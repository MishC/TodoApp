public class TodoItem
{
    private static int _nextId = 1;
    public int Id { get; private set; }
    public string Title { get; set; } = string.Empty;
    public bool Completed { get; set; } = false;
    public DateTime CurrentDate { get; private set; } = DateTime.Now;
    public DateTime? TimeCompleted { get; set; }

    public TodoItem()
    {
        Id = _nextId++;
        CurrentDate = DateTime.Now;
    }
    public string TimeDifference
    {
        get
        {
            if (TimeCompleted != null)
            {
                TimeSpan timeSpan = 
                    TimeCompleted.Value - CurrentDate; 
                return $"{timeSpan.Days} days {timeSpan.Hours} hours";
            }
            else
            {
                return "Not completed yet";
            }
        }
    }
}