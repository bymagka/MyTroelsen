using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFNotifications.Models;
using WPFNotifications.Cmds;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace WPFNotifications.ViewModels
{
    public class MainWindowViewModel

    {
        public IList<Inventory> Cars { get; } = new ObservableCollection<Inventory>();



        private ICommand _changeColorCommand = null;

        public ICommand ChangeColorCmd => _changeColorCommand ?? (_changeColorCommand = new ChangeColorCommand());


        private ICommand _addColorCommand = null;

        public ICommand AddColorCommand => _addColorCommand ?? (_addColorCommand = new AddCarCommand());


        private RelayCommand<Inventory> _deleteCarCommand = null;

        public RelayCommand<Inventory> DeleteCarCommand => _deleteCarCommand ?? (_deleteCarCommand = new RelayCommand<Inventory>(DeleteCar, CanDeleteCar));

        private bool CanDeleteCar(Inventory car)
        {
            return car != null;
        }

        private void DeleteCar(Inventory car)
        {
            Cars.Remove(car);
        }


        public MainWindowViewModel()
        {
            Cars.Add(new Inventory { CarId = 1, Color = "Blue", Make = "Chevy", PetName = "Kit", IsChanged = false });
            Cars.Add(new Inventory { CarId = 2, Color = "Red", Make = "Ford", PetName = "Red Rider", IsChanged = false });
        }


    }
}
