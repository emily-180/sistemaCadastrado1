using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace SistemaClinicaEstetica
{
    internal class ConectaBanco
    {
        MySqlConnection conexao = new MySqlConnection("server=localhost;user id=root;password=;database=clinicaestetica");
        public String mensagem;

        public DataTable listaProcedimentos() // listaGeneros dataTable tabela virtual, recebe os dados do banco e coloca nela
        {
            MySqlCommand cmd = new MySqlCommand("lista_procedimentos", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conexao.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable tabela = new DataTable();
                da.Fill(tabela);
                return tabela;
            }// fim try
            catch (MySqlException e)
            {
                mensagem = "Erro:" + e.Message;
                return null;
            }
            finally
            {
                conexao.Close();
            }

        }// fim lista_procedimentos

        public DataTable listaConsultas() //dataTable tabela virtual, recebe os dados do banco e coloca nela
        {
            MySqlCommand cmd = new MySqlCommand("lista_consultas", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conexao.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable tabela = new DataTable();
                da.Fill(tabela);
                return tabela;
            }// fim try
            catch (MySqlException e)
            {
                mensagem = "Erro:" + e.Message;
                return null;
            }
            finally
            {
                conexao.Close();
            }

        }// fim lista_bandas

        public bool insereConsulta(Consulta c)
        {
            MySqlCommand cmd = new MySqlCommand("proc_insereConsulta", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("cliente", c.Cliente);
            cmd.Parameters.AddWithValue("cpf", c.Cpf);
            cmd.Parameters.AddWithValue("telefone", c.Telefone);
            cmd.Parameters.AddWithValue("hora", c.Hora);
            cmd.Parameters.AddWithValue("dataD", c.DataD);
            cmd.Parameters.AddWithValue("proce", c.Proce);
            try
            {
                conexao.Open();
                cmd.ExecuteNonQuery(); // executa o comando
                return true;
            }
            catch (MySqlException e)
            {
                mensagem = "Erro:" + e.Message;
                return false;
            }
            finally
            {
                conexao.Close();
            }
        }// fim insereConsulta

        public bool deletaConsulta(int idconsulta)
        {
            MySqlCommand cmd = new MySqlCommand("proc_apagarConsulta", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("codConsulta", idconsulta);
            try
            {
                conexao.Open();
                cmd.ExecuteNonQuery(); // executa o comando
                return true;
            }
            catch (MySqlException e)
            {
                mensagem = "Erro:" + e.Message;
                return false;
            }
            finally
            {
                conexao.Close();
            }
        }// fim deletaConsulta

        public bool alteraConsulta(Consulta c, int idconsulta)
        {
            MySqlCommand cmd = new MySqlCommand("proc_alterarConsulta", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("codConsulta", idconsulta);
            cmd.Parameters.AddWithValue("cliente", c.Cliente);
            cmd.Parameters.AddWithValue("cpf", c.Cpf);
            cmd.Parameters.AddWithValue("telefone", c.Telefone);
            cmd.Parameters.AddWithValue("hora", c.Hora);
            cmd.Parameters.AddWithValue("dataD", c.DataD);
            cmd.Parameters.AddWithValue("proce", c.Proce);
            try
            {
                conexao.Open();
                cmd.ExecuteNonQuery(); // executa o comando
                return true;
            }
            catch (MySqlException e)
            {
                mensagem = "Erro:" + e.Message;
                return false;
            }
            finally
            {
                conexao.Close();
            }
        }// fim update_consulta


        //—--------------------------


    }
}
