using App.RegistroFunc.DAL.Interface;
using App.RegistroFunc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace App.RegistroFunc.DAL
{
	public class FuncionarioDAL : IFuncionarioDAL
	{
		private string sqlConn()
		{
			string SqlConn = "Password=admin;Persist Security Info=True;User ID=admin;Initial Catalog=cadastroDB;Data Source=DESKTOP-9LK3UQP";
			return SqlConn;
		}

		IEnumerable<Funcionario> IFuncionarioDAL.GetAllFuncionarios()
		{
			List<Funcionario> lstFuncinario = new List<Funcionario>();

			using (SqlConnection connection = new SqlConnection(sqlConn()))
			{
				SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Funcionarios", connection);
				cmd.CommandType = CommandType.Text;

				connection.Open();
				SqlDataReader rdr = cmd.ExecuteReader();

				while (rdr.Read())
				{
					Funcionario funcionario = new Funcionario();

					funcionario.FuncionarioId = Convert.ToInt32(rdr["FuncionarioID"]);
					funcionario.Nome = rdr["Nome"].ToString();
					funcionario.Cidade = rdr["Cidade"].ToString();
					funcionario.Departamento = rdr["Departamento"].ToString();
					funcionario.Sexo = rdr["Sexo"].ToString();

					lstFuncinario.Add(funcionario);
				}

				connection.Close();
			}

			return lstFuncinario;
		}

		public void AddFuncionario(Funcionario funcionario)
		{
			using (SqlConnection connection = new SqlConnection(sqlConn()))
			{
				string comandoSQL = "INSERT INTO dbo.Funcionarios (Nome, Cidade, Departamento, Sexo) VALUES (@Nome, @Cidade, @Departamento, @Sexo)";

				SqlCommand cmd = new SqlCommand(comandoSQL, connection);
				cmd.CommandType = CommandType.Text;

				cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
				cmd.Parameters.AddWithValue("@Cidade", funcionario.Cidade);
				cmd.Parameters.AddWithValue("@Departamento", funcionario.Departamento);
				cmd.Parameters.AddWithValue("@Sexo", funcionario.Sexo);

				connection.Open();
				cmd.ExecuteNonQuery();
				connection.Close();
			}
		}

		public void UpdateFuncionario(Funcionario funcionario)
		{
			using (SqlConnection connection = new SqlConnection(sqlConn()))
			{
				string comandoSQL = "UPDATE dbo.Funcionarios SET Nome = @Nome, Cidade = @Cidade, Departamento = @Departamento, Sexo = @Sexo WHERE FuncionarioID = @FuncionarioID";

				SqlCommand cmd = new SqlCommand(comandoSQL, connection);
				cmd.CommandType = CommandType.Text;

				cmd.Parameters.AddWithValue("@FuncionarioID", funcionario.FuncionarioId);
				cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
				cmd.Parameters.AddWithValue("@Cidade", funcionario.Cidade);
				cmd.Parameters.AddWithValue("@Departamento", funcionario.Departamento);
				cmd.Parameters.AddWithValue("@Sexo", funcionario.Sexo);

				connection.Open();
				cmd.ExecuteNonQuery();
				connection.Close();
			}
		}

		public Funcionario GetFuncionario(int? id)
		{
			Funcionario funcionario = null;

			using (SqlConnection connection = new SqlConnection(sqlConn()))
			{
				string comandoSQL = $"SELECT * FROM dbo.Funcionarios WHERE FuncionarioID={id}";
				SqlCommand cmd = new SqlCommand(comandoSQL, connection);

				connection.Open();
				SqlDataReader rdr = cmd.ExecuteReader();

				if (rdr.HasRows)
				{
					funcionario = new Funcionario();
					while (rdr.Read())
					{
						string funcString = rdr["FuncionarioID"].ToString();
						funcionario.FuncionarioId = Convert.ToInt32(funcString);
						funcionario.Nome = rdr["Nome"].ToString();
						funcionario.Cidade = rdr["Cidade"].ToString();
						funcionario.Departamento = rdr["Departamento"].ToString();
						funcionario.Sexo = rdr["Sexo"].ToString();
					}
				}

				return funcionario;
			}

		}

		public void DeleteFuncionario(int? id)
		{
			using (SqlConnection connection = new SqlConnection(sqlConn()))
			{
				string comandoSQL = "DELETE FROM dbo.Funcionarios WHERE FuncionarioID = @FuncionarioID";

				SqlCommand cmd = new SqlCommand(comandoSQL, connection);
				cmd.CommandType = CommandType.Text;

				cmd.Parameters.AddWithValue("@FuncionarioID", id);

				connection.Open();
				cmd.ExecuteNonQuery();
				connection.Close();
			}
		}		
	}
}
