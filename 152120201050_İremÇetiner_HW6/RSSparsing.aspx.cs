using System;
using System.Collections;
using System.Web.UI;
using System.Xml;
using HW6Web;
namespace HW6Web
{
    public partial class RSSparsing : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ArrayList newsList = new ArrayList();
                string rssUrl = "https://m.haberturk.com/rss/spor.xml";
                
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(rssUrl);

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
                nsmgr.AddNamespace("media", "http://search.yahoo.com/mrss/");

                XmlNodeList items = xmlDoc.SelectNodes("//item");

                foreach (XmlNode item in items)
                {
                    XmlNode descriptionNode = item.SelectSingleNode("description");
                    XmlNode authorNode = item.SelectSingleNode("author");
                    XmlNode pubDateNode = item.SelectSingleNode("pubDate");
                    XmlNode imageUrlNode = item.SelectSingleNode("media:content/@url", nsmgr);

                    string description = descriptionNode != null ? descriptionNode.InnerText : "";
                    string author = authorNode != null ? authorNode.InnerText : "";
                    string pubDate = pubDateNode != null ? pubDateNode.InnerText : "";
                    string imageUrl = imageUrlNode != null ? imageUrlNode.Value : "";

                    int newsID = GenerateUniqueID();

                    newsList.Add(new NewsItem(newsID, description, author, pubDate, imageUrl));
                }

                Session["NewsList"] = newsList;
                Response.Redirect("Home.aspx");
            }
        }

        private int GenerateUniqueID()
        {
            return DateTime.Now.GetHashCode();
        }
    }
}
