using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace UserAplication
{
    public class MainWindowVM
    {
        public ObservableCollection<Usuario> listaDeUsuarios { get; set; }
        public Usuario selectedUser {get; set;}

        public ICommand Add { get; set; }
        public ICommand Remove { get; set; }
        public ICommand Update { get; set; }

        public Connection connection { get; set; }

        public MainWindowVM()
        {
            connection = new Connection();

            listaDeUsuarios = new ObservableCollection<Usuario>(connection.Select());

            AdicionarUsuario(connection);

            AtualizarUsuario(connection);

            RemoverUsuario(connection);
            
        }

        public void AdicionarUsuario(Connection connection)
        {
            Add = new RelayCommand((object _) =>
            {                
                Usuario newUser = new Usuario();
                UserWindow addWindow = new UserWindow();
                addWindow.DataContext = newUser;
                addWindow.ShowDialog();

                if ((bool)addWindow.DialogResult){

                   newUser.Id = listaDeUsuarios.Count() + 1;
                    
                   connection.InsertUser(newUser);

                   listaDeUsuarios.Add(newUser);
                  
                }
            });
        }

        public void AtualizarUsuario(Connection connection)
        {
            Update = new RelayCommand((object _) =>
            {
                Usuario updateUser = selectedUser.Clone();

                UserWindow updateWindow = new UserWindow();

                updateWindow.DataContext = updateUser;

                updateWindow.ShowDialog();                
                
                if ((bool)updateWindow.DialogResult)
                {
                    selectedUser.Name = updateUser.Name;
                    selectedUser.Email = updateUser.Email;
                    selectedUser.Address = updateUser.Address;
                    selectedUser.Job = updateUser.Job;

                    connection.UpdateUser(selectedUser);
                }
            }, (object _) => selectedUser != null);
        }

        public void RemoverUsuario(Connection connection)
        {
            Remove = new RelayCommand((object _) =>
            {
                connection.RemoveUser(selectedUser);

                listaDeUsuarios.Remove(selectedUser);

            },(object _) => selectedUser != null);
        }
    }
}
