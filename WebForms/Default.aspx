<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cadastro de Cliente</title>
    <link href="CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="CSS/ajuste-forms.css" rel="stylesheet" />
    <link href="CSS/bootstrap-icons.css" rel="stylesheet" />
</head>
<body>
    <div class="container mt-4">
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />

            <h2 class="mb-4">Cliente</h2>

            <div class="row mb-3">
                <div class="col-md-4 mb-3">
                    <label>CPF</label>
                    <asp:TextBox ID="txtCpf" runat="server" CssClass="form-control" MaxLength="14" />
                    <ajaxToolkit:MaskedEditExtender ID="meeCpf" runat="server"
                        TargetControlID="txtCpf"
                        Mask="999.999.999-99"
                        MaskType="None"
                        InputDirection="LeftToRight"
                        MessageValidatorTip="true"
                        ClearMaskOnLostFocus="False" />
                    <ajaxToolkit:MaskedEditValidator ID="mevCpf" runat="server"
                        ControlExtender="meeCpf"
                        ControlToValidate="txtCpf"
                        IsValidEmpty="false"
                        EmptyValueMessage="CPF obrigatório"
                        InvalidValueMessage="CPF inválido"
                        Display="Dynamic"
                        ForeColor="Red" />
                </div>
                <div class="col-md-8 mb-3">
                    <label>Nome</label>
                    <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="valNome" runat="server" ControlToValidate="txtNome" ErrorMessage="Nome obrigatório" ForeColor="red" Display="Dynamic" />
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-2 mb-3">
                    <label>RG</label>
                    <asp:TextBox ID="txtRg" runat="server" CssClass="form-control" MaxLength="12" />
                    <ajaxToolkit:MaskedEditExtender ID="meeRg" runat="server"
                        TargetControlID="txtRg"
                        Mask="9.999.999-9"
                        MaskType="None"
                        InputDirection="LeftToRight"
                        MessageValidatorTip="true"
                        ClearMaskOnLostFocus="False"/>
                    <ajaxToolkit:MaskedEditValidator ID="mevRg" runat="server"
                        ControlExtender="meeRg"
                        ControlToValidate="txtRg"
                        IsValidEmpty="false"
                        EmptyValueMessage="RG obrigatório"
                        InvalidValueMessage="RG inválido"
                        Display="Dynamic"
                        ForeColor="Red" />
                </div>
                <div class="col-md-3 mb-3">
                    <label>Data Expedição</label>
                    <asp:TextBox ID="txtDataExpedicao" runat="server" CssClass="form-control" MaxLength="10" />
                    <ajaxToolkit:MaskedEditExtender ID="meeDataExp" runat="server"
                        TargetControlID="txtDataExpedicao"
                        Mask="99/99/9999"
                        MaskType="Date"
                        InputDirection="LeftToRight"
                        MessageValidatorTip="true"
                        ClearMaskOnLostFocus="False"
                        CultureName="pt-BR" />
                    <asp:CompareValidator ID="valDataExp" runat="server" ControlToValidate="txtDataExpedicao" Operator="DataTypeCheck" Type="Date" ErrorMessage="Data Expedição inválida" ForeColor="red" Display="Dynamic" />
                </div>
                <div class="col-md-4 mb-3">
                    <label>Órgão Expedição</label>
                    <asp:TextBox ID="txtOrgaoExpedicao" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="valOrgao" runat="server" ControlToValidate="txtOrgaoExpedicao" ErrorMessage="Órgão Expedição obrigatório" ForeColor="red" Display="Dynamic" />
                </div>
                <div class="col-md-3 mb-3">
                    <label>UF Expedição</label>
                    <asp:TextBox ID="txtUfExpedicao" runat="server" CssClass="form-control" MaxLength="2" />
                    <asp:RegularExpressionValidator ID="valUfExpedicao" runat="server" ControlToValidate="txtUfExpedicao" ValidationExpression="^[A-Z]{2}$" ErrorMessage="UF inválida" ForeColor="red" Display="Dynamic" />
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-3 mb-3">
                    <label>Data Nascimento</label>
                    <asp:TextBox ID="txtDataNascimento" runat="server" CssClass="form-control" MaxLength="10" />
                    <ajaxToolkit:MaskedEditExtender ID="meeDataNasc" runat="server"
                        TargetControlID="txtDataNascimento"
                        Mask="99/99/9999"
                        ClearMaskOnLostFocus="False"
                        MaskType="Date"
                        InputDirection="LeftToRight"
                        MessageValidatorTip="true"
                        CultureName="pt-BR" />
                    <asp:CompareValidator ID="valDataNasc" runat="server" ControlToValidate="txtDataNascimento" Operator="DataTypeCheck" Type="Date" ErrorMessage="Data Nascimento inválida" ForeColor="red" Display="Dynamic" />
                </div>
                <div class="col-md-2 mb-3">
                    <label>Sexo</label>
                    <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Selecione" Value="" />
                        <asp:ListItem Text="Masculino" Value="M" />
                        <asp:ListItem Text="Feminino" Value="F" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="valSexo" runat="server" ControlToValidate="ddlSexo" InitialValue="" ErrorMessage="Selecione o sexo" ForeColor="red" Display="Dynamic" />
                </div>
                <div class="col-md-3 mb-3">
                    <label>Estado Civil</label>
                    <asp:TextBox ID="txtEstadoCivil" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="valEstadoCivil" runat="server" ControlToValidate="txtEstadoCivil" ErrorMessage="Estado Civil obrigatório" ForeColor="red" Display="Dynamic" />
                </div>
            </div>

            <h3 class="mt-4 mb-3">Endereço</h3>

            <div class="row mb-3">
                <div class="col-md-2 mb-3">
                    <label>CEP</label>
                    <asp:TextBox ID="txtCEP" runat="server" CssClass="form-control" MaxLength="9" />
                    <asp:RequiredFieldValidator ID="valCEP" runat="server" ControlToValidate="txtCEP" ErrorMessage="CEP obrigatório" ForeColor="red" Display="Dynamic" />
                </div>
                <div class="col-md-4 mb-3">
                    <label>Logradouro</label>
                    <asp:TextBox ID="txtLogradouro" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="valLogradouro" runat="server" ControlToValidate="txtLogradouro" ErrorMessage="Logradouro obrigatório" ForeColor="red" Display="Dynamic" />
                </div>
                <div class="col-md-1 mb-3">
                    <label>Número</label>
                    <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="valNumero" runat="server" ControlToValidate="txtNumero" ErrorMessage="Número obrigatório" ForeColor="red" Display="Dynamic" />
                </div>
                <div class="col-md-3 mb-3">
                    <label>Complemento</label>
                    <asp:TextBox ID="txtComplemento" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-2 mb-3">
                    <label>Bairro</label>
                    <asp:TextBox ID="txtBairro" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="valBairro" runat="server" ControlToValidate="txtBairro" ErrorMessage="Bairro obrigatório" ForeColor="red" Display="Dynamic" />
                </div>
            </div>

            <div class="row mb-4">
                <div class="col-md-4 mb-3">
                    <label>Cidade</label>
                    <asp:TextBox ID="txtCidade" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="valCidade" runat="server" ControlToValidate="txtCidade" ErrorMessage="Cidade obrigatória" ForeColor="red" Display="Dynamic" />
                </div>
                <div class="col-md-2 mb-3">
                    <label>UF</label>
                    <asp:TextBox ID="txtUF" runat="server" CssClass="form-control" MaxLength="2" />
                    <asp:RegularExpressionValidator ID="valUF" runat="server" ControlToValidate="txtUF" ValidationExpression="^[A-Z]{2}$" ErrorMessage="UF inválida" ForeColor="red" Display="Dynamic" />
                </div>
            </div>

            <div class="button-container mt-3 mb-4">
                <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-primary btn-lg" OnClick="btnSalvar_Click" CausesValidation="true" />
            </div>

            <hr />

            <div class="table-responsive">
                <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered" DataKeyNames="Id" OnRowEditing="gvClientes_RowEditing" OnRowDeleting="gvClientes_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" />
                        <asp:BoundField DataField="Nome" HeaderText="Nome" />
                        <asp:BoundField DataField="CPF" HeaderText="CPF" />
                        <asp:BoundField DataField="RG" HeaderText="RG" />
                        <asp:BoundField DataField="DataNascimento" HeaderText="Data Nascimento" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Sexo" HeaderText="Sexo" />
                        <asp:BoundField DataField="EstadoCivil" HeaderText="Estado Civil" />

                        <asp:TemplateField ItemStyle-CssClass="gv-icon-cell">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEditar" CausesValidation="false" runat="server" CommandName="Edit" CssClass="btn btn-sm btn-outline-primary">
                                    <i class="bi bi-pencil"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="btnEditarSalvar" runat="server" CommandName="Update" CssClass="btn btn-sm btn-success">
                                    <i class="bi bi-check2"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnCancelar" CausesValidation="false" runat="server" CommandName="Cancel" CssClass="btn btn-sm btn-secondary">
                                    <i class="bi bi-x"></i>
                                </asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-CssClass="gv-icon-cell">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnExcluir" CausesValidation="false" runat="server" CommandName="Delete" CssClass="btn btn-sm btn-outline-danger">
                                    <i class="bi bi-trash"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </form>
    </div>
</body>
</html>