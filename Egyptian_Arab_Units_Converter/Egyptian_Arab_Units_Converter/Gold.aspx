<%@ Import Namespace="Egyptian_Arab_Units_Converter" %>
<%@ Page Title="Gold - Egyptian Units Converter" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" MaintainScrollPositionOnPostBack="true" CodeBehind="Gold.aspx.cs" Inherits="Egyptian_Arab_Units_Converter.GoldPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="page-head">
        <div class="wrap">
            <h1><%= SiteLang.T("GoldPageTitle") %></h1>
            <p><%= SiteLang.T("GoldPageSub") %></p>
            <div class="chips">
                <a href="#weight"><%= SiteLang.T("GoldWeight") %></a>
                <a href="#karat"><%= SiteLang.T("GoldKarat") %></a>
                <a href="#price"><%= SiteLang.T("GoldPrice") %></a>
            </div>
        </div>
    </section>

    <div class="wrap block">
        <asp:Label ID="lblError" runat="server" CssClass="alert" Visible="false" EnableViewState="false" />
        <div class="grid-3">

            <div class="card" id="weight">
                <h2><%= SiteLang.T("WeightTitle") %></h2>
                <label for="<%= txtGWAmount.ClientID %>"><%= SiteLang.T("Weight") %></label>
                <asp:TextBox ID="txtGWAmount" runat="server" CssClass="inp" inputmode="decimal" />
                <div class="row">
                    <div>
                        <label for="<%= ddlGWFrom.ClientID %>"><%= SiteLang.T("From") %></label>
                        <asp:DropDownList ID="ddlGWFrom" runat="server" CssClass="inp" />
                    </div>
                    <button type="button" class="swap" data-swap="<%= ddlGWFrom.ClientID %>,<%= ddlGWTo.ClientID %>" aria-label="swap">⇄</button>
                    <div>
                        <label for="<%= ddlGWTo.ClientID %>"><%= SiteLang.T("To") %></label>
                        <asp:DropDownList ID="ddlGWTo" runat="server" CssClass="inp" />
                    </div>
                </div>
                <asp:Button ID="btnGW" runat="server" CssClass="btn btn-primary btn-block" OnClick="btnGW_Click" />
                <asp:Panel ID="pnlGW" runat="server" Visible="false" CssClass="out"><asp:Literal ID="litGW" runat="server" /></asp:Panel>
                <p class="hint"><%= SiteLang.T("WeightHint") %></p>
            </div>

            <div class="card" id="karat">
                <h2><%= SiteLang.T("KaratTitle") %></h2>
                <label for="<%= txtGKGrams.ClientID %>"><%= SiteLang.T("Grams") %></label>
                <asp:TextBox ID="txtGKGrams" runat="server" CssClass="inp" inputmode="decimal" />
                <div class="row">
                    <div>
                        <label for="<%= ddlKFrom.ClientID %>"><%= SiteLang.T("FromKarat") %></label>
                        <asp:DropDownList ID="ddlKFrom" runat="server" CssClass="inp" />
                    </div>
                    <button type="button" class="swap" data-swap="<%= ddlKFrom.ClientID %>,<%= ddlKTo.ClientID %>" aria-label="swap">⇄</button>
                    <div>
                        <label for="<%= ddlKTo.ClientID %>"><%= SiteLang.T("ToKarat") %></label>
                        <asp:DropDownList ID="ddlKTo" runat="server" CssClass="inp" />
                    </div>
                </div>
                <asp:Button ID="btnKarat" runat="server" CssClass="btn btn-primary btn-block" OnClick="btnKarat_Click" />
                <asp:Panel ID="pnlKarat" runat="server" Visible="false" CssClass="out"><asp:Literal ID="litKarat" runat="server" /></asp:Panel>
                <p class="hint"><%= SiteLang.T("KaratHint") %></p>
            </div>

            <div class="card" id="price">
                <h2><%= SiteLang.T("PriceTitle") %></h2>
                <div class="row" style="grid-template-columns:1fr 1fr;margin-top:0">
                    <div>
                        <label for="<%= txtPGrams.ClientID %>"><%= SiteLang.T("GramsShort") %></label>
                        <asp:TextBox ID="txtPGrams" runat="server" CssClass="inp" inputmode="decimal" />
                    </div>
                    <div>
                        <label for="<%= ddlPKarat.ClientID %>"><%= SiteLang.T("Karat") %></label>
                        <asp:DropDownList ID="ddlPKarat" runat="server" CssClass="inp" AutoPostBack="true" OnSelectedIndexChanged="ddlPKarat_Changed" />
                    </div>
                </div>
                <label for="<%= txtPPrice.ClientID %>"><%= SiteLang.T("GramPrice") %></label>
                <asp:TextBox ID="txtPPrice" runat="server" CssClass="inp" inputmode="decimal" />
                <div class="row" style="grid-template-columns:1fr 1fr">
                    <div>
                        <label for="<%= txtMaking.ClientID %>"><%= SiteLang.T("Making") %></label>
                        <asp:TextBox ID="txtMaking" runat="server" CssClass="inp" inputmode="decimal" placeholder="0" />
                    </div>
                    <div>
                        <label for="<%= ddlMakingType.ClientID %>"><%= SiteLang.T("MakingType") %></label>
                        <asp:DropDownList ID="ddlMakingType" runat="server" CssClass="inp">
                            <asp:ListItem Value="gram" Text="gram" />
                            <asp:ListItem Value="percent" Text="percent" />
                            <asp:ListItem Value="total" Text="total" />
                        </asp:DropDownList>
                    </div>
                </div>
                <label class="chk"><asp:CheckBox ID="chkSavePrice" runat="server" /> <%= SiteLang.T("SavePrice") %></label>
                <asp:Button ID="btnPrice" runat="server" CssClass="btn btn-primary btn-block" OnClick="btnPrice_Click" />
                <asp:Panel ID="pnlPrice" runat="server" Visible="false" CssClass="out"><asp:Literal ID="litPrice" runat="server" /></asp:Panel>
            </div>

        </div>
    </div>
</asp:Content>