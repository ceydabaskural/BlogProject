using BlogProject.Models.Entitites;
using System.ComponentModel;

namespace BlogProject.Models
{
    public class EditorNamesModel : Editor
    {
        [DisplayName("Yazar")]
        public string AuthorName { get; set; }


        [DisplayName("Kategori")]
        public string CategoryName { get; set; }
    }
}
