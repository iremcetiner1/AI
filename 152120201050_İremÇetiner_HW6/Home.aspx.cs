using System;
using System.Collections;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Net;
namespace HW6Web
{
    public partial class Home : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
       
                // Default kategori yüklenebilir burada
            }
        }
        
        }
        protected void btnCategory_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string category = btn.CommandArgument;
            LoadNews(category);
        }

        private void LoadNews(string category)
        {
            ArrayList newsList = new ArrayList();
            string rssUrl = GetRssUrl(category);

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
                string pubDate = pubDateNode != null ? ConvertToTurkishDate(pubDateNode.InnerText) : "";
                string imageUrl = imageUrlNode != null ? imageUrlNode.Value : "";

                int newsID = GenerateUniqueID();

                newsList.Add(new NewsItem(newsID, description, author, pubDate, imageUrl));
            }

            // Haber listesini uygulama değişkenine kaydet
            Application["NewsList"] = newsList;

            GridView1.DataSource = newsList;
            GridView1.DataBind();
        }

        private string ConvertToTurkishDate(string dateStr)
        {
            DateTime pubDate;
            if (DateTime.TryParse(dateStr, out pubDate))
            {
                return pubDate.ToString("dd MMMM yyyy HH:mm", new CultureInfo("tr-TR"));
            }
            return dateStr;
        }

        private string GetRssUrl(string category)
        {
            switch (category)
            {
                case "sports":
                    return "https://m.haberturk.com/rss/spor.xml";
                case "science":
                    return "https://www.haberturk.com/rss/kategori/teknoloji.xml";
                case "health":
                    return "https://www.haberturk.com/rss/kategori/saglik.xml";
                case "travel":
                    return "https://www.haberturk.com/rss/kategori/tatil.xml";
                case "art":
                    return "https://www.haberturk.com/rss/kategori/kultur-sanat.xml";
                default:
                    return "https://www.haberturk.com/rss/manset.xml";
            }
        }

        private int GenerateUniqueID()
        {
            return Guid.NewGuid().GetHashCode();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetail")
            {
                int newsID = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("NewDetail.aspx?NewsID=" + newsID);
            }
        }
    }

    public class NewsItem
    {
        public int NewsID { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string PubDate { get; set; }
        public string ImageUrl { get; set; }

        public NewsItem(int newsID, string description, string author, string pubDate, string imageUrl)
        {
            NewsID = newsID;
            Description = description;
            Author = author;
            PubDate = pubDate;
            ImageUrl = imageUrl;
        }
    }
}
