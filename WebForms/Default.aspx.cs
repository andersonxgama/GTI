using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm
{
    public partial class Default : Page
    {
        // guarda o Id do cliente que tá sendo editado
        private int ClienteIdEditando
        {
            get => ViewState["ClienteIdEditando"] != null ? (int)ViewState["ClienteIdEditando"] : 0;
            set => ViewState["ClienteIdEditando"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarClientes();
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            var client = new WebForm.ClienteWcf.ClienteServiceClient();

            var cliente = new WebForm.ClienteWcf.Cliente
            {
                Id = ClienteIdEditando, // se for 0, é inserção
                Nome = txtNome.Text,
                CPF = txtCpf.Text,
                RG = txtRg.Text,
                DataExpedicao = DateTime.Parse(txtDataExpedicao.Text),
                OrgaoExpedicao = txtOrgaoExpedicao.Text,
                UFExpedicao = txtUfExpedicao.Text,
                DataNascimento = DateTime.Parse(txtDataNascimento.Text),
                Sexo = ddlSexo.SelectedValue,
                EstadoCivil = txtEstadoCivil.Text,
                Endereco = new WebForm.ClienteWcf.Endereco
                {
                    CEP = txtCEP.Text,
                    Logradouro = txtLogradouro.Text,
                    Numero = txtNumero.Text,
                    Complemento = txtComplemento.Text,
                    Bairro = txtBairro.Text,
                    Cidade = txtCidade.Text,
                    UF = txtUF.Text
                }
            };

            if (ClienteIdEditando == 0)
                client.Inserir(cliente); // novo
            else
                client.Atualizar(cliente); // edição

            ClienteIdEditando = 0; // limpa edição
            LimparFormulario();
            CarregarClientes();
        }

        private void CarregarClientes()
        {
            var client = new WebForm.ClienteWcf.ClienteServiceClient();

            // converte pra array de Cliente pra não dar erro no Array.Find
            var clientes = client.Listar();

            gvClientes.DataSource = clientes;
            gvClientes.DataKeyNames = new string[] { "Id" }; // necessário pra editar/excluir
            gvClientes.DataBind();
        }

        private void LimparFormulario()
        {
            txtNome.Text = "";
            txtCpf.Text = "";
            txtRg.Text = "";
            txtDataExpedicao.Text = "";
            txtOrgaoExpedicao.Text = "";
            txtUfExpedicao.Text = "";
            txtDataNascimento.Text = "";
            ddlSexo.SelectedIndex = 0;
            txtEstadoCivil.Text = "";
            txtCEP.Text = "";
            txtLogradouro.Text = "";
            txtNumero.Text = "";
            txtComplemento.Text = "";
            txtBairro.Text = "";
            txtCidade.Text = "";
            txtUF.Text = "";
        }

        protected void gvClientes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            var client = new ClienteWcf.ClienteServiceClient();
            var clientes = client.Listar();

            int id = Convert.ToInt32(gvClientes.DataKeys[e.NewEditIndex].Value);
            var cliente = Array.Find(clientes, c => c.Id == id);

            if (cliente != null)
            {
                ClienteIdEditando = cliente.Id;

                txtNome.Text = cliente.Nome;
                txtCpf.Text = cliente.CPF;
                txtRg.Text = cliente.RG;
                txtDataExpedicao.Text = cliente.DataExpedicao.ToString("dd/MM/yyyy");
                txtOrgaoExpedicao.Text = cliente.OrgaoExpedicao;
                txtUfExpedicao.Text = cliente.UFExpedicao;
                txtDataNascimento.Text = cliente.DataNascimento.ToString("dd/MM/yyyy");
                ddlSexo.SelectedValue = cliente.Sexo;
                txtEstadoCivil.Text = cliente.EstadoCivil;

                txtCEP.Text = cliente.Endereco.CEP;
                txtLogradouro.Text = cliente.Endereco.Logradouro;
                txtNumero.Text = cliente.Endereco.Numero;
                txtComplemento.Text = cliente.Endereco.Complemento;
                txtBairro.Text = cliente.Endereco.Bairro;
                txtCidade.Text = cliente.Endereco.Cidade;
                txtUF.Text = cliente.Endereco.UF;
            }

            e.Cancel = true; // cancela edição na grid
        }

        protected void gvClientes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var client = new ClienteWcf.ClienteServiceClient();
            int id = Convert.ToInt32(gvClientes.DataKeys[e.RowIndex].Value);

            client.Excluir(id);

            CarregarClientes();
        }
    }
}
