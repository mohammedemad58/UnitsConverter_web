<%@ Import Namespace="Egyptian_Arab_Units_Converter" %>
<%@ Page Title="Home - Egyptian Units Converter" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Egyptian_Arab_Units_Converter._Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section class="hero">
        <div class="wrap hero-in">
            <div class="hero-txt">
                <h1><%= SiteLang.T("HeroTitle") %></h1>
                <p><%= SiteLang.T("HeroSub") %></p>
                <div class="hero-cta">
                    <a class="btn-gold" href="Land.aspx"><%= SiteLang.T("HeroLandCta") %></a>
                    <a class="btn-ghost" href="Gold.aspx"><%= SiteLang.T("HeroGoldCta") %></a>
                </div>
            </div>
            <aside class="ref" aria-label="<%= SiteLang.T("RefTitle") %>">
                <h2><%= SiteLang.T("RefTitle") %></h2>
                <ul>
                    <li><span>1 <%= SiteLang.IsRtl ? "فدان" : "feddan" %></span><b>24 <%= SiteLang.IsRtl ? "قيراط" : "qirat" %></b></li>
                    <li><span>1 <%= SiteLang.IsRtl ? "قيراط" : "qirat" %></span><b>24 <%= SiteLang.IsRtl ? "سهم" : "sahm" %></b></li>
                    <li><span>1 <%= SiteLang.IsRtl ? "فدان" : "feddan" %></span><b>576 <%= SiteLang.IsRtl ? "سهم" : "sahm" %></b></li>
                    <li><span>1 <%= SiteLang.IsRtl ? "فدان" : "feddan" %></span><b>4,200.83 m²</b></li>
                    <li><span>1 <%= SiteLang.IsRtl ? "قيراط" : "qirat" %></span><b>175.03 m²</b></li>
                    <li><span>1 <%= SiteLang.IsRtl ? "سهم" : "sahm" %></span><b>7.29 m²</b></li>
                </ul>
            </aside>
        </div>
    </section>

    <section class="wrap block">
        <h2 class="sec-t"><%= SiteLang.T("ChooseTitle") %></h2>
        <div class="cats">
            <a class="cat" href="Land.aspx">
                <span class="cat-ic">▦</span>
                <h3><%= SiteLang.T("CatLandTitle") %></h3>
                <p><%= SiteLang.T("CatLandSub") %></p>
                <span class="cat-go"><%= SiteLang.T("CatLandGo") %> →</span>
            </a>
            <a class="cat" href="Gold.aspx#weight">
                <span class="cat-ic">◍</span>
                <h3><%= SiteLang.T("CatGoldWTitle") %></h3>
                <p><%= SiteLang.T("CatGoldWSub") %></p>
                <span class="cat-go"><%= SiteLang.T("CatGoldWGo") %> →</span>
            </a>
            <a class="cat" href="Gold.aspx#karat">
                <span class="cat-ic">◈</span>
                <h3><%= SiteLang.T("CatGoldKTitle") %></h3>
                <p><%= SiteLang.T("CatGoldKSub") %></p>
                <span class="cat-go"><%= SiteLang.T("CatGoldKGo") %> →</span>
            </a>
            <a class="cat" href="Gold.aspx#price">
                <span class="cat-ic">◉</span>
                <h3><%= SiteLang.T("CatGoldPTitle") %></h3>
                <p><%= SiteLang.T("CatGoldPSub") %></p>
                <span class="cat-go"><%= SiteLang.T("CatGoldPGo") %> →</span>
            </a>
        </div>
    </section>

    <section class="wrap block">
        <div class="how">
            <div>
                <b><%= SiteLang.T("How1T") %></b>
                <p><%= SiteLang.T("How1D") %></p>
            </div>
            <div>
                <b><%= SiteLang.T("How2T") %></b>
                <p><%= SiteLang.T("How2D") %></p>
            </div>
            <div>
                <b><%= SiteLang.T("How3T") %></b>
                <p><%= SiteLang.T("How3D") %></p>
            </div>
        </div>
    </section>

</asp:Content>