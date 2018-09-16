<%@ WebHandler Language="C#" Class="baymyoStatic.SecurityImage" %>

using System;
using System.Web;

namespace baymyoStatic
{
    public class SecurityImage : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            try
            {
                if (context.Session["securityCode"].Equals(true))
                {
                    context.Response.ContentType = "image/jpeg";
                    using (System.Drawing.Image vi = new System.Drawing.Bitmap(125, 31))
                    {
                        using (System.Drawing.Graphics vd = System.Drawing.Graphics.FromImage(vi))
                        {
                            string code = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 5).ToUpper();
                            Random rnd = new Random();
                            vd.Clear(System.Drawing.Color.FromArgb(75, 51, 105));
                            using (System.Drawing.Brush textBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 255, 255)))
                            {
                                context.Session["securityCode"] = Core.Compute(code);
                                vd.DrawString(code.Insert(rnd.Next(1, 4), " "), new System.Drawing.Font(System.Drawing.FontFamily.GenericMonospace, rnd.Next(16, 20), System.Drawing.FontStyle.Bold), textBrush, rnd.Next(0, 5), rnd.Next(0, 5));
                                vd.DrawString(Guid.NewGuid().ToString().Replace("-", " ").ToUpper() + "\n" + Guid.NewGuid().ToString().Replace("-", " ").ToUpper(), new System.Drawing.Font(System.Drawing.FontFamily.GenericMonospace, rnd.Next(1, 12), System.Drawing.FontStyle.Regular), textBrush, rnd.Next(-100, 100), -1);
                                vi.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                                code = null;
                            }
                        }
                    }
                }
                else
                {
                    context.Response.Write("No Data!");
                    context.Session.Remove("securityCode");
                }
            }
            catch (Exception)
            {
                context.Response.Write("No Data Error!");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}