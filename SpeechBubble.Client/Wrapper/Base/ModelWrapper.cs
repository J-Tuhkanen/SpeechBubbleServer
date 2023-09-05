using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using SpeechBubble.Client.ViewModels.Base;

namespace SpeechBubble.Client.Wrapper.Base
{
    public abstract class ModelWrapper<TModel> : NotifyDataErrorInfoBase
        where TModel : class
    {
        protected ModelWrapper(TModel model)
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public TModel Model { get; }

        protected virtual TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
        {
            return (TValue)typeof(TModel).GetProperty(propertyName).GetValue(Model);
        }

        protected virtual void SetValue<TValue>(TValue value, [CallerMemberName] string propertyName = null)
        {
            typeof(TModel).GetProperty(propertyName).SetValue(Model, value);

            RaisePropertyChanged(propertyName);
            ValidatePropertyInternal(propertyName, value);
        }

        private void ValidatePropertyInternal(string propertyName, object currentValue)
        {
            ClearErrors(propertyName);

            ValidateDataAnnotations(propertyName, currentValue);
            ValidateCustomErrors(propertyName);
        }

        private void ValidateCustomErrors(string propertyName)
        {
            var errors = OnValidateProperty(propertyName);

            if (errors != null)
            {
                foreach (var error in errors)
                {
                    AddError(propertyName, error);
                }
            }
        }

        private void ValidateDataAnnotations(string propertyName, object currentValue)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(Model) { MemberName = propertyName };
            Validator.TryValidateProperty(currentValue, context, results);
            foreach (var result in results)
            {
                AddError(propertyName, result.ErrorMessage);
            }
        }

        protected virtual IEnumerable<string> OnValidateProperty(string propertyNameerrors)
        {
            return null;
        }
    }
}
