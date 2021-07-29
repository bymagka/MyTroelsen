using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace WPFNotifications.Models
{
    public class EntityBase : INotifyDataErrorInfo
    {
        readonly Dictionary<string, IList<string>> _errors = new Dictionary<string, IList<string>>();

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName)) return _errors.Values;

            return _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors => _errors.Count != 0;


        protected virtual void OnErrorsChanged(string propertyName)
        {

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected void AddEror(string propertyName, string error)
        {
            AddErrors(propertyName, new List<string> { error });
        }

        protected void AddErrors(string propertyName, IList<string> listOfErrors)
        {
            bool changed = false;

            if (listOfErrors is null || listOfErrors.Count == 0)
            {
                return;
            }

            if (!_errors.ContainsKey(propertyName))
            {
                _errors.Add(propertyName, listOfErrors);
                changed = true;
            }

            foreach (var item in listOfErrors)
            {
                if (_errors[propertyName].Contains(item)) continue;
                changed = true;
                _errors[propertyName].Add(item);
            }

            if (changed)
            {
                OnErrorsChanged(propertyName);
            }


        }

        protected void ClearErrors(string propertyName = "")
        {
            if (String.IsNullOrEmpty(propertyName)) _errors.Clear();
            else _errors.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }

        protected string[] GetErrorsFromAnnotations<T>(string propertyName, T value)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext vc = new ValidationContext(this, null, null) { MemberName = propertyName};
            var isValid = Validator.TryValidateProperty(value, vc, results);

            return isValid ? null : Array.ConvertAll(results.ToArray(), o => o.ErrorMessage);
        }
    }
}
