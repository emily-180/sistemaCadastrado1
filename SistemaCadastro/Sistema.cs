using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SistemaClinicaEstetica
{
    public partial class Sistema : Form
    {
        int idAlterar;
        public Sistema()
        {
            InitializeComponent();
            
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCadastra_Click(object sender, EventArgs e)
        {
            marcador.Height = btnCadastra.Height;
            marcador.Top = btnCadastra.Top;
            tabControl1.SelectedTab = tabControl1.TabPages[0];
        }
        

        private void btnBusca_Click(object sender, EventArgs e)
        {
            marcador.Height = btnBusca.Height;
            marcador.Top = btnBusca.Top;
            tabControl1.SelectedTab = tabControl1.TabPages[1];
        }

        void listaProcedimento()
        {
            ConectaBanco con = new ConectaBanco();
            DataTable tabelaDados = new DataTable();
            tabelaDados = con.listaProcedimentos(); //mudar o campo cbGenero //caixa box
            cbProcedimento.DataSource = tabelaDados;
            cbProcedimento.DisplayMember = "nomeProcedimento";
            cbProcedimento.ValueMember = "codProcedimento";
            //preenchndo cbAlteraProcedimento
            cbAlteraProcedimento.DataSource = tabelaDados;
            cbAlteraProcedimento.DisplayMember = "nomeProcedimento";
            cbAlteraProcedimento.ValueMember = "codProcedimento";
            lblmsgerro.Text = con.mensagem;
            cbProcedimento.Text = "";
            cbAlteraProcedimento.Text = "";
        }

        void listaConsulta()
        {
            ConectaBanco con = new ConectaBanco(); //mudar dgbandas
            dgProcedimentos.DataSource = con.listaConsultas();            
        }
        void limpaCampos()
        {
            txtcliente.Text = "";
            txtcpf.Text = "";
            txttelefone.Text = "";
            txtdata.Text = "";
            txthora.Text = "";
            txtproce.Text = "";
            txtcliente.Focus();
        }
        private void Sistema_Load(object sender, EventArgs e)
        {
            listaProcedimento();
            listaConsulta();
        }

        private void BtnConfirmaCadastro_Click_1(object sender, EventArgs e)
        {
            Consulta c = new Consulta();
            c.Cliente = txtnome.Text;
            c.Cpf = txtcpf.Text;
            c.Telefone = txttelefone.Text;
            c.Hora = txthora.Text;
            c.DataD = txtdataD.Text;
            c.Proce = txtproce.Text;
            //enviar a ficha para o banco
            ConectaBanco conecta = new ConectaBanco();
            bool retorno = conecta.insereConsulta(c);
            if (retorno == true)
            {
                MessageBox.Show("Dados inseridos com sucesso!");
                listaConsulta();
                limpaCampos();
            }
            else
                lblmsgerro.Text = conecta.mensagem;
        }

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            (dgConsultas.DataSource as DataTable).DefaultView.RowFilter = String.Format("nome like '{0}%'", txtBusca.Text);
        }

        private void btnRemoveBanda_Click(object sender, EventArgs e)
        {
            int linha = dgConsultas.CurrentRow.Index;//pega alinha selecionada
            int idRemover = Convert.ToInt32(dgConsultas.Rows[linha].Cells["codConsulta"].Value.ToString());
            DialogResult resp = MessageBox.Show("Confirmar exclusão?", "Remove consulta", MessageBoxButtons.OKCancel);
            if (resp == DialogResult.OK)
            {
                ConectaBanco conecta = new ConectaBanco();
                bool retorno = conecta.deletaConsulta(idRemover);
                if (retorno == true)
                    MessageBox.Show("Consulta Excluida");
                else
                    lblmsgerro.Text = conecta.mensagem;
                listaConsulta();
            }
            else
                MessageBox.Show("Operacao Cancelada!");
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            int linha = dgConsultas.CurrentRow.Index;//pega alinha selecionada
            idAlterar = Convert.ToInt32(dgConsultas.Rows[linha].Cells["codConsulta"].Value.ToString());
            txtAlteraCliente.Text = dgConsultas.Rows[linha].Cells["cliente"].Value.ToString();
            txtAlteraCpf.Text = dgConsultas.Rows[linha].Cells["cpf"].Value.ToString();
            txtAlteraTelefone.Text = dgConsultas.Rows[linha].Cells["telefone"].Value.ToString();
            txtAlteraHora.Text = dgConsultas.Rows[linha].Cells["hora"].Value.ToString();
            txtAlteraDataD.Text = dgConsultas.Rows[linha].Cells["data"].Value.ToString();
            cbAlteraProce.Text = dgConsultas.Rows[linha].Cells["proce"].Value.ToString();
            tabControl1.SelectedTab = tabAlterar;
        }

         private void btnConfirmaAlteracao_Click(object sender, EventArgs e)
        {
            Consulta c = new Consulta();
            c.Cliente = txtAlteraCliente.Text;
            c.Cpf = txtAlteraCpf.Text;
            c.Telefone = txtAlteraTelefone.Text;
            c.Hora = txtAlteraHora.Text;
            c.DataD = txAlteraDataD.Text;
            c.Proce = txtAlteraProce.Text;
            //enviar os dados para alterar
            ConectaBanco conecta = new ConectaBanco();
            bool retorno = conecta.alteraConsulta(c, idAlterar);
            if (retorno)
            {
                MessageBox.Show("Dados alterados com sucesso!");
                listaConsulta();
                limpaCampos();
            }               
            else
                lblmsgerro.Text = conecta.mensagem;

        }

        private void bntAddGenero_Click(object sender, EventArgs e)
        {
          
        }

        private void tabCadastrar_Click(object sender, EventArgs e)
        {

        }
    }
}
