using System.ComponentModel;

namespace BlogProject.Models
{
    public class Author
    {
        [DisplayName("Yazar Adı")]
        public int AuthorId { get; set; }

        [DisplayName("Yazar Adı")]
        public string AuthorName { get; set; }
    }
}
