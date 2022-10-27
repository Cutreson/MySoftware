using System;
using System.Text;

namespace EventHandlerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            HocSinh hs = new HocSinh();
            hs.NameChanged += Hs_NameChanged;
            hs.Name = "Tên lần 1";
            hs.Name = "Tên lần 2";
            hs.Name = "Tên cuối";

            Console.ReadLine();
        }

        private static void Hs_NameChanged(object sender, NameChangedEventArgs e)
        {
            Console.WriteLine("Tên có thay đổi: " + e.Name);
        }
    }

    public class HocSinh
    {
        private string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                _Name = value;
                OnNameChanged(value);
            }
        }


        private event EventHandler<NameChangedEventArgs> _NameChanged;
        public event EventHandler<NameChangedEventArgs> NameChanged
        {
            add
            {
                _NameChanged += value;
            }
            remove
            {
                _NameChanged -= value;
            }
        }

        void OnNameChanged(string name)
        {
            if (_NameChanged != null)
            {
                _NameChanged(this, new NameChangedEventArgs(name));
            }
        }
    }

    public class NameChangedEventArgs : EventArgs
    {
        public string Name { get; set; }
        public NameChangedEventArgs(string name)
        {
            Name = name;
        }
    }

}
