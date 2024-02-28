<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="CP.Inicio" %>

<%--<!DOCTYPE html>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Facturación UEB</title>
    <link href="css/estilo.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tbody>
            <tr>
                <td align="center">
                    <table border="0" cellpadding="0" cellspacing="0" width="978">
                        <tbody>
                            <tr>
                                <td>
                                    <div id="header">
                                        <div class="logo">
                                            <img alt="" height="" width="" src="imagen/logo_ueb.png" />
                                        </div>
                                    </div>
                                    <div id="content">
                                        <div class="title">
                                            <div class="dtitle">
                                                <h1 class="lbtitle">FACTURACIÓN UEB PRUEBAS</h1>
                                            </div>
                                            <%--  <div class="rximg">
                                                <img src="imagen/dxr.png" alt="" width="65" height="65" />
                                            </div>--%>
                                        </div>
                                        <div class="ddatos">
                                            <div class="dcampos">
                                                <asp:Label ID="lbFactura" runat="server" Text="N. Transaccion" CssClass="lbFactura"></asp:Label>
                                                <asp:TextBox  ID="tboxFactura" runat="server" Width="130px" CssClass="tboxFactura"></asp:TextBox>
                                               
                                                 <asp:Label ID="Label2" runat="server" Text="Estudiante" CssClass="lbFactura" ></asp:Label>
                                               
                                                <asp:TextBox ID="tboxCode" runat="server" Width="170px" CssClass="tboxFactura"></asp:TextBox>


                                                <asp:Label ID="lbTCaja" runat="server" Text="Tipo Caja" CssClass="lbFecha"></asp:Label>
                                                <asp:TextBox ID="tboxTCaja" runat="server" Width="50px" CssClass="tboxFecha" OnTextChanged="tboxTCaja_TextChanged" ReadOnly="True">1</asp:TextBox>

                                                <asp:Label ID="lbFecha" runat="server" Text="Fecha" CssClass="lbFecha" Visible="False"></asp:Label>
                                               
                                                <asp:TextBox ID="tboxFecha" runat="server" Width="170px" CssClass="tboxFecha" AllowNull="False" TabIndex="3" ToolTip="Fecha Factura" TextMode="Date" Enabled="False" ReadOnly="True" Visible="False"></asp:TextBox>
                                               
                                                <%--<dx:ASPxTextBox ID="tboxFecha" runat="server" Width="170px" CssClass="tboxFecha">
                                                </dx:ASPxTextBox>--%><%--   <asp:Button ID="btEnviar" runat="server" Text="Enviar" OnClick="btEnviar_Click"
                                                    CssClass="btEnviar" ValidationGroup="vgroFactura" />--%>                                                <%--  <asp:Button ID="btEnviar2" runat="server" Text="Enviar Odoo" OnClick="btEnviar_ClickOdoo"
                                                    CssClass="btEnviar" ValidationGroup="vgroFactura" />--%>
                                               
                                               
                                               
                                            </div>
                                            <div class="dvalidacion">
                                                <asp:RequiredFieldValidator ID="rvalFactura" ControlToValidate="tboxFactura" ValidationGroup="vgroFactura"
                                                    runat="server" ErrorMessage="INGRESE NRO.TRANSACCION" CssClass="valcampo" />
                                                <asp:RequiredFieldValidator ID="rvalTCaja" ControlToValidate="tboxTCaja" ValidationGroup="vgroFactura"
                                                    runat="server" ErrorMessage="INGRESE T.Caja" CssClass="valcampo" />
                                                <asp:ValidationSummary ID="valSumary" runat="server" ValidationGroup="vgroFactura"
                                                    ShowMessageBox="true" ShowSummary="false" />
                                            </div>
                                            <div ="ddatos">
                                                <%--  <div class="dcampos">
                                                 <asp:Label  ID="LabelStudent" runat="server" Text="Estudiante" style="float: left; margin: 5px 40px 10px 15px;" ></asp:Label>
                                                 <asp:TextBox  ID="TextBox1" runat="server" Width="170px" CssClass="tboxFactura" ></asp:TextBox>
                                                 
                                                 <asp:Label ID="labelMonto" runat="server" Text="Monto"  style="float: left; margin: 5px 40px 10px 15px;"></asp:Label>
                                                 <asp:TextBox  ID="TextMonto" runat="server" Width="70px" CssClass="tboxFactura"></asp:TextBox> 
                                              
                                            </div>--%><%--                                     <div class="dcampos">
                                                 <asp:Label ID="LabelGlosa" runat="server" Text="Glosa" CssClass="lbFactura"></asp:Label>
                                                 <asp:TextBox  ID="TextBoxGlosa" runat="server" Width="170px" CssClass="tboxFactura"></asp:TextBox>
                                            </div>--%>
                                        </div>
                                           
                                            
                                        <div style="text-align:center;padding-top:10px;">
                                         <asp:Button style="margin-top:40px;" ID="btEnviar2" runat="server" Text="Enviar Odoo" OnClick="btEnviar_ClickOdoo"  ValidationGroup="vgroFactura"/>
                                            
                                           
                                            <asp:Button style="margin-left:7px;" ID="ButtonUpdateFacturacion" runat="server" Text="Actualizar Odoo" OnClick="updateFacturacion" />
                                            
                                           
                                        </div>
                                     
                                        <div style="text-align:center;padding-bottom:50px;padding-top:20px;" class="report" id="divReport">
                                            
                                           <%-- <iframe  id="invoice_frame" src="http://192.168.7.9:9096/"></iframe><br /> --%><%--    <dx:aspxdocumentviewer  ID="docView" runat="server" ClientInstanceName="docView" ReportTypeName="CP.Reportes.rFactura">
                                            </dx:aspxdocumentviewer>--%>
                                            <asp:Label ID="Label1" runat="server" Text="La factura se ha creado correctamente." Visible="False"></asp:Label>
                                               
                                            <asp:HyperLink ID="HyperLinkImprimir" runat="server" Target="_blank" Visible="False">Visualizar Factura</asp:HyperLink>
                                                <div id="errorMensaje" runat="server">
                                                     <asp:Label ID="LabelError" runat="server"></asp:Label>
                                                     <asp:Label  style="display:block;border: 1px solid rgb(214,233,198);background-color: rgb(223,240,216);color: #721c24;border-radius: 0.25rem;" ID="LabelExito" runat="server" Text="Actualizacion realizada con exito" ViewStateMode="Enabled" Visible="False"></asp:Label>
                                                </div>
                                             
                                        </div>
                                
                                       
                                    </div>
                                    <div id="footer">
                                        <p class="ayuda">
                                                              
                                        </p>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>
