using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vieon.Models
{
    public class HomePage : BasePage
    {
        protected override string RenderHeader()
        {
            return "<header><h1>Welcome to our website!</h1></header>";
        }

        protected override string RenderContent()
        {
            return "<section><p>This is the home page content...</p></section>";
        }

        protected override string RenderFooter()
        {
            return "<footer><p>&copy; 2024 Our Website</p></footer>";
        }
    }
}