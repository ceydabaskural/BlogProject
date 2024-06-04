using BlogProject.Models;
using BlogProject.Models.Entitites;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BlogProject.Controllers
{
    public class EditorsController : Controller
    {
        SqlConnection connection = new SqlConnection();
        public EditorsController(IConfiguration configuration)
        {
            connection.ConnectionString = configuration.GetConnectionString("BlogContext");
        }
        public IActionResult Index()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("select e.*, c.CategoryName as CategoryName, a.AuthorName as AuthorName from dbo.Editors as e inner join dbo.Categories as c on c.CategoryId=e.CategoryId inner join dbo.Authors as a on a.AuthorId=e.AuthorId order by e.TextTitle", connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            List<EditorNamesModel> list = new List<EditorNamesModel>();

            foreach (DataRow row in dt.Rows)
            {
                EditorNamesModel text1 = new EditorNamesModel();
                text1.TextId = Convert.ToInt32(row["TextId"]);
                text1.TextTitle = row["TextTitle"].ToString();
                text1.CategoryName = row["CategoryName"].ToString();
                text1.AuthorName = row["AuthorName"].ToString();
                text1.AuthorId = Convert.ToInt32(row["AuthorId"]);
                text1.CategoryId = Convert.ToInt32(row["CategoryId"]);
                text1.Summary = row["Summary"].ToString();
                text1.Content = row["Content"].ToString();
                text1.CreationDate = Convert.ToDateTime(row["CreationDate"]);

                list.Add(text1);
            }
            return View(list);

        }

        public IActionResult Create()
        {
            EditorCreateModel createModel = new EditorCreateModel
            {
                Editor = new Editor(),
                Categories = GetCategories(),
                Authors = GetAuthors()
            };

            return View(createModel);
        }


        [HttpPost]
        public IActionResult Create(EditorCreateModel model)
        {
            if (ModelState.IsValid)
            {
                SqlCommand command = new SqlCommand("insert into dbo.Editors values (@TextTitle,@Summary, @Content , @CreationDate , @CategoryId,@AuthorId )", connection);
                command.Parameters.AddWithValue("TextTitle", model.Editor.TextTitle);
                command.Parameters.AddWithValue("AuthorId", model.Editor.AuthorId);
                command.Parameters.AddWithValue("CategoryId", model.Editor.CategoryId);
                command.Parameters.AddWithValue("Summary", model.Editor.Summary);
                command.Parameters.AddWithValue("Content", model.Editor.Content);
                command.Parameters.AddWithValue("CreationDate", model.Editor.CreationDate);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                return RedirectToAction(nameof(Index));

            }
            else
            {
                //ViewBag.KategoriListesi = GetCategories();
                EditorCreateModel createModel = new EditorCreateModel
                {
                    Editor = model.Editor,
                    Categories = GetCategories(),
                    Authors = GetAuthors()
                };
                return View(createModel);
            }
        }


        public List<Category> GetCategories()
        {
            List<Category> list = new List<Category>();

            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Categories", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);


            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Category
                {
                    CategoryId = Convert.ToInt32(row["CategoryId"]),
                    CategoryName = row["CategoryName"].ToString()
                });
            }

            return list;
        }
        
        public List<Author> GetAuthors()
        {
            List<Author> list = new List<Author>();

            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Authors", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);


            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Author
                {
                    AuthorId = Convert.ToInt32(row["AuthorId"]),
                    AuthorName = row["AuthorName"].ToString()
                });
            }

            return list;
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


        public IActionResult Edit(int id)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Editors where TextId=@TextId", connection);
            da.SelectCommand.Parameters.AddWithValue("TextId", id);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
                return RedirectToAction(nameof(Index));
            else
            {
                Category category = new Category
                {
                    CategoryId = Convert.ToInt32(dt.Rows[0]["TextId"]),
                    CategoryName = dt.Rows[0]["TextName"].ToString()
                };

                return View(category);
            }
        }


        [HttpPost]
        public IActionResult Edit(Editor model)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Editors where TextId=@TextId", connection);
            da.SelectCommand.Parameters.AddWithValue("TextId", model.CategoryId);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                return View(model);
            }
            else
            {
                SqlCommand cmd = new SqlCommand("update dbo.Editors set TextTitle=@TextTitle where TextId=@TextId", connection);
                cmd.Parameters.AddWithValue("TextId", model.TextId);
                cmd.Parameters.AddWithValue("TextTitle", model.TextTitle);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                return RedirectToAction(nameof(Index));
            }
        }


        public IActionResult Delete(int id)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Editors where TextId=@TextId", connection);
            da.SelectCommand.Parameters.AddWithValue("TextId", id);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
                return RedirectToAction(nameof(Index));
            else
            {
                Category category = new Category
                {
                    CategoryId = Convert.ToInt32(dt.Rows[0]["TextId"]),
                    CategoryName = dt.Rows[0]["TextName"].ToString()
                };

                return View(category);
            }
        }


        [HttpPost]
        public IActionResult Delete(Editor model)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Editors where TextId=@TextId", connection);
            da.SelectCommand.Parameters.AddWithValue("TextId", model.TextId);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                return View(model);
            }
            else
            {
                SqlCommand cmd = new SqlCommand("delete from dbo.Editors where TextId=@TextId", connection);
                cmd.Parameters.AddWithValue("TextId", model.TextId);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                return RedirectToAction(nameof(Index));
            }
        }




    }
}
