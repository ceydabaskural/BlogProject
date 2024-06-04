using BlogProject.Models.Entitites;

namespace BlogProject.Models
{
    public class HomeIndexModel
    {
        public Editor Editor { get; set; }
        public Category Category { get; set; }
        public Author Author  { get; set; }
        public List<Editor> Editors { get; set; }
    }
}
