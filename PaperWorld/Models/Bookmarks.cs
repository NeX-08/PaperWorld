public class Bookmarks
{
    public int Id { get; set; }

    public string MemberId { get; set; }
    public Members Member { get; set; }

    public int BookId { get; set; }
    public Books Book { get; set; }
}
