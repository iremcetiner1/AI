using System;
using System.Collections;
using System.Web.UI;

namespace HW6Web
{
    public partial class NewsDetail : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string newsID = Request.QueryString["NewsID"];
                if (!string.IsNullOrEmpty(newsID))
                {
                    LoadNewsDetail(int.Parse(newsID));
                }
            }
        }

        private void LoadNewsDetail(int newsID)
        {
            // Haber listesine erişim
            ArrayList newsList = (ArrayList)Application["NewsList"];
            if (newsList != null)
            {
                foreach (NewsItem news in newsList)
                {
                    if (news.NewsID == newsID)
                    {
                        imgNews.ImageUrl = news.ImageUrl;
                        lblDescription.Text = news.Description;
                        lblAuthor.Text = news.Author;
                        lblPubDate.Text = news.PubDate;
                        break;
                    }
                }
            }
        }
    }
}
