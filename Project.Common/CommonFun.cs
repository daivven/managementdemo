using System;
using System.Web;
using System.Web.UI;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Security;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Project.Common
{
    public class CommonFun
    {

        /// <summary>
        /// 从网页编辑器中获取全部图片url
        /// </summary>
        /// <param name="str">编辑器内容</param>
        /// <returns>图片url列表</returns>
        public static List<string> GetUrlsFromEidtor(string str)
        {
            List<string> list = new List<string>();
            Regex reg = new Regex(@"(?i)<img\b[^>]*?src=(['""]?)([^'""\s>]+)\1[^>]*>");
            MatchCollection mc = reg.Matches(str);
            foreach (Match m in mc)
            {
                list.Add(m.Groups[2].Value);
            }
            return list;
        }

        /// <summary>
        /// 此方法用于确认用户输入的不是恶意信息
        /// </summary>
        /// <param name="text">用户输入信息</param>
        /// <param name="maxLength">输入的最大长度</param>
        /// <returns>处理后的输入信息</returns>
        public static string InputText(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            if (string.IsNullOrEmpty(text.Trim()))
                return string.Empty;
            if (text.Length > maxLength)
                text = text.Substring(0, maxLength);
            //将网页中非法和有攻击性的符号替换掉，以防sql注入！返回正常数据
            text = Regex.Replace(text, "[\\s]{2,}", " ");	// 2个或以上的空格
            text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");	//<br> html换行符
            text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");	//&nbsp;   html空格符
            text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);	// 任何其他的标签
            text = text.Replace("'", "''");// 单引号
            return text;
        }

        /// <summary>
        /// 用户登陆成功后，发放表单cookie验证票据并记录用户的相关信息
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="userName">与票证关联的用户名</param>
        /// <param name="Page">页面对象</param>
        /// <param name="expiration">FormsAuthenticationTicket过期时间</param> 
        /// <param name="userInfo">要保存在cookie中用户对象</param>
        public static void UserLoginSetCookie<T>(string userName, Page page, DateTime expiration, T userInfo)
        {
            //将对象序列化成字符串
            string strUser = Serialize<T>(userInfo);

            // 设置票据Ticket信息
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, expiration, false, strUser);

            string strTicket = FormsAuthentication.Encrypt(ticket);// 加密票据

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, strTicket);// 使用新userdata保存cookie
            //cookie.Expires = ticket.Expiration;//将票据的过期时间和Cookie的过期时间同步，避免因两者的不同所产生的矛盾


            page.Response.Cookies.Add(cookie);
        }


        /// <summary>
        /// 获得保存在cookie中的用户对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="page">页面对象</param>
        /// <returns>保存在cookie中的用户对象</returns>
        public static T GetCookieUserData<T>(Page page)
        {
            if (page != null)
            {
                if (page.User.Identity.IsAuthenticated)//如果没验证通过的话,强制类型转换会出错!~
                {
                    string strUser = ((FormsIdentity)page.User.Identity).Ticket.UserData;
                    return Desrialize<T>(strUser);
                }
                else
                    return default(T);
            }
            else
                throw new ApplicationException("page == null");
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="t">泛型对象</param>
        /// <returns>对象序列化后变成的字符串</returns>
        public static string Serialize<T>(T t)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                using (MemoryStream stream = new MemoryStream())
                {
                    formatter.Serialize(stream, t);
                    stream.Position = 0;
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    stream.Flush();
                    stream.Close();
                    return Convert.ToBase64String(buffer);
                }
            }
            catch (Exception ex)
            {
                //Log4NET.Log4Net.Error(ex.ToString(), ex);
                throw new Exception("序列化失败");
            }
        }



        /// <summary>
        /// 反序列化 字符串到对象
        /// </summary>
        /// <param name="str">要转换为对象的字符串</param>
        /// <returns>反序列化出来的对象</returns>
        public static T Desrialize<T>(string str)
        {
            T t = default(T);
            try
            {
                IFormatter formatter = new BinaryFormatter();
                byte[] buffer = Convert.FromBase64String(str);
                using (MemoryStream stream = new MemoryStream(buffer))
                {
                    t = (T)formatter.Deserialize(stream);
                    stream.Flush();
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                //Log4NET.Log4Net.Error(ex.ToString(), ex);
                throw new Exception("反序列化失败");
            }
            return t;
        }




        //获得客户端IP
        public static string GetClientIP(HttpRequest request)
        {
            // 穿过代理服务器取远程用户真实IP地址
            string ip = string.Empty;
            if (request.ServerVariables["HTTP_VIA"] != null)
            {
                if (request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
                {
                    if (request.ServerVariables["HTTP_CLIENT_IP"] != null)
                        ip = request.ServerVariables["HTTP_CLIENT_IP"].ToString();
                    else
                        if (request.ServerVariables["REMOTE_ADDR"] != null)
                            ip = request.ServerVariables["REMOTE_ADDR"].ToString();
                        else
                            ip = "";
                }
                else
                    ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (request.ServerVariables["REMOTE_ADDR"] != null)
            {
                ip = request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            else
            {
                ip = "";
            }
            return ip;
        }



        /// <summary>    
        /// 生成缩略图    
        /// </summary>   
        ///<param name="originalImagePath">原图片绝对路径</param>    
        /// <param name="thumNailPath">缩略图绝对路径</param>    
        /// <param name="width">缩略图宽度</param>    
        /// <param name="height">缩略图高度</param>   
        /// <param name="model">生成缩略的模式</param>
        public static void CreateThumbnail(string originalImagePath, string thumNailPath, int width, int height, string model)
        {
            System.Drawing.Image originalImage = null;
            System.Drawing.Image bitmap = null;
            System.Drawing.Graphics graphic = null;
            try
            {
                originalImage = System.Drawing.Image.FromFile(originalImagePath);
                int thumWidth = width;      //缩略图的宽度     
                int thumHeight = height;    //缩略图的高度     
                int x = 0; int y = 0;
                int originalWidth = originalImage.Width;    //原始图片的宽度   
                int originalHeight = originalImage.Height;  //原始图片的高度   
                switch (model)
                {
                    case "HW":
                        //指定高宽缩放,可能变形      
                        break;
                    case "W":
                        //指定宽度,高度按照比例缩放             
                        thumHeight = originalImage.Height * width / originalImage.Width;
                        break;
                    case "H":
                        //指定高度,宽度按照等比例缩放       
                        thumWidth = originalImage.Width * height / originalImage.Height;
                        break;
                    case "Cut"://指定高宽裁减（不变形）
                        if ((double)originalImage.Width / (double)originalImage.Height > (double)thumWidth / (double)thumHeight)
                        {
                            originalHeight = originalImage.Height;
                            originalWidth = originalImage.Height * thumWidth / thumHeight;
                            y = 0;
                            x = (originalImage.Width - originalWidth) / 2;
                        }
                        else
                        {
                            originalWidth = originalImage.Width;
                            originalHeight = originalWidth * height / thumWidth;
                            x = 0;
                            y = (originalImage.Height - originalHeight) / 2;
                        }
                        break;
                    default:
                        break;
                }
                //新建一个bmp图片      
                bitmap = new System.Drawing.Bitmap(thumWidth, thumHeight);
                //新建一个画板    
                graphic = System.Drawing.Graphics.FromImage(bitmap);
                //设置高质量查值法     
                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //设置高质量，低速度呈现平滑程度      
                graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //清空画布并以透明背景色填充     
                graphic.Clear(System.Drawing.Color.Transparent);
                //在指定位置并且按指定大小绘制原图片的指定部分     
                graphic.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, thumWidth, thumHeight), new System.Drawing.Rectangle(x, y, originalWidth, originalHeight), System.Drawing.GraphicsUnit.Pixel);

                bitmap.Save(thumNailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (originalImage != null)
                    originalImage.Dispose();
                if (bitmap != null)
                    bitmap.Dispose();
                if (graphic != null)
                    graphic.Dispose();
            }
        }




        /// <summary>
        /// 将DataTable转换成Json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        sb.AppendFormat("\"{0}\":\"{1}\"", dt.Columns[j].ColumnName, dt.Rows[i][j].ToString());
                        if (j < dt.Columns.Count - 1)
                            sb.Append(",");
                    }
                    sb.Append("}");
                    if (i < dt.Rows.Count - 1)
                        sb.Append(",");
                }
                sb.Append("]");
                return sb.ToString();
            }
            else
                return string.Empty;

        }


        /// <summary>
        /// 获取MD5值
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns>返回MD5值</returns>
        public static string GetMD5(string str)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
        }
    }
}
