using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DISANVANHOA.Filters
{
    public class RequireLoginPopupAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new ContentResult
                {
                    Content = @"
<style>
    body{overflow:hidden}
    .login-overlay{
        position:fixed;inset:0;
        background:rgba(0,0,0,.45);
        backdrop-filter:blur(3px);
        display:flex;
        align-items:center;
        justify-content:center;
        z-index:99999
    }
    .login-popup{
        background:#fff;
        width:400px;
        padding:26px 24px;
        border-radius:14px;
        text-align:center;
        box-shadow:0 15px 40px rgba(0,0,0,.2);
        animation:zoom .35s ease;
        font-family:'Segoe UI',Arial
    }
    .login-popup h3{font-size:20px;font-weight:600;margin-bottom:8px}
    .login-popup p{font-size:14px;color:#555;line-height:1.6;margin-bottom:18px}
    .popup-actions{display:flex;justify-content:center;gap:12px}
    .btn-login{
        background:linear-gradient(135deg,#17a2b8,#1fc8db);
        color:#fff;padding:9px 18px;border-radius:8px;
        text-decoration:none;transition:.25s
    }
    .btn-login:hover{transform:translateY(-1px);box-shadow:0 6px 14px rgba(23,162,184,.4)}
    .btn-cancel{
        background:#f1f1f1;border:none;padding:9px 18px;
        border-radius:8px;cursor:pointer
    }
    @keyframes zoom{
        from{opacity:0;transform:scale(.85)}
        to{opacity:1;transform:scale(1)}
    }
</style>

<div class='login-overlay'>
    <div class='login-popup'>
        <h3>🔐 Yêu cầu đăng nhập</h3>
        <p>Bạn cần đăng nhập để tiếp tục sử dụng chức năng này.</p>
        <div class='popup-actions'>
            <a href='/Account/Login' class='btn-login'>Đăng nhập</a>
            <button class='btn-cancel' onclick='window.history.back()'>Quay lại</button>
        </div>
    </div>
</div>"
                };
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
