<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm.Default" %>

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

    <h2 class="mb-4">Cliente</h2>

    <div class="row mb-3">
        <div class="col-md-4 mb-3">
            <label>CPF</label>
            <asp:TextBox ID="txtCpf" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-8 mb-3">
            <label>Nome</label>
            <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" />
        </div>
    </div>

    <div class="row mb-3">
        <div class="col-md-2 mb-3">
            <label>RG</label>
            <asp:TextBox ID="txtRg" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-3 mb-3">
            <label>Data Expedição</label>
            <asp:TextBox ID="txtDataExpedicao" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-4 mb-3">
            <label>Órgão Expedição</label>
            <asp:TextBox ID="txtOrgaoExpedicao" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-3 mb-3">
            <label>UF Expedição</label>
            <asp:TextBox ID="txtUfExpedicao" runat="server" CssClass="form-control" />
        </div>
    </div>

    <div class="row mb-3">
        <div class="col-md-3 mb-3">
            <label>Data Nascimento</label>
            <asp:TextBox ID="txtDataNascimento" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-2 mb-3">
            <label>Sexo</label>
            <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-control">
                <asp:ListItem Text="Masculino" Value="M" />
                <asp:ListItem Text="Feminino" Value="F" />
            </asp:DropDownList>
        </div>
        <div class="col-md-3 mb-3">
            <label>Estado Civil</label>
            <asp:TextBox ID="txtEstadoCivil" runat="server" CssClass="form-control" />
        </div>
    </div>

    <h3 class="mt-4 mb-3">Endereço</h3>

    <div class="row mb-3">
        <div class="col-md-2 mb-3">
            <label>CEP</label>
            <asp:TextBox ID="txtCEP" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-4 mb-3">
            <label>Logradouro</label>
            <asp:TextBox ID="txtLogradouro" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-1 mb-3">
            <label>Número</label>
            <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-3 mb-3">
            <label>Complemento</label>
            <asp:TextBox ID="txtComplemento" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-2 mb-3">
            <label>Bairro</label>
            <asp:TextBox ID="txtBairro" runat="server" CssClass="form-control" />
        </div>
    </div>

    <div class="row mb-4">
        <div class="col-md-4 mb-3">
            <label>Cidade</label>
            <asp:TextBox ID="txtCidade" runat="server" CssClass="form-control" />
        </div>
        <div class="col-md-2 mb-3">
            <label>UF</label>
            <asp:TextBox ID="txtUF" runat="server" CssClass="form-control" />
        </div>
    </div>

    <div class="button-container">
        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="btnSalvar_Click" />
    </div>

    <hr />

<div class="table-responsive">
<asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="false"
    CssClass="table table-striped table-bordered"
    DataKeyNames="Id"
    OnRowEditing="gvClientes_RowEditing"
    OnRowDeleting="gvClientes_RowDeleting">

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
            <asp:LinkButton ID="btnEditar" runat="server" CommandName="Edit" CssClass="btn btn-sm btn-outline-primary">
                <i class="bi bi-pencil"></i>
            </asp:LinkButton>
        </ItemTemplate>
        <EditItemTemplate>
            <asp:LinkButton ID="btnEditarSalvar" runat="server" CommandName="Update" CssClass="btn btn-sm btn-success">
                <i class="bi bi-check2"></i>
            </asp:LinkButton>
            <asp:LinkButton ID="btnCancelar" runat="server" CommandName="Cancel" CssClass="btn btn-sm btn-secondary">
                <i class="bi bi-x"></i>
            </asp:LinkButton>
        </EditItemTemplate>
    </asp:TemplateField>

    <asp:TemplateField ItemStyle-CssClass="gv-icon-cell">
        <ItemTemplate>
            <asp:LinkButton ID="btnExcluir" runat="server" CommandName="Delete" CssClass="btn btn-sm btn-outline-danger">
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