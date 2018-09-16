<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="kategori.ascx.cs" Inherits="baymyoStatic.panel.ascx.kategori" %>
<%@ Register Assembly="BAYMYO.UI" Namespace="BAYMYO.UI.Data" TagPrefix="baymyoCnt" %>
<%@ Register Src="~/common/control/CustomizeControl.ascx" TagName="CustomizeControl"
    TagPrefix="baymyoCnt" %>
<baymyoCnt:CustomizeControl ID="CustomizeControl1" FormTitleVisible="true" runat="server" />
<baymyoCnt:HierarchicalObjectDataSource ID="hierarDataSource" DataParentField="ParentID"
    DataValueField="Value" DataTextField="Text" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetHierarchical" TypeName="baymyoStatic.KategoriMethods">
    <SelectParameters>
        <asp:QueryStringParameter Name="modulid" QueryStringField="mdl" Type="String" />
        <asp:Parameter DefaultValue="true" Name="rootNode" Type="Boolean" />
    </SelectParameters>
</baymyoCnt:HierarchicalObjectDataSource>