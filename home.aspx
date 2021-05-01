<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="HealthManagementApplication.home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderLeft" runat="server">
    <div>
        <p>Sign up: </p>
        <div>
            <span>User name: </span>
            <input runat="server" type="text" name="username" value="" id="registerNameInput" autocomplete="on" />
        </div>
        <div>
            <span>Password: </span>
            <input runat="server" type="password" name="password" value="" autocomplete="on" />
        </div>
        <br />
        <asp:Button Text="OK" runat="server" OnClick="Register_button"/>

        <span id="consoletext" runat="server"></span>

        <asp:GridView runat="server" id="grid"></asp:GridView>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
