using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BLL
{
    public class ShopCartAccess
    {

        public static string getCartID
        {
            get {

                HttpContext hc = HttpContext.Current;
                
                string cartId = null;
       
                //先尝试从session中获取cartid
                object tempCartId = hc.Session["cartId_shopping_" + hc.User.Identity.Name];

                if (tempCartId != null)
                {
                    //如果存在，则将它提供给cartId，再进行判断
                    cartId = tempCartId.ToString();
                }


                if (!string.IsNullOrEmpty(cartId))
                {
                    //如果cartId不为空，说明已成功从session中获得cartID，则返回
                    return cartId;
                }
                else
                {
                    
                        //如果从session中获取不到，则从发送请求者的cookies中尝试获取cartId,不为空的情况
                        if (hc.Request.Cookies["cartId_shopping_" + hc.User.Identity.Name] != null)
                        {
                            //该cookies不为空，则取出里面的值，也就是cartID;
                            cartId = hc.Request.Cookies["cartId_shopping_" + hc.User.Identity.Name].Value;

                            //将cookies中取出的cartId放入该用户的session中，并返回
                            hc.Session["cartId_shopping_" + hc.User.Identity.Name] = cartId;
                            return cartId;
                        }
                        else
                        {
                            //如果从cookies中也取不到cartId,说明用户第一次访问,则创建一个cartID
                            cartId = Guid.NewGuid().ToString();
                            //为用户创建cookies
                            HttpCookie ck = new HttpCookie("cartId_shopping_" + hc.User.Identity.Name, cartId);
                            //持久化Cookies
                            ck.Expires = DateTime.Now.AddDays(30);
                            //为用户计算机添加该cookies
                            hc.Response.Cookies.Add(ck);
                            //再将创建好的cookies放入session中使用
                            hc.Session["cartId_shopping_" + hc.User.Identity.Name] = cartId;
                            //返回该cartId
                            return cartId;
                        }
                    

                }
            }
        }
        
    }
}
