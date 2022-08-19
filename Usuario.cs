using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAplication
{
    public class Usuario : INotifyPropertyChanged, ICloneable
    {
        private int id;
        private string name;
        private string email;
        private string address;
        private string job;

        public event PropertyChangedEventHandler PropertyChanged;
        public Usuario() { }

        public Usuario(int id ,string name, string email, string address, string job)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.address = address;
            this.job = job;
        }        

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Usuario Clone()
        {
            Usuario cloneUsario = (Usuario)this.MemberwiseClone();
            return cloneUsario;
        }

        object ICloneable.Clone()
        {
            throw new NotImplementedException();
        }

        public int Id
        {
            get { return id; }
            set 
            { 
                id = value;
                NotifyPropertyChanged("Id");
            }
        }

        public string Name
        {
            get { return name; }
            set { 
                name = value;
                NotifyPropertyChanged("Name");
            }
        }
        public string Email
        {
            get { return email; }
            set {
                email = value;
                NotifyPropertyChanged("Email");
            }
        }
        public string Address
        {
            get { return address; }
            set { 
                address = value;
                NotifyPropertyChanged("Address");
            }
        }

        public string Job
        {
            get { return job; }
            set { 
                job = value;
                NotifyPropertyChanged("Job");
            }
        }
    }
}
