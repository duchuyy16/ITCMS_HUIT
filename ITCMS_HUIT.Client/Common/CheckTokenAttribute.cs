using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using ITCMS_HUIT.Client.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using X.PagedList;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CheckTokenAttribute : ActionFilterAttribute
{
    private readonly string[] _roleArray;

    public CheckTokenAttribute(string roles)
    {
        _roleArray = roles.Split(',');
    }

    public string DecodeJwtToken(string jwtToken)
    {
        var handler = new JwtSecurityTokenHandler();

        // Chuyển đổi JWT token thành đối tượng SecurityToken
        var jsonToken = handler.ReadToken(jwtToken.Replace("\"", "")) as JwtSecurityToken;

        if (jsonToken == null)
            throw new InvalidOperationException("Invalid JWT token.");

        // Chuyển đối tượng SecurityToken thành đối tượng JSON
        var json = "{";
        foreach (var claim in jsonToken.Claims)
        {
            json += $"\"{claim.Type}\":\"{claim.Value}\",";
        }

        // Loại bỏ dấu ',' cuối cùng nếu có
        if (json.EndsWith(","))
        {
            json = json.Remove(json.Length - 1);
        }

        json += "}";


        return JsonDocument.Parse(json).RootElement.GetProperty("http://schemas.microsoft.com/ws/2008/06/identity/claims/role").GetString()!;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.Session.GetString("Token") == null)
        {
            context.Result = new RedirectToActionResult("Login", "Auth", null);
        }

        if (context.HttpContext.Session.GetString("Token") != null)
        {
            var userRole = DecodeJwtToken(context.HttpContext.Session.GetString("Token")!);

            // Kiểm tra xem quyền của người dùng có nằm trong danh sách quyền được phép không
            if (!_roleArray.Any(role => userRole.Equals(role, StringComparison.OrdinalIgnoreCase)))
            {
                var tempData = (context.Controller as Controller)?.TempData;

                tempData!["Warning"] = "Bạn không có quyền truy cập vào tài nguyên này!";

                context.Result = new RedirectToActionResult("Login", "Auth", null);
            }
        }

        base.OnActionExecuting(context);
    }
}


//[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
//public class CheckTokenAttribute : ActionFilterAttribute
//{
//    private readonly string _roleName;
//    public CheckTokenAttribute(string roleName)
//    {
//        _roleName = roleName;
//    }

//    public string DecodeJwtToken(string jwtToken)
//    {
//        var handler = new JwtSecurityTokenHandler();

//        // Chuyển đổi JWT token thành đối tượng SecurityToken
//        var jsonToken = handler.ReadToken(jwtToken.Replace("\"", "")) as JwtSecurityToken;

//        if (jsonToken == null)
//            throw new InvalidOperationException("Invalid JWT token.");      

//        // Chuyển đối tượng SecurityToken thành đối tượng JSON
//        var json = "{";
//        foreach (var claim in jsonToken.Claims)
//        {
//            json += $"\"{claim.Type}\":\"{claim.Value}\",";
//        }

//        // Loại bỏ dấu ',' cuối cùng nếu có
//        if (json.EndsWith(","))
//        {
//            json = json.Remove(json.Length - 1);
//        }

//        json += "}";

//        return JsonDocument.Parse(json).RootElement.GetProperty("http://schemas.microsoft.com/ws/2008/06/identity/claims/role").GetString()!;
//    }

//    public override void OnActionExecuting(ActionExecutingContext context)
//    {
//        if (context.HttpContext.Session.GetString("Token") == null)
//        {
//            context.Result = new RedirectToActionResult("Login", "Auth", null);
//        }

//        if(context.HttpContext.Session.GetString("Token") != null)
//        {
//            if (DecodeJwtToken(context.HttpContext.Session.GetString("Token")!) != _roleName)
//            {
//                var tempData = (context.Controller as Controller)?.TempData;

//                tempData!["Warning"] = "Bạn không có quyền truy cập vào tài nguyên này!";

//                context.Result = new RedirectToActionResult("Login", "Auth", null);
//            }
//        }     

//        base.OnActionExecuting(context);
//    }
//}
