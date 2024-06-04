using BlogProject.Models;
using BlogProject.Models.Entitites;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace BlogProject.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection connection = new SqlConnection();
        public HomeController(IConfiguration configuration)
        {
            connection.ConnectionString = configuration.GetConnectionString("BlogContext");
        }

        public IActionResult Index(int id)
        {
            //ViewBag.Categories=GetCategories();
            //ViewBag.Authors=GetAuthors();


            HomeIndexModel model = new HomeIndexModel();
            model.Author = GetAuthor(model.Author.AuthorId);
            model.Editor = GetText(id);
            model.Editors = GetTexts();
            model.Category = GetCategory(model.Category.CategoryId);
            return View(model);
        }


        public Editor GetText(int id)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("select * from dbo.Editors where TextId=@TextId", connection);
            adapter.SelectCommand.Parameters.AddWithValue("TextId", id);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            Editor text = new Editor();
            {
                Editor text1 = new Editor();
                text1.TextId = Convert.ToInt32(dt.Rows[0]["TextId"]);
                text1.TextTitle = dt.Rows[0]["TextTitle"].ToString();
                text1.AuthorId = Convert.ToInt32(dt.Rows[0]["AuthorId"]);
                text1.CategoryId = Convert.ToInt32(dt.Rows[0]["CategoryId"]);
                text1.Summary = dt.Rows[0]["Summary"].ToString();
                text1.Content = dt.Rows[0]["Content"].ToString();
                text1.CreationDate = Convert.ToDateTime(dt.Rows[0]["CreationDate"]);

            }

            return text;
        }

        private Category GetCategory(int id)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Categories where CategoryId=@CategoryId", connection);
            da.SelectCommand.Parameters.AddWithValue("CategoryId", id);
            DataTable dt = new DataTable();
            da.Fill(dt);

            Category category = new Category
            {
                CategoryId = Convert.ToInt32(dt.Rows[0]["CategoryId"]),
                CategoryName = dt.Rows[0]["CategoryName"].ToString()
            };


            return category;
        }

        public Author GetAuthor(int id)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Authors where AuthorId=@AuthorId", connection);
            da.SelectCommand.Parameters.AddWithValue("AuthorId", id);
            DataTable dt = new DataTable();
            da.Fill(dt);

            Author author = new Author
            {
                AuthorId = Convert.ToInt32(dt.Rows[0]["AuthorId"]),
                AuthorName = dt.Rows[0]["AuthorName"].ToString()
            };

            return author;

        }


        public List<Editor> GetTexts()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Editors", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<Editor> editors = new List<Editor>();

            foreach (DataRow row in dt.Rows)
            {
                Editor editor = new Editor
                {
                    TextId = Convert.ToInt32(row["TextId"]),
                    TextTitle = Convert.ToString(row["TextTitle"]),
                    CategoryId = Convert.ToInt32(row["CategoryId"]),
                    Content = Convert.ToString(row["Content"]),
                    CreationDate = Convert.ToDateTime(row["CreationDate"]),
                    Summary = Convert.ToString(row["Summary"]),
                };

                editors.Add(editor);
            }

            return editors;


            //}


            //private List<Category> GetCategories()
            //{
            //    SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Categories", connection);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);

            //    List<Category> categories = new List<Category>();

            //    foreach (DataRow row in dt.Rows)
            //    {
            //        categories.Add(new Category
            //        {
            //            CategoryId = Convert.ToInt32(row["CategoryId"]),
            //            CategoryName = row["CategoryName"].ToString()
            //        });
            //    }

            //    return categories;
            //} 

            //public List<Author> GetAuthors()
            //{
            //    SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Authors", connection);
            //    DataTable dt = new DataTable();
            //    da.Fill(dt);

            //    List<Author> authors = new List<Author>();

            //    foreach (DataRow row in dt.Rows)
            //    {
            //        authors.Add(new Author
            //        {
            //            AuthorId = Convert.ToInt32(row["AuthorId"]),
            //            AuthorName = row["AuthorName"].ToString()
            //        });
            //    }

            //    return authors;
            //}
        }
    }
}
