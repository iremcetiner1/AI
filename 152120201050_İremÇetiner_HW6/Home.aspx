<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="HW6Web.Home" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Haberler</title>
    <link href="styles.css" rel="stylesheet" type="text/css" />
    <style>
        /* styles.css */
        .header {
            background-color: black;
            color: white;
            text-align: center;
            padding: 10px 0;
            height: 61px;
        }

        .header img {
            vertical-align: middle;
            width: 50px;
            height: 50px;
        }

        .header h1 {
            display: inline;
            margin-left: 10px;
            vertical-align: middle;
        }

        /* styles.css */
        .category-buttons {
            margin: 20px 0;
            text-align: center;
        }

        .category-buttons .btn {
            margin: 0 5px;
            padding: 10px 20px;
            border: none;
            cursor: pointer;
            border-radius: 5px;
        }

        .category-buttons #btnSports {
            background-color: #4CAF50;
            color: white;
        }

        .category-buttons #btnScience {
            background-color: #007bff;
            color: white;
        }

        .category-buttons #btnHealth {
            background-color: #dc3545;
            color: white;
        }

        .category-buttons #btnTravel {
            background-color: #ffc107;
            color: black;
        }

        .category-buttons #btnArt {
            background-color: #6c757d;
            color: white;
        }


        .category-buttons asp:Button {
            margin: 0 5px;
        }

        .gridview-table {
            width: 100%;
            border-collapse: collapse;
            margin: 20px 0;
        }

        .gridview-table th, .gridview-table td {
            border: 1px solid #ddd;
            padding: 8px;
        }

        .gridview-table th {
            background-color: #f2f2f2;
            text-align: left;
        }

        .gridview-table tr:nth-child(even) {
            background-color: #f9f9f9;
        }

        .gridview-table tr:hover {
            background-color: #ddd;
        }

        .gridview-image {
            max-width: 100px;
            height: auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <h1>HaBerBaHçE</h1>
        </div>
        <div class="category-buttons">
            <!-- Kategori Butonları -->
            <asp:Button ID="btnSports" runat="server" Text="Spor" OnClick="btnCategory_Click" CommandArgument="sports" Height="55px" Width="70px" />
            <asp:Button ID="btnScience" runat="server" Text="Bilim" OnClick="btnCategory_Click" CommandArgument="science" Height="55px" />
            <asp:Button ID="btnHealth" runat="server" Text="Sağlık" OnClick="btnCategory_Click" CommandArgument="health" Height="55px" Width="72px" />
            <asp:Button ID="btnTravel" runat="server" Text="Seyahat" OnClick="btnCategory_Click" CommandArgument="travel" ForeColor="White" Height="55px" Width="66px" />
            <asp:Button ID="btnArt" runat="server" Text="Sanat" OnClick="btnCategory_Click" CommandArgument="art" Height="55px" Width="51px" />
        </div>
        
        <!-- Haberleri Gösteren GridView -->
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="gridview-table" OnRowCommand="GridView1_RowCommand">
            <Columns>
      
                <asp:TemplateField HeaderText="Görsel">
                    <ItemTemplate>
                        <div>
                            <asp:Image ID="Image1" runat="server" CssClass="gridview-image" ImageUrl='<%# Eval("ImageUrl") %>' />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Açıklama">
                    <ItemTemplate>
                        <div>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Yayın Tarihi">
                    <ItemTemplate>
                        <div>
                            <asp:Label ID="lblPubDate" runat="server" Text='<%# Eval("PubDate") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Detay">
            <ItemTemplate>
                <div>
                    <asp:Button ID="btnViewDetail" runat="server" Text="Haber Detayı" CommandName="ViewDetail" CommandArgument='<%# Eval("NewsID") %>' />
                </div>
            </ItemTemplate>
        </asp:TemplateField>
            </Columns>
        </asp:GridView>
       
    </form>
</body>
</html>

