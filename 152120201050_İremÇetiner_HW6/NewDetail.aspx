<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsDetail.aspx.cs" Inherits="HW6Web.NewsDetail" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>News Detail</title>
    <style>
        .news-detail {
            max-width: 600px;
            margin: auto;
        }

        .news-detail img {
            max-width: 100%;
            height: auto;
        }

        .news-detail h2 {
            font-size: 24px;
            margin: 20px 0;
        }

        .news-detail p {
            font-size: 16px;
            line-height: 1.5;
        }

        .news-detail .meta {
            font-size: 14px;
            color: #666;
            margin-bottom: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="news-detail">
            <asp:Image ID="imgNews" runat="server" />
            <h2><asp:Label ID="lblDescription" runat="server" Text=""></asp:Label></h2>
            <p class="meta">By <asp:Label ID="lblAuthor" runat="server" Text=""></asp:Label> on <asp:Label ID="lblPubDate" runat="server" Text=""></asp:Label></p>
        </div>
    </form>
</body>
</html>
