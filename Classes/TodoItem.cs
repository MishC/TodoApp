public class TodoItem
{
    private static int _nextId = 1;
    public int Id { get; private set; }
    public string Title { get; set; } = string.Empty;
    public bool Completed { get; set; } = false;

    public TodoItem()
    {
        Id = _nextId++;
    }
}