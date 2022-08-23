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

        public ICommand Add { get; private set; }
        public ICommand Remove { get; private set; }
        public ICommand Update { get; private set; }

        private Connection connection;

        public MainWindowVM()
        {
            connection = new Connection();

            listaDeUsuarios = new ObservableCollection<Usuario>(connection.Select());

            IniciaComandos();    
        }

        public void IniciaComandos()
        {
            Add = new RelayCommand((object _) =>
            {
                Usuario newUser = new Usuario();
                UserWindow addWindow = new UserWindow();
                addWindow.DataContext = newUser;
                addWindow.ShowDialog();

                if ((bool)addWindow.DialogResult)
                {

                    connection.InsertUser(newUser);

                    listaDeUsuarios.Add(newUser);

                }
            });

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

            Remove = new RelayCommand((object _) =>
            {
                connection.RemoveUser(selectedUser);

                listaDeUsuarios.Remove(selectedUser);

            }, (object _) => selectedUser != null);

        }
    }
}
