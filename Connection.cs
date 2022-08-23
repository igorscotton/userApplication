using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UserAplication
{
    public class Connection
    {
        private string connString = String.Format("Server={0};Port={1};" +
                                    "User Id={2};Password={3};Database={4}", "localhost", 5432,
                                    "postgres", "12345678", "postgres");

        private NpgsqlConnection conn;

        private string sql;

        private NpgsqlCommand commandSelect;

        private NpgsqlCommand commandInsert;

        private NpgsqlCommand commandDelete;

        public Connection() 
        {
            conn = new NpgsqlConnection(connString);
        }


        public List<Usuario> Select()
        {
            
            List<Usuario> lista = new List<Usuario>();

            try
            {
                conn.Open();               

                sql = @"SELECT * FROM usuario";
                commandSelect = new NpgsqlCommand(sql, conn);
                                
                NpgsqlDataReader reader = commandSelect.ExecuteReader();

                while (reader.Read())
                {
                    Usuario usuario = new Usuario();

                    usuario.Id = reader.GetInt32(0);
                    usuario.Name = reader.GetString(1);
                    usuario.Email = reader.GetString(2);
                    usuario.Address = reader.GetString(3);
                    usuario.Job = reader.GetString(4);

                    lista.Add(usuario);
                }

                return lista;

            }
            catch(Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
                return lista;
            }
            finally
            {
                conn.Close();
            }
        }

        public void InsertUser(Usuario usuario)
        {
            try
            {
                conn.Open();
                sql = String.Format("INSERT INTO usuario(name, email, address, job) VALUES ('{0}', '{1}', '{2}', '{3}') RETURNING id", usuario.Name, usuario.Email, usuario.Address, usuario.Job);
                commandInsert = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader reader = commandInsert.ExecuteReader();

                while (reader.Read())
                {
                    usuario.Id = reader.GetInt32(0);
                }               
                
                
            }
            catch(Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
            finally
            {                
                conn.Close();
            }
            

        }

        public void UpdateUser(Usuario usuario)
        {
            try
            {
                conn.Open();
                sql = String.Format("UPDATE usuario SET name = '{0}', email = '{1}', address='{2}', job='{3}' WHERE id = '{4}'", usuario.Name, usuario.Email, usuario.Address, usuario.Job, usuario.Id);                
                
                commandInsert = new NpgsqlCommand(sql, conn);
                commandInsert.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void RemoveUser(Usuario usuario)
        {
            try
            {
                conn.Open();
                sql = String.Format("DELETE FROM usuario WHERE id = '{0}'", usuario.Id);
                commandDelete = new NpgsqlCommand(sql, conn);
                commandDelete.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
            finally
            {
                conn.Close();
            }
            
        }
    }
}
