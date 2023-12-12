using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop_Web.Models.Flower_Reponsitory
{
    public class AccessCounterMiddleware
    {
        private readonly RequestDelegate _next;

        public AccessCounterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Kiểm tra xem URL yêu cầu là trang "Index" của controller cụ thể
            if (IsHomePage(context.Request.Path))
            {
                // Lấy giá trị số lượng truy cập từ cookie (nếu đã tồn tại)
                var accessCount = context.Request.Cookies["AccessCount"];

                if (string.IsNullOrEmpty(accessCount))
                {
                    accessCount = "0";
                }

                // Chuyển đổi giá trị thành số nguyên
                var accessCountValue = int.Parse(accessCount);

                // Tăng giá trị số lượng truy cập
                accessCountValue++;

                // Lưu giá trị mới vào cookie
                context.Response.Cookies.Append("AccessCount", accessCountValue.ToString());
            }

            await _next(context);
        }
        private bool IsHomePage(string path)
        {
            return path == "/" || path == "/Home/Index";
        }
    }
}
