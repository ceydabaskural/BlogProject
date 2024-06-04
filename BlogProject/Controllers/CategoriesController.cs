using BlogProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BlogProject.Controllers
{
    public class CategoriesController : Controller
    {
        SqlConnection connection = new SqlConnection();
        public CategoriesController(IConfiguration configuration)
        {
            connection.ConnectionString = configuration.GetConnectionString("BlogContext");
        }

        public IActionResult Index()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Categories", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<Category> list = new List<Category>();

            foreach (DataRow row in dt.Rows)
            {
                Category category = new Category
                {
                    CategoryId = Convert.ToInt32(row["CategoryId"]),
                    CategoryName = row["CategoryName"].ToString()
                };

                list.Add(category);
            }

            return View(list);
        }



        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                SqlCommand cmd = new SqlCommand("insert into dbo.Categories values (@CategoryName)", connection);
                cmd.Parameters.AddWithValue("CategoryName", model.CategoryName);

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
            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Categories where CategoryId=@CategoryId", connection);
            da.SelectCommand.Parameters.AddWithValue("CategoryId", id);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
                return RedirectToAction(nameof(Index));
            else
            {
                Category category = new Category
                {
                    CategoryId = Convert.ToInt32(dt.Rows[0]["CategoryId"]),
                    CategoryName = dt.Rows[0]["CategoryName"].ToString()
                };

                return View(category);
            }
        }


        [HttpPost]
        public IActionResult Delete(Category model)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Categories where CategoryId=@CategoryId", connection);
            da.SelectCommand.Parameters.AddWithValue("CategoryId", model.CategoryId);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                return View(model);
            }
            else
            {
                SqlCommand cmd = new SqlCommand("delete from dbo.Categories where CategoryId=@CategoryId", connection);
                cmd.Parameters.AddWithValue("CategoryId", model.CategoryId);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                return RedirectToAction(nameof(Index));
            }
        }



        public IActionResult Edit(int id)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Categories where CategoryId=@CategoryId", connection);
            da.SelectCommand.Parameters.AddWithValue("CategoryId", id);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
                return RedirectToAction(nameof(Index));
            else
            {
                Category category = new Category
                {
                    CategoryId = Convert.ToInt32(dt.Rows[0]["CategoryId"]),
                    CategoryName = dt.Rows[0]["CategoryName"].ToString()
                };

                return View(category);
            }
        }


        [HttpPost]
        public IActionResult Edit(Category model)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Categories where CategoryId=@CategoryId", connection);
            da.SelectCommand.Parameters.AddWithValue("CategoryId",model.CategoryId);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                return View(model);
            }
            else
            {
                SqlCommand cmd = new SqlCommand("update dbo.Categories set CategoryName=@CategoryName where CategoryId=@CategoryId", connection);
                cmd.Parameters.AddWithValue("CategoryId", model.CategoryId);
                cmd.Parameters.AddWithValue("CategoryName", model.CategoryName);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
