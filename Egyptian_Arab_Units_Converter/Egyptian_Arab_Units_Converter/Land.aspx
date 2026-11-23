<%@ Import Namespace="Egyptian_Arab_Units_Converter" %>
<%@ Page Title="Land - Egyptian Units Converter" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Land.aspx.cs" Inherits="Egyptian_Arab_Units_Converter.LandPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="page-head">
        <div class="wrap">
            <h1><%= SiteLang.T("LandTitle") %></h1>
            <p><%= SiteLang.T("LandSub") %></p>
        </div>
    </section>

    <div class="wrap block">
        <asp:Label ID="lblError" runat="server" CssClass="alert" Visible="false" EnableViewState="false" />
        <div class="grid-2">
            <div class="card">
                <h2><%= SiteLang.T("LandInputTitle") %></h2>
                <label for="<%= txtLandAmount.ClientID %>"><%= SiteLang.T("Amount") %></label>
                <asp:TextBox ID="txtLandAmount" runat="server" CssClass="inp" inputmode="decimal" />
                <div class="row">
                    <div>
                        <label for="<%= ddlLandFrom.ClientID %>"><%= SiteLang.T("From") %></label>
                        <asp:DropDownList ID="ddlLandFrom" runat="server" CssClass="inp" />
                    </div>
                    <button type="button" class="swap" data-swap="<%= ddlLandFrom.ClientID %>,<%= ddlLandTo.ClientID %>" aria-label="swap">⇄</button>
                    <div>
                        <label for="<%= ddlLandTo.ClientID %>"><%= SiteLang.T("To") %></label>
                        <asp:DropDownList ID="ddlLandTo" runat="server" CssClass="inp" />
                    </div>
                </div>
                <asp:Button ID="btnLand" runat="server" CssClass="btn btn-primary btn-block" OnClick="btnLand_Click" />
                <p class="hint"><%= SiteLang.T("LandHint") %></p>
            </div>

            <div class="card">
                <asp:PlaceHolder ID="phLandEmpty" runat="server">
                    <p class="empty"><%= SiteLang.T("LandEmpty") %></p>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phLandResult" runat="server" Visible="false">
                    <div class="big-line"><asp:Literal ID="litLandBig" runat="server" /></div>
                    <asp:Literal ID="litFeddanSplit" runat="server" />
                    <div class="sub"><%= SiteLang.T("LandAlsoEq") %></div>
                    <div class="tiles">
                        <asp:Repeater ID="rptLandAll" runat="server">
                            <ItemTemplate>
                                <div class="tile">
                                    <span class="tile-v"><%# Eval("Value") %></span>
                                    <span class="tile-n"><%# Eval("Name") %></span>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </asp:PlaceHolder>
            </div>
        </div>
    </div>
</asp:Content>