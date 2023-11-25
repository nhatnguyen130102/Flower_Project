﻿using Flower_Models;
using Flower_Repository;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Flower_Services
{
    public class RecipeService
    {
        private readonly ApplicationDbContext _context;

        public RecipeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void ExportToExcel(List<Recipe> data, string filePath)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Tiêu đề cột
                worksheet.Cells[1, 1].Value = "ID_Recipe";
                worksheet.Cells[1, 2].Value = "ID_Product";
                worksheet.Cells[1, 3].Value = "ID_Material";
                worksheet.Cells[1, 4].Value = "Material_Quantity";
                // Thêm dữ liệu từ danh sách
                for (int i = 0; i < data.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = data[i].ID_Recipe;
                    worksheet.Cells[i + 2, 2].Value = data[i].ID_Product;
                    worksheet.Cells[i + 2, 3].Value = data[i].ID_Material;
                    worksheet.Cells[i + 2, 4].Value = data[i].Material_Quantity;
                }

                //// Lưu file Excel
                //FileInfo excelFile = new FileInfo(filePath);
                //package.SaveAs(excelFile);

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                filePath = Path.Combine(desktopPath, "Sheet1.xlsx");
                FileInfo excelFile = new FileInfo(filePath);
                package.SaveAs(excelFile);
            }
        }

        public List<Recipe> ImportFromExcel(string filePath)
        {
            List<Recipe> data = new List<Recipe>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];

                // Bắt đầu đọc từ dòng thứ 2 (vì dòng đầu là tiêu đề cột)
                int row = 2;
                while (worksheet.Cells[row, 1].Value != null)
                {
                    Recipe item = new Recipe
                    {
                        //ID_Recipe = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                        ID_Product = Convert.ToInt32(worksheet.Cells[row, 2].Value),
                        ID_Material = Convert.ToInt32(worksheet.Cells[row, 3].Value),
                        Material_Quantity = Convert.ToInt32(worksheet.Cells[row, 4].Value)
                    };

                    data.Add(item);
                    row++;
                }
            }

            return data;
        }

        public List<Recipe> GetRecipes()
        {
            return _context.Recipes.ToList();
        }

        public void AddRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            _context.SaveChanges();
        }
    }
}
