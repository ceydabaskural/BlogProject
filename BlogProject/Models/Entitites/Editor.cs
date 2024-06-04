using System.ComponentModel;

namespace BlogProject.Models.Entitites
{
    public class Editor
    {
        public int TextId { get; set; }


        [DisplayName("Başlık")]
        public string TextTitle { get; set; }


        [DisplayName("Özet")]
        public string Summary { get; set; }


        [DisplayName("İçerik")]
        public string Content { get; set; }


        [DisplayName("Oluşturulma Tarihi")]
        public DateTime CreationDate { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
    }
}
