namespace BlogV1.Models
{
    public class Comment
    {
        public int id { get; set; }
        public int  BlogId { get; set; }
        public String Name { get; set; }
        public DateTime PublishDate { get; set; }
        public  string Email { get; set; }
        public string Message { get; set; }

    }
}
