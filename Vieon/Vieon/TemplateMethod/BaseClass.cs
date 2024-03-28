using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Vieon.Models
{
    public abstract class BasePage
    {
        protected abstract string RenderHeader();
        protected abstract string RenderContent();
        protected abstract string RenderFooter();

        public string RenderPage()
        {
            var result = new StringBuilder();
            result.Append(RenderHeader());
            result.Append(RenderContent());
            result.Append(RenderFooter());
            return result.ToString();
        }
    }
}