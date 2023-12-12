using FlowerShop_Web.Models.Flower_Models;
using FloweShop_Web.Models.Flower_Repository;
using OfficeOpenXml;

namespace FlowerShop_Web.Models.Flower_Services
{
    public class Import_ExportService
    {
        private readonly ApplicationDbContext _context;

        public Import_ExportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void ExportToExcel<T>(List<T> data, string[] columnHeaders, string fileName)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Data");

                // Thêm tiêu đề cột
                for (int i = 0; i < columnHeaders.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = columnHeaders[i];
                }

                // Thêm dữ liệu từ danh sách
                for (int i = 0; i < data.Count; i++)
                {
                    for (int j = 0; j < columnHeaders.Length; j++)
                    {
                        var propertyValue = typeof(T).GetProperty(columnHeaders[j]).GetValue(data[i]);
                        worksheet.Cells[i + 2, j + 1].Value = propertyValue;
                    }
                }

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = Path.Combine(desktopPath, $"{fileName}.xlsx");
                FileInfo excelFile = new FileInfo(filePath);
                package.SaveAs(excelFile);
            }
        }
        public List<T> ImportFromExcel<T>(Stream stream, string[] columnHeaders)
        {
            List<T> dataList = new List<T>();

            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    T data = ReadRowData<T>(worksheet, row, columnHeaders);
                    dataList.Add(data);
                }
            }

            return dataList;
        }

        private T ReadRowData<T>(ExcelWorksheet worksheet, int row, string[] columnHeaders)
        {
            T data = Activator.CreateInstance<T>();

            for (int j = 0; j < columnHeaders.Length; j++)
            {
                var property = typeof(T).GetProperty(columnHeaders[j]);
                if (property != null)
                {
                    var cellValue = worksheet.Cells[row, j + 1].Value;
                    if (cellValue != null)
                    {
                        property.SetValue(data, Convert.ChangeType(cellValue, property.PropertyType));
                    }
                }
            }

            return data;
        }

        public List<Recipe> GetRecipes()
        {
            return _context.Recipes.ToList();
        }

        public void AddEntity<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }
    }
}
