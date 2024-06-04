using BlogProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace BlogProject.Controllers
{
    public class AuthorsController : Controller
    {
        SqlConnection connection = new SqlConnection();
        public AuthorsController(IConfiguration configuration)
        {
            connection.ConnectionString = configuration.GetConnectionString("BlogContext");
        }
        public IActionResult Index()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Authors",connection);
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<Author> list = new List<Author>();

            foreach (DataRow row in dt.Rows)
            {
                Author author = new Author
                {
                    AuthorId = Convert.ToInt32(row["AuthorId"]),
                    AuthorName = row["AuthorName"].ToString()
                };

                list.Add(author);
            }

            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Author model)
        {
            if (ModelState.IsValid)
            {
                SqlCommand cmd = new SqlCommand("insert into dbo.Authors values (@AuthorName)", connection);
                cmd.Parameters.AddWithValue("AuthorName", model.AuthorName);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                return RedirectToAction(nameof(Index));
            }
            else
                return View(model);
        }


        public IActionResult Delete(int id)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Authors where AuthorId=@AuthorId", connection);
            da.SelectCommand.Parameters.AddWithValue("AuthorId", id);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
                return RedirectToAction(nameof(Index));
            else
            {
                Author author = new Author
                {
                    AuthorId = Convert.ToInt32(dt.Rows[0]["AuthorId"]),
                    AuthorName = dt.Rows[0]["AuthorName"].ToString()

                };
                    return View(author);
            }
        }


        [HttpPost]
        public IActionResult Delete (Author model)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Authors where AuthorId=@AuthorId",connection);
            da.SelectCommand.Parameters.AddWithValue("AuthorId", model.AuthorId);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if(dt.Rows.Count == 0)
            {
                return View(model);
            }
            else
            {
                SqlCommand cmd = new SqlCommand("delete from dbo.Authors where AuthorId=@AuthorId", connection);
                cmd.Parameters.AddWithValue("AuthorId", model.AuthorId);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                return RedirectToAction(nameof(Index));
            }
        }



        public IActionResult Edit(int id)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Authors where AuthorId=@AuthorId", connection);
            da.SelectCommand.Parameters.AddWithValue("AuthorId", id);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
                return RedirectToAction(nameof(Index));
            else
            {
                Author author = new Author
                {
                    AuthorId = Convert.ToInt32(dt.Rows[0]["AuthorId"]),
                    AuthorName = dt.Rows[0]["AuthorName"].ToString()

                };
                return View(author);
            }
        }


        [HttpPost]
        public IActionResult Edit(Author model)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Authors where AuthorId=@AuthorId", connection);
            da.SelectCommand.Parameters.AddWithValue("AuthorId", model.AuthorId);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                return View(model);
            }
            else
            {
                SqlCommand cmd = new SqlCommand("update dbo.Authors set AuthorName=@AuthorName where AuthorId=@AuthorId", connection);
                cmd.Parameters.AddWithValue("AuthorId", model.AuthorId);
                cmd.Parameters.AddWithValue("AuthorName", model.AuthorName);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
