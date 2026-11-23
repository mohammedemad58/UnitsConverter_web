<%@ Import Namespace="Egyptian_Arab_Units_Converter" %>
<%@ Page Title="History - Egyptian Units Converter" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="Egyptian_Arab_Units_Converter.HistoryPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="page-head">
        <div class="wrap">
            <h1><%= SiteLang.T("HistoryTitle") %></h1>
            <p><%= SiteLang.T("HistorySub") %></p>
        </div>
    </section>
    <div class="wrap block">
        <div class="card">
            <asp:Repeater ID="rptHistory" runat="server">
                <HeaderTemplate><ul class="hist"></HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <span class="tag <%# Eval("Category").ToString() == "Land" ? "tag-land" : "tag-gold" %>">
                            <%# Eval("Category").ToString() == "Land" ? SiteLang.T("TagLand") : SiteLang.T("TagGold") %>
                        </span>
                        <span class="hist-t"><%# Server.HtmlEncode(Eval("Details").ToString()) %></span>
                        <time><%# Convert.ToDateTime(Eval("CreatedAt")).ToString("yyyy-MM-dd HH:mm") %></time>
                    </li>
                </ItemTemplate>
                <FooterTemplate></ul></FooterTemplate>
            </asp:Repeater>
            <asp:Label ID="lblNoHistory" runat="server" CssClass="empty" Visible="false" />
        </div>
    </div>
</asp:Content>