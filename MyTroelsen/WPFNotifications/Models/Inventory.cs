using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace WPFNotifications.Models
{
    public class InventoryManual : INotifyPropertyChanged
    {
        int _carId;
        string _make;
        string _color;
        string _petName;
        bool _isChanged;

        public int CarId { get => _carId; 
            set 
            { 
                if (value == _carId) return;
                else
                {
                    _carId = value;
                    OnPropertyChanged();
                }
            } 
        }

        public string Make
        {
            get => _make;
            set
            {
                if (value == _make) return;
                else
                {
                    _make = value;
                    OnPropertyChanged(nameof(Color));
                }
            }
        }
        
        public string Color
        {
            get => _color;
            set
            {
                if (value == _color) return;
                else
                {
                    _color = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PetName
        {
            get => _petName; 
            set
            {
                if (value == _petName) return;
                else
                {
                    _petName = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsChanged
        {
            get => _isChanged;
            set
            {
                if (value == _isChanged) return;
                else
                {
                    _isChanged = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName != nameof(IsChanged)) IsChanged = true;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public partial class Inventory : INotifyPropertyChanged, IDataErrorInfo
    {

        public bool IsChanged { get; set; }
        [Required]       
        public int CarId { get; set; }
        [Required]
        [StringLength(20)]
        public string Make { get; set; }
        [Required]
        [StringLength(20)]
        public string Color { get; set; }
        [StringLength(20)]
        public string PetName { get; set; }

        

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public partial class Inventory : EntityBase
    {
        private string _error;
        public string Error => _error;


       



        public string this[string columnName] {
            get
                {
                bool hasError = false;
                switch (columnName)
                {
                    case nameof(CarId):
                        AddErrors(nameof(CarId), GetErrorsFromAnnotations(nameof(CarId), CarId));
                        break;

                    case nameof(Make):

                        hasError = CheckColorAndMake();
                        if (Make == "ModelT")
                        {
                            AddEror(nameof(Make), "Too Old!");
                            hasError = true;
                        }
                        if (!hasError)
                        {
                            ClearErrors(nameof(Make));
                            ClearErrors(nameof(Color));
                        }
                        AddErrors(nameof(Make), GetErrorsFromAnnotations(nameof(Make), Make));
                        break;

                    case nameof(Color):
                        hasError = CheckColorAndMake();

                        if (!hasError)
                        {
                            ClearErrors(nameof(Make));
                            ClearErrors(nameof(Color));
                        }
                        AddErrors(nameof(Make), GetErrorsFromAnnotations(nameof(Color), Color));
                        break;

                    case nameof(PetName):
                        AddErrors(nameof(PetName), GetErrorsFromAnnotations(nameof(PetName), PetName));
                        break;

                }
                    return string.Empty;
                }
        }

        bool CheckColorAndMake()
        {
            if(Make == "Chevy" && Color == "Pink")
            {
                AddEror(nameof(Make), $"{Make}'s don't come in {Color}");
                AddEror(nameof(Color), $"{Make}'s don't come in {Color}");
                return true;
            }
            return false;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if(propertyName != nameof(IsChanged))
            {
                IsChanged = true;
            };

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        }


        //
      


    }
}
