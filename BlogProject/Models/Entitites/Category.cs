using System.ComponentModel;

namespace BlogProject.Models
{
    public class Category
    {
        [DisplayName("Kategori Adı")]
        public int CategoryId { get; set; }

        [DisplayName("Kategori Adı")]
        public string CategoryName { get; set; }
    }
}
