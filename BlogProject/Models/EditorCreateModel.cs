using BlogProject.Models.Entitites;

namespace BlogProject.Models
{
    public class EditorCreateModel
    {
        public Editor Editor { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Author>? Authors { get; set; }
    }
}
